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
using WebApp.Areas.Models;

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
            return View(await _context.ItemAttributes.ToListAsync());
        }

        // GET: Admin/ItemAttribute/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var attribute = await _context.ItemAttributes
                .FirstOrDefaultAsync(x => x.Id == id);
            if (attribute == null) return NotFound();
            var viewModel = new ItemAttributeModel {ItemAttribute = attribute};
            return View(viewModel);
        }

        // GET: Admin/ItemAttribute/Create
        public IActionResult Create()
        {
            return View(new ItemAttributeModel());
        }

        // POST: Admin/ItemAttribute/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ItemAttribute itemAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Add(itemAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ItemAttributeModel {ItemAttribute = itemAttribute};
            return View(viewModel);
        }

        // GET: Admin/ItemAttribute/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var itemAttribute = await _context.ItemAttributes.FindAsync(id);
            if (itemAttribute == null) return NotFound();
            var viewModel = new ItemAttributeModel {ItemAttribute = itemAttribute};
            return View(viewModel);
        }

        // POST: Admin/ItemAttribute/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ItemAttribute itemAttribute)
        {
            if (ModelState.IsValid)
            {
                _context.Update(itemAttribute);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = new ItemAttributeModel {ItemAttribute = itemAttribute};
            return View(viewModel);
        }

        // GET: Admin/ItemAttribute/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var itemAttribute = await _context.ItemAttributes
                .FirstOrDefaultAsync(m => m.Id == id);
            if (itemAttribute == null) return NotFound();
            var viewModel = new ItemAttributeModel {ItemAttribute = itemAttribute};
            return View(viewModel);
        }

        // POST: Admin/ItemAttribute/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(ItemAttribute itemAttribute)
        {
            if (itemAttribute != null)
            {
                _context.ItemAttributes.Remove(itemAttribute);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}
