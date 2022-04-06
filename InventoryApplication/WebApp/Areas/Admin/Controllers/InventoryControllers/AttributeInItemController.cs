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
    public class AttributeInItemController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AttributeInItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/AttributeInItem
        public async Task<IActionResult> Index()
        {
            var applicationDbContext = _context.ItemAttributes.Include(a => a.ItemAttribute).Include(a => a.StorageItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AttributeInItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeInItem = await _context.ItemAttributes
                .Include(a => a.ItemAttribute)
                .Include(a => a.StorageItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeInItem == null)
            {
                return NotFound();
            }

            return View(attributeInItem);
        }

        // GET: Admin/AttributeInItem/Create
        public IActionResult Create()
        {
            ViewData["ItemAttributeId"] = new SelectList(_context.Attributes, "Id", "AttributeName");
            ViewData["StorageItemId"] = new SelectList(_context.StorageItems, "Id", "ItemName");
            return View();
        }

        // POST: Admin/AttributeInItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ItemAttributeId,StorageItemId,AttributeValue,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] AttributeInItem attributeInItem)
        {
            if (ModelState.IsValid)
            {
                attributeInItem.Id = Guid.NewGuid();
                _context.Add(attributeInItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["ItemAttributeId"] = new SelectList(_context.Attributes, "Id", "AttributeName", attributeInItem.ItemAttributeId);
            ViewData["StorageItemId"] = new SelectList(_context.StorageItems, "Id", "ItemName", attributeInItem.StorageItemId);
            return View(attributeInItem);
        }

        // GET: Admin/AttributeInItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeInItem = await _context.ItemAttributes.FindAsync(id);
            if (attributeInItem == null)
            {
                return NotFound();
            }
            ViewData["ItemAttributeId"] = new SelectList(_context.Attributes, "Id", "AttributeName", attributeInItem.ItemAttributeId);
            ViewData["StorageItemId"] = new SelectList(_context.StorageItems, "Id", "ItemName", attributeInItem.StorageItemId);
            return View(attributeInItem);
        }

        // POST: Admin/AttributeInItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("ItemAttributeId,StorageItemId,AttributeValue,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] AttributeInItem attributeInItem)
        {
            if (id != attributeInItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(attributeInItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AttributeInItemExists(attributeInItem.Id))
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
            ViewData["ItemAttributeId"] = new SelectList(_context.Attributes, "Id", "AttributeName", attributeInItem.ItemAttributeId);
            ViewData["StorageItemId"] = new SelectList(_context.StorageItems, "Id", "ItemName", attributeInItem.StorageItemId);
            return View(attributeInItem);
        }

        // GET: Admin/AttributeInItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var attributeInItem = await _context.ItemAttributes
                .Include(a => a.ItemAttribute)
                .Include(a => a.StorageItem)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (attributeInItem == null)
            {
                return NotFound();
            }

            return View(attributeInItem);
        }

        // POST: Admin/AttributeInItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var attributeInItem = await _context.ItemAttributes.FindAsync(id);
            _context.ItemAttributes.Remove(attributeInItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AttributeInItemExists(Guid id)
        {
            return _context.ItemAttributes.Any(e => e.Id == id);
        }
    }
}
