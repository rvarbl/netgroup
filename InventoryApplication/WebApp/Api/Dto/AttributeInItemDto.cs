using System.ComponentModel.DataAnnotations;
using App.Domain.Inventory;
using Base.Domain;

namespace WebApp.Api.Dto;

public class AttributeInItemDto: DomainEntityId
{
    public Guid AttributeId { get; set; }
    public Guid ItemId { get; set; }
    
    [MaxLength(512)]
    public string AttributeValue { get; set; } = default!;
}