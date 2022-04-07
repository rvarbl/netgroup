using App.Domain.Inventory;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WebApp.Areas.Models;

public class StorageModel
{
    public Storage Storage { get; set; } = default!;
    public SelectList? ParentStorageSelectList { get; set; }
    public SelectList? AppUserSelectList { get; set; }
}