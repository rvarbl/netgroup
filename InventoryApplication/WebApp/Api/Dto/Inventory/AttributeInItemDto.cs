using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.Api.Dto.Inventory;

public class AttributeInItemDto: DomainEntityId
{
    public Guid AttributeId { get; set; }
    
    [StringLength(maximumLength:512, MinimumLength = 1, ErrorMessage = "Wrong length on Attribute Value.")]
    public string AttributeName { get; set; } = default!;
    public Guid ItemId { get; set; }
    

    
    [StringLength(maximumLength:512, MinimumLength = 1, ErrorMessage = "Wrong length on Attribute Value.")]
    public string AttributeValue { get; set; } = default!;
}