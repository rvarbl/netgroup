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
    public class ItemAttributeController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ItemAttributeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Admin/ItemAttribute
        public async Task<IActionResult> Index()
        {
            return View(await _context.Attributes.ToListAsync());
        }

        // GET: Admin/ItemAttribute/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemAttribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemAttribute == null)
            {
                return NotFound();
            }

            return View(itemAttribute);
        }

        // GET: Admin/ItemAttribute/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Admin/ItemAttribute/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("AttributeName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] ItemAttribute itemAttribute)
        {
            if (ModelState.IsValid)
            {
                itemAttribute.Id = Guid.NewGuid();
                _context.Add(itemAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(itemAttribute);
        }

        // GET: Admin/ItemAttribute/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemAttribute = await _context.Attributes.FindAsync(id);
            if (itemAttribute == null)
            {
                return NotFound();
            }
            return View(itemAttribute);
        }

        // POST: Admin/ItemAttribute/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(Guid id, [Bind("AttributeName,CreatedBy,CreatedAt,UpdatedBy,UpdatedAt,Comment,Id")] ItemAttribute itemAttribute)
        {
            if (id != itemAttribute.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(itemAttribute);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ItemAttributeExists(itemAttribute.Id))
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
            return View(itemAttribute);
        }

        // GET: Admin/ItemAttribute/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var itemAttribute = await _context.Attributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemAttribute == null)
            {
                return NotFound();
            }

            return View(itemAttribute);
        }

        // POST: Admin/ItemAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(Guid id)
        {
            var itemAttribute = await _context.Attributes.FindAsync(id);
            _context.Attributes.Remove(itemAttribute);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ItemAttributeExists(Guid id)
        {
            return _context.Attributes.Any(e => e.Id == id);
        }
    }
}
