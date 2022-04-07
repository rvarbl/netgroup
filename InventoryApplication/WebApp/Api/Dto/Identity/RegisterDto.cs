using System.ComponentModel.DataAnnotations;

namespace WebApp.Api.Dto.Identity;

public class RegisterDto
{
    [StringLength(maximumLength: 128, MinimumLength = 3, ErrorMessage = "Wrong length on email")]
    public string Email { get; set; } = default!;

    [StringLength(maximumLength: 128, MinimumLength = 3, ErrorMessage = "Wrong length on password")]
    public string Password { get; set; } = default!;

    [StringLength(maximumLength: 128, MinimumLength = 1, ErrorMessage = "Wrong length on firstname")]
    public string FirstName { get; set; } = default!;

    [StringLength(maximumLength: 128, MinimumLength = 1, ErrorMessage = "Wrong length on lastname")] 
    public string LastName { get; set; } = default!;
}