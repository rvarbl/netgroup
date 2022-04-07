#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Areas.Models;

namespace WebApp.Areas.Admin.Controllers.InventoryControllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class StorageItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StorageItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/StorageItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.StorageItems
                .Include(s => s.Storage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/StorageItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var storageItem = await _context.StorageItems
                .Include(x => x.Storage)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storageItem == null) return NotFound();
            var viewModel = new StorageItemModel {StorageItem = storageItem};
            return View(viewModel);
        }

        // GET: Admin/StorageItem/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new StorageItemModel
            {
                StorageLocationSelectList = new SelectList(
                    await _context.Storages
                        .OrderBy(x => x.StorageName)
                        .Select(x => new {x.Id, x.StorageName})
                        .ToListAsync(),
                    dataValueField: nameof(Storage.Id),
                    dataTextField: nameof(Storage.StorageName)
                ),
            };
            return View(viewModel);
        }

        // POST: Admin/StorageItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(StorageItem storageItem)
        {
            var storage = await _context.Storages
                .Include(x => x.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == storageItem.StorageId);

            if (storage != null)
            {
                storageItem.Storage = storage;
            }

            ModelState.ClearValidationState(nameof(storageItem));
            if (TryValidateModel(storageItem, nameof(storageItem)))
            {
                _context.Add(storageItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            IEnumerable<ModelError> allErrors = ModelState.Values.SelectMany(v => v.Errors);
            foreach (var error in allErrors)
            {
                Console.WriteLine(error.ErrorMessage);
            }

            var viewModel = SetUpViewModel(storageItem).Result;
            return View(viewModel);
        }

        // GET: Admin/StorageItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var storageItem = await _context.StorageItems.FindAsync(id);
            if (storageItem == null) return NotFound();
            var viewModel = SetUpViewModel(storageItem).Result;
            return View(viewModel);
        }

        // POST: Admin/StorageItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("StorageId,ItemName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] StorageItem storageItem)
        {
            var storage = await _context.Storages.FirstOrDefaultAsync(x => x.Id == storageItem.StorageId);

            if (storage != null)
            {
                storageItem.Storage = storage;
            }

            ModelState.ClearValidationState(nameof(storageItem));
            if (TryValidateModel(storageItem, nameof(storageItem)))
            {
                _context.Update(storageItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = SetUpViewModel(storageItem).Result;
            return View(viewModel);
        }

        // GET: Admin/StorageItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var storageItem = await _context.StorageItems
                .Include(x => x.Storage)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storageItem == null) return NotFound();
            var viewModel = new StorageItemModel {StorageItem = storageItem};
            return View(viewModel);
        }

        // POST: Admin/StorageItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(StorageItem storageItem)
        {
            if (storageItem == null)
            {
                return NotFound();
            }

            _context.StorageItems.Remove(storageItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<StorageItemModel> SetUpViewModel(StorageItem storageItem)
        {
            var viewModel = new StorageItemModel
            {
                StorageItem = storageItem
            };
            viewModel.StorageLocationSelectList = new SelectList(
                await _context.Storages
                    .OrderBy(x => x.StorageName)
                    .Select(x => new {x.Id, x.StorageName})
                    .ToListAsync(),
                dataValueField: nameof(Storage.Id),
                dataTextField: nameof(Storage.StorageName),
                viewModel.StorageItem.StorageId
            );
            return viewModel;
        }
    }
}