using System.ComponentModel.DataAnnotations;
using App.Domain.Inventory;
using Base.Contracts.Domain;
using Base.Domain.Identity;

namespace App.Domain.Identity;

public class ApplicationUser : BaseUser, IDomainEntityId
{
    public ICollection<Storage>? UserStorages;
    public ICollection<RefreshToken>? RefreshTokens { get; set; }
    
    [MaxLength(128)] public string FirstName { get; set; } = default!;

    [MaxLength(128)] public string LastName { get; set; } = default!;
    
    public string FullName => FirstName + " " + LastName;
    
}