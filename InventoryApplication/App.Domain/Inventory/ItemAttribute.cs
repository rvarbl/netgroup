using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain.Inventory;

public class ItemAttribute : DomainEntityIdMeta
{
    public ICollection<Storage>? AttributeInItems { get; set; }

    [MaxLength(128)] public string AttributeName { get; set; } = default!;
}