namespace WebApp.Api.Dto.Identity;

public class JwtResponse
{
    public string Token { get; set; } = default!;
    public string RefreshToken { get; set; } = default!;

    public string Email { get; set; } = default!;
    
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    
    public List<string> Roles { get; set; } = new List<string> {"user"};
}