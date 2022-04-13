using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain.Inventory;

public class Storage : DomainEntityIdMeta
{
    public Guid ApplicationUserId { get; set; }
    public ApplicationUser ApplicationUser { get; set; } = default!;

    public Guid? StorageId { get; set; }
    public Storage? ParentStorage { get; set; }

    public ICollection<Storage>? ChildStorages { get; set; }

    public ICollection<StorageItem>? Items { get; set; }
    [MaxLength(128)] public string StorageName { get; set; } = default!;
}