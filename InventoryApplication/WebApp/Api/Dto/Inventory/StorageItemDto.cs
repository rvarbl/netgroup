using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.Api.Dto.Inventory;

public class StorageItemDto: DomainEntityId
{
    public Guid StorageId { get; set; }

    public IEnumerable<AttributeInItemDto>? ItemAttributes { get; set; }
    
    [StringLength(maximumLength:128, MinimumLength = 3, ErrorMessage = "Wrong length on Item Name")]
    public string ItemName { get; set; } = default!;
}