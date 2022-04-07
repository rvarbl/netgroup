#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Identity;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using WebApp.Areas.Models;

namespace WebApp.Areas.Admin.Controllers.InventoryControllers
{
    [Area("Admin")]
    [Route("Admin/[controller]/[action]")]
    public class StorageController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StorageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/Storage
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.Storages
                .Include(s => s.ApplicationUser)
                .Include(s => s.ParentStorage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Storage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var storage = await _context.Storages
                .Include(x => x.ApplicationUser)
                .Include(x => x.ParentStorage)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storage == null) return NotFound();
            var viewModel = new StorageModel {Storage = storage};
            return View(viewModel);
        }

        // GET: Admin/Storage/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new StorageModel
            {
                ParentStorageSelectList = new SelectList(
                    await _context.Storages
                        .OrderBy(x => x.StorageName)
                        .Select(x => new {x.Id, x.StorageName})
                        .ToListAsync(),
                    dataValueField: nameof(Storage.Id),
                    dataTextField: nameof(Storage.StorageName)
                ),
                AppUserSelectList = new SelectList(
                    await _context.Users
                        .OrderBy(x => x.UserName)
                        .Select(x => new {x.Id, x.UserName})
                        .ToListAsync(),
                    dataValueField: nameof(ApplicationUser.Id),
                    dataTextField: nameof(ApplicationUser.UserName)
                )
            };
            return View(viewModel);
        }

        // POST: Admin/Storage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Storage storage)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == storage.ApplicationUserId);
            var parentStorage = await _context.Storages.FirstOrDefaultAsync(x => x.Id == storage.StorageId);

            if (user != null)
            {
                storage.ApplicationUser = user;
            }

            if (parentStorage != null)
            {
                storage.ParentStorage = parentStorage;
            }

            ModelState.ClearValidationState(nameof(storage));
            if (TryValidateModel(storage, nameof(storage)))
            {
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = SetUpViewModel(storage).Result;
            return View(viewModel);
        }

        // GET: Admin/Storage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var storage = await _context.Storages.FindAsync(id);
            if (storage == null) return NotFound();
            var viewModel = SetUpViewModel(storage).Result;
            return View(viewModel);
        }

        // POST: Admin/Storage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Storage storage)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == storage.ApplicationUserId);
            var parentStorage = await _context.Storages.FirstOrDefaultAsync(x => x.Id == storage.StorageId);
            if (user != null && parentStorage != null)
            {
                storage.ApplicationUser = user;
                storage.ParentStorage = parentStorage;
            }

            ModelState.ClearValidationState(nameof(storage));
            if (TryValidateModel(storage, nameof(storage)))
            {
                _context.Update(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = SetUpViewModel(storage).Result;
            return View(viewModel);
        }

        // GET: Admin/Storage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var storage = await _context.Storages
                .Include(x => x.ApplicationUser)
                .Include(x => x.ParentStorage)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (storage == null) return NotFound();
            var viewModel = new StorageModel {Storage = storage};
            return View(viewModel);
        }

        // POST: Admin/Storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Storage storage)
        {
            if (storage == null)
            {
                return NotFound();
            }

            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<StorageModel> SetUpViewModel(Storage storage)
        {
            var viewModel = new StorageModel
            {
                Storage = storage
            };
            viewModel.ParentStorageSelectList = new SelectList(
                await _context.Storages
                    .OrderBy(x => x.StorageName)
                    .Select(x => new {x.Id, x.StorageName})
                    .ToListAsync(),
                dataValueField: nameof(storage.Id),
                dataTextField: nameof(storage.StorageName),
                viewModel.Storage.StorageId
            );
            viewModel.AppUserSelectList = new SelectList(
                await _context.Users
                    .OrderBy(x => x.UserName)
                    .Select(x => new {x.Id, x.UserName})
                    .ToListAsync(),
                dataValueField: nameof(ApplicationUser.Id),
                dataTextField: nameof(ApplicationUser.UserName),
                viewModel.Storage.ApplicationUserId
            );
            return viewModel;
        }
    }
}