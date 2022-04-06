using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Inventory;

public class Attribute: DomainEntityIdMeta
{
    [MaxLength(128)]public string AttributeName { get; set; } = default!;
}