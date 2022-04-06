using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Inventory;

public class StorageItem : DomainEntityIdMeta
{
    public Guid StorageId { get; set; }
    public Storage Storage { get; set; } = default!;

    public ICollection<AttributeInItem>? ItemAttributes { get; set; }

    [MaxLength(128)] public string ItemName { get; set; } = default!;
}