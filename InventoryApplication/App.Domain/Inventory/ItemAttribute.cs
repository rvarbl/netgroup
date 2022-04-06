using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Inventory;

public class ItemAttribute : DomainEntityIdMeta
{
    public Guid AttributeId { get; set; }
    public Attribute Attribute { get; set; } = default!;

    public Guid StorageItemId { get; set; }
    public StorageItem StorageItem { get; set; } = default!;
    
    [MaxLength(512)]public string AttributeValue { get; set; } = default!;
}