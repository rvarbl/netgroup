using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using App.DAL.EF;
using App.Domain.Identity;
using Base.Helpers.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Dto.Identity;
using WebApp.Api.Validation;

namespace WebApp.Api.Controllers.Identity;

[EnableCors]
[Route("api/identity/[controller]/[action]")]
[ApiController]
public class AuthenticationController : Controller
{
    private readonly SignInManager<ApplicationUser> _signInManager;
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ILogger<AuthenticationController> _logger;
    private readonly IConfiguration _configuration;
    private readonly AuthenticationValidation _validation = new (new ErrorResponse()); //TODO: use class to validate and add errors!
    private readonly ApplicationDbContext _context;

    public AuthenticationController(SignInManager<ApplicationUser> signInManager,
        UserManager<ApplicationUser> userManager, ILogger<AuthenticationController> logger,
        IConfiguration configuration, ApplicationDbContext context)
    {
        _signInManager = signInManager;
        _userManager = userManager;
        _logger = logger;
        _configuration = configuration;
        _context = context; ;
    }


    // GET: api/identity/authentication/get
    [HttpGet]
    public OkResult Get()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<JwtResponse>> Login([FromBody] LoginDto loginDto)
    {
        var refreshToken = new RefreshToken();
        return await Login(loginDto.Email, loginDto.Password, refreshToken);
    }


    [HttpPost]
    public async Task<ActionResult<JwtResponse>> Register([FromBody] RegisterDto registerDto)
    {
        var user = await _userManager.FindByEmailAsync(registerDto.Email);
        if (user != null)
        {
            _logger.LogWarning("WebApi register failed, user already exists");
            _validation.SetResponseBadRequest("Registration Failed", HttpContext.TraceIdentifier);
            _validation.SetError("user", "User Already Exists");
            return BadRequest(_validation.GetResponse());
        }

        //user creation
        var refreshToken = new RefreshToken();
        user = new ApplicationUser
        {
            Email = registerDto.Email,
            UserName = registerDto.Email,
            FirstName = registerDto.FirstName,
            LastName = registerDto.LastName,
            RefreshTokens = new List<RefreshToken>()
        };

        user.RefreshTokens.Add(refreshToken);

        //user persistence
        var creationResult = await _userManager.CreateAsync(user, registerDto.Password);
        if (!creationResult.Succeeded)
        {
            _logger.LogWarning("WebApi register failed. Failure at creating user");
            _validation.SetResponseBadRequest("Registration Failed", HttpContext.TraceIdentifier);
            _validation.SetError("user", "User Creation Failed");
            return BadRequest(_validation.GetResponse());
        }

        //adding user claims

        var claimsResult = await _userManager.AddClaimAsync(user, new Claim("aspnet.personFname", user.FirstName));
        var claimsResult2 = await _userManager.AddClaimAsync(user, new Claim("aspnet.personLname", user.LastName));
        if (!claimsResult.Succeeded || !claimsResult2.Succeeded)
        {
            _logger.LogWarning("WebApi register failed. Failure at creating user claims.");
            _validation.SetResponseBadRequest("Registration Failed", HttpContext.TraceIdentifier);
            _validation.SetError("claims", "User Creation Failed");
            return BadRequest(_validation.GetResponse());
        }


        _logger.LogInformation("User created a new account with password.");
        //if above was successful, log in.
        var identityResultRole = _userManager.AddToRolesAsync(user, new[] {"user"}).Result;
        if (!identityResultRole.Succeeded)
        {
            _logger.LogWarning("WebApi register failed. Failure at adding user to role.");
            _validation.SetResponseBadRequest("Registration Failed", HttpContext.TraceIdentifier);
            _validation.SetError("roles", "User Creation Failed");
            return BadRequest(_validation.GetResponse());
        }
        return await Login(registerDto.Email, registerDto.Password, refreshToken);
    }

    private async Task<ActionResult<JwtResponse>> Login(string email, string password, RefreshToken refreshToken)
    {
        var user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            _logger.LogWarning("WebApi login failed. User not found");
            _validation.SetResponseBadRequest("Login Failed", HttpContext.TraceIdentifier);
            _validation.SetError("login", "User Not Found");

            return BadRequest(_validation.GetResponse());
        }

        var result = await _signInManager.CheckPasswordSignInAsync(user, password, false);
        if (!result.Succeeded)
        {
            _logger.LogWarning("WebApi login failed, invalid password.");
            _validation.SetResponseBadRequest("Login Failed", HttpContext.TraceIdentifier);
            _validation.SetError("login", "Invalid Username or Password");
            return BadRequest(_validation.GetResponse());
        }

        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user.");
            _validation.SetResponseBadRequest("Login Failed", HttpContext.TraceIdentifier);
            _validation.SetError("login", "Login Failure");
            return BadRequest(_validation.GetResponse());
        }

        var roles = new List<string>(_userManager.GetRolesAsync(user).Result);
        
        var jwt = IdentityExtensions.GenerateJwt(
            claims: claimsPrincipal.Claims,
            key: _configuration["JWT:Key"],
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Issuer"],
            expirationDateTime: DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );
        var response = new JwtResponse
        {
            Token = jwt,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RefreshToken = refreshToken.Token,
            Roles = roles
        };
        
        user.RefreshTokens ??= new List<RefreshToken>();

        user.RefreshTokens.Add(refreshToken);
        _context.Users.Update(user);
        await _context.SaveChangesAsync();

        return Ok(response);
    }
    
    [HttpPost]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public async Task<ActionResult> Revoke()
    {
        var username = User.Identity?.Name;
        
        _logger.LogInformation("Logging out: " + username);
        var user = _context.Users
            .Include(x => x.RefreshTokens)
            .SingleOrDefault(u => u.UserName == username);
        if (user == null) return BadRequest();
        
       
        if (user.RefreshTokens != null)
        {
            _context.RefreshTokens.RemoveRange(user.RefreshTokens);
            user.RefreshTokens = null;
        }

        await _context.SaveChangesAsync();
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult> RefreshToken([FromBody] RefreshTokenDto refreshTokenDto)
    {
        //get info from jwt
        JwtSecurityToken? jwt;
        try
        {
            jwt = new JwtSecurityTokenHandler().ReadJwtToken(refreshTokenDto.Jwt);
            if (jwt == null)
            {
                return BadRequest("no token");
            }
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }

        //TODO:validate token

        var userEmail = jwt.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
        if (userEmail == null)
        {
            return BadRequest("no email");
        }

        //get user and tokens
        var user = await _userManager.FindByEmailAsync(userEmail);
        if (user == null)
        {
            return BadRequest("no user");
        }

        await _context.Entry(user)
            .Collection(x => x.RefreshTokens!)
            .Query()
            .Where(x =>
                x.Token == refreshTokenDto.RefreshToken && x.TokenExpirationDateTime > DateTime.UtcNow ||
                x.PreviousToken == refreshTokenDto.RefreshToken && x.PreviousTokenExpirationDateTime > DateTime.UtcNow)
            .ToListAsync();

        //load and compare refresh tokens
        if (user.RefreshTokens == null)
        {
            return Problem("RefreshTokens is null");
        }

        if (user.RefreshTokens.Count > 1)
        {
            return Problem("More than one valid refresh token found");
        }

        if (user.RefreshTokens.Count == 0)
        {
            return Problem("No valid refresh token found");
        }

        //generate new jwt
        var claimsPrincipal = await _signInManager.CreateUserPrincipalAsync(user);
        if (claimsPrincipal == null)
        {
            _logger.LogWarning("Could not get ClaimsPrincipal for user.");
            _validation.SetResponseBadRequest("Login Failed", HttpContext.TraceIdentifier);
            _validation.SetError("login", "Invalid username or password.");
            return BadRequest(_validation.GetResponse());
        }

        //generate new refresh token
        var newJwt = IdentityExtensions.GenerateJwt(
            claims: claimsPrincipal.Claims,
            key: _configuration["JWT:Key"],
            issuer: _configuration["JWT:Issuer"],
            audience: _configuration["JWT:Issuer"],
            expirationDateTime: DateTime.Now.AddMinutes(_configuration.GetValue<int>("JWT:ExpireInMinutes"))
        );

        //save new refresh token, move old one to prev, update expiration date
        var refreshToken = user.RefreshTokens.First();

        if (refreshToken.Token == refreshTokenDto.RefreshToken)
        {
            refreshToken.PreviousToken = refreshToken.Token;
            refreshToken.PreviousTokenExpirationDateTime = DateTime.UtcNow.AddMinutes(1);

            refreshToken.Token = Guid.NewGuid().ToString();
            refreshToken.TokenExpirationDateTime = DateTime.UtcNow.AddDays(7);

            await _context.SaveChangesAsync();
        }

        var response = new JwtResponse
        {
            Token = newJwt,
            Email = user.Email,
            FirstName = user.FirstName,
            LastName = user.LastName,
            RefreshToken = refreshToken.Token
        };

        return Ok(response);
    }
}