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
            var applicationDbContext = _context.StorageItems.Include(s => s.Storage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/StorageItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems
                .Include(s => s.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageItem == null)
            {
                return NotFound();
            }

            return View(storageItem);
        }

        // GET: Admin/StorageItem/Create
        public IActionResult Create()
        {
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName");
            return View();
        }

        // POST: Admin/StorageItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("StorageId,ItemName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] StorageItem storageItem)
        {
            if (ModelState.IsValid)
            {
                storageItem.Id = Guid.NewGuid();
                _context.Add(storageItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storageItem.StorageId);
            return View(storageItem);
        }

        // GET: Admin/StorageItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems.FindAsync(id);
            if (storageItem == null)
            {
                return NotFound();
            }
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storageItem.StorageId);
            return View(storageItem);
        }

        // POST: Admin/StorageItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("StorageId,ItemName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] StorageItem storageItem)
        {
            if (id != storageItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storageItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageItemExists(storageItem.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storageItem.StorageId);
            return View(storageItem);
        }

        // GET: Admin/StorageItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storageItem = await _context.StorageItems
                .Include(s => s.Storage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storageItem == null)
            {
                return NotFound();
            }

            return View(storageItem);
        }

        // POST: Admin/StorageItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var storageItem = await _context.StorageItems.FindAsync(id);
            _context.StorageItems.Remove(storageItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageItemExists(Guid id)
        {
            return _context.StorageItems.Any(e => e.Id == id);
        }
    }
}
