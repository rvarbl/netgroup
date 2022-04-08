using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Dto;

namespace WebApp.Api.Controllers.Files;

[Route("api/[controller]/[action]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class ImageController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly ILogger<ImageController> _logger;

    public ImageController(IConfiguration configuration, ILogger<ImageController> logger)
    {
        _configuration = configuration;
        _logger = logger;
    }

    // GET: api/Image
    [HttpGet]
    public IActionResult Index()
    {
        return Ok();
    }

    [HttpPost]
    public IActionResult Get([FromBody]ImageDto dto)
    {
        if (!System.IO.File.Exists(dto.path)) return BadRequest();
        
        var b = System.IO.File.ReadAllBytes(dto.path); // You can use your own method over here.         
        return File(b, "image/png");
    }

    //https://docs.microsoft.com/en-us/aspnet/core/mvc/models/file-uploads?view=aspnetcore-6.0
    [HttpPost]
    public async Task<IActionResult> Add(IFormFile file)
    {
        //validate file/image and the entity
        //add file to appointed path
        //if successful, add image attribute to the entity with the attribute value of file path.

        var filePath = _configuration["StoredFilesPath"];
        var size = file.Length;
        if (size is > 1 and < 5242880) //5MB
        {
            var fileName = Path.GetRandomFileName();
            var path = filePath + fileName + ".png";
            await using var stream = System.IO.File.Create(filePath + fileName + ".png");
            await file.CopyToAsync(stream);

            if (System.IO.File.Exists(path))
            {
                _logger.LogInformation(@"Uploaded file " + path);
                return Ok();
            }
        }

        return BadRequest();
    }

    [HttpDelete]
    public IActionResult Delete([FromBody]ImageDto dto)
    {
        if (System.IO.File.Exists(dto.path))
        {
            System.IO.File.Delete(dto.path);
        }

        if (!System.IO.File.Exists(dto.path))
        {
            return Ok();
        }

        return Problem();
    }
}