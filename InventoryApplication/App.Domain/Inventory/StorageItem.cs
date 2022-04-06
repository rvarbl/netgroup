using System.ComponentModel.DataAnnotations;

namespace App.Domain.Inventory;

public class StorageItem
{
    public Guid StorageId { get; set; }
    public Storage Storage { get; set; } = default!;
    
    public ICollection<ItemAttribute>? ItemAttributes { get; set; }

    [MaxLength(128)] public string ItemName { get; set; } = default!;
}