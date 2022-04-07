using App.Domain.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Models;

public class AttributeInItemModel
{
    public AttributeInItem AttributeInItem { get; set; } = default!;
    public SelectList? AttributeSelectList { get; set; }
    public SelectList? ItemSelectList { get; set; }
}