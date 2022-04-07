using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.Api.Dto.Inventory;

public class ItemAttributeDto: DomainEntityId
{
    [StringLength(maximumLength:128, MinimumLength = 3, ErrorMessage = "Wrong length Attribute name.")]
    public string AttributeName { get; set; } = default!;
}