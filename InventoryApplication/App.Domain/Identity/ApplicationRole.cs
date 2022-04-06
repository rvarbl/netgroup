using Base.Domain.Identity;

namespace App.Domain.Identity;

public class ApplicationRole : BaseRole
{
    public string? Description { get; set; }
}