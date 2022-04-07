using System.ComponentModel.DataAnnotations;
using App.Domain.Inventory;
using Base.Domain;

namespace WebApp.Api.Dto;

public class StorageItemDto: DomainEntityId
{
    public Guid StorageId { get; set; }

    public IEnumerable<Guid>? ItemAttributes { get; set; }
    
    [MaxLength(128)] 
    public string ItemName { get; set; } = default!;
}