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
            var applicationDbContext = _context.Storages.Include(s => s.ApplicationUser).Include(s => s.ParentStorage);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/Storage/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.ApplicationUser)
                .Include(s => s.ParentStorage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // GET: Admin/Storage/Create
        public IActionResult Create()
        {
            ViewData["ApplicationUserId"] = new SelectList(_context.Users, "Id", "FirstName");
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName");
            return View();
        }

        // POST: Admin/Storage/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(
            [Bind("ApplicationUserId,StorageId,StorageName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")]
            Storage storage)
        {
            if (ModelState.IsValid)
            {
                storage.Id = Guid.NewGuid();
                _context.Add(storage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            ViewData["ApplicationUserId"] =
                new SelectList(_context.Users, "Id", "FirstName", storage.ApplicationUserId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storage.StorageId);
            return View(storage);
        }

        // GET: Admin/Storage/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }

            ViewData["ApplicationUserId"] =
                new SelectList(_context.Users, "Id", "FirstName", storage.ApplicationUserId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storage.StorageId);
            return View(storage);
        }

        // POST: Admin/Storage/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id,
            [Bind("ApplicationUserId,StorageId,StorageName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")]
            Storage storage)
        {
            if (id != storage.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(storage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StorageExists(storage.Id))
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

            ViewData["ApplicationUserId"] =
                new SelectList(_context.Users, "Id", "FirstName", storage.ApplicationUserId);
            ViewData["StorageId"] = new SelectList(_context.Storages, "Id", "StorageName", storage.StorageId);
            return View(storage);
        }

        // GET: Admin/Storage/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var storage = await _context.Storages
                .Include(s => s.ApplicationUser)
                .Include(s => s.ParentStorage)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (storage == null)
            {
                return NotFound();
            }

            return View(storage);
        }

        // POST: Admin/Storage/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var storage = await _context.Storages.FindAsync(id);
            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StorageExists(Guid id)
        {
            return _context.Storages.Any(e => e.Id == id);
        }
    }
}