using App.Domain.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Models;

public class StorageItemModel
{
    public StorageItem StorageItem { get; set; } = default!;
    public SelectList? StorageLocationSelectList { get; set; }
}