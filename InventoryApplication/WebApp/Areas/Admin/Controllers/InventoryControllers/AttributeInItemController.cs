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
            var applicationDbContext = _context.AttributeInItems
                .Include(a => a.ItemAttribute)
                .Include(a => a.StorageItem);
            return View(await applicationDbContext.ToListAsync());
        }

        // GET: Admin/AttributeInItem/Details/5
        public async Task<IActionResult> Details(Guid? id)
        {
            if (id == null) return NotFound();
            var attributeInItem = await _context.AttributeInItems
                .Include(x => x.StorageItem)
                .Include(x => x.ItemAttribute)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (attributeInItem == null) return NotFound();
            var viewModel = new AttributeInItemModel {AttributeInItem = attributeInItem};
            return View(viewModel);
        }

        // GET: Admin/AttributeInItem/Create
        public async Task<IActionResult> Create()
        {
            var viewModel = new AttributeInItemModel
            {
                AttributeSelectList = new SelectList(
                    await _context.ItemAttributes
                        .OrderBy(x => x.AttributeName)
                        .Select(x => new {x.Id, x.AttributeName})
                        .ToListAsync(),
                    dataValueField: nameof(ItemAttribute.Id),
                    dataTextField: nameof(ItemAttribute.AttributeName)
                ),
                ItemSelectList = new SelectList(
                    await _context.StorageItems
                        .OrderBy(x => x.ItemName)
                        .Select(x => new {x.Id, x.ItemName})
                        .ToListAsync(),
                    dataValueField: nameof(StorageItem.Id),
                    dataTextField: nameof(StorageItem.ItemName)
                )
            };
            return View(viewModel);
        }

        // POST: Admin/AttributeInItem/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AttributeInItem attributeInItem)
        {
            var attribute = await _context.ItemAttributes
                .FirstOrDefaultAsync(x => x.Id == attributeInItem.ItemAttributeId);
            var item = await _context.StorageItems
                .Include(x => x.Storage.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == attributeInItem.StorageItemId);

            if (attribute != null && item != null)
            {
                attributeInItem.ItemAttribute = attribute;
                attributeInItem.StorageItem = item;
            }

            Console.WriteLine(attributeInItem.ItemAttribute + " ----------- " + attributeInItem.StorageItem);
            ModelState.ClearValidationState(nameof(attributeInItem));
            if (TryValidateModel(attributeInItem, nameof(attributeInItem)))
            {
                _context.Add(attributeInItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = SetUpViewModel(attributeInItem).Result;
            return View(viewModel);
        }

        // GET: Admin/AttributeInItem/Edit/5
        public async Task<IActionResult> Edit(Guid? id)
        {
            if (id == null) return NotFound();
            var attributeInItem = await _context.AttributeInItems.FindAsync(id);
            if (attributeInItem == null) return NotFound();
            var viewModel = SetUpViewModel(attributeInItem).Result;
            return View(viewModel);
        }

        // POST: Admin/AttributeInItem/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(AttributeInItem attributeInItem)
        {
            var attribute = await _context.ItemAttributes
                .FirstOrDefaultAsync(x => x.Id == attributeInItem.ItemAttributeId);
            var item = await _context.StorageItems
                .Include(x => x.Storage.ApplicationUser)
                .FirstOrDefaultAsync(x => x.Id == attributeInItem.StorageItemId);

            if (attribute != null && item != null)
            {
                attributeInItem.ItemAttribute = attribute;
                attributeInItem.StorageItem = item;
            }
            
            ModelState.ClearValidationState(nameof(attributeInItem));
            if (TryValidateModel(attributeInItem, nameof(attributeInItem)))
            {
                _context.Update(attributeInItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            var viewModel = SetUpViewModel(attributeInItem).Result;
            return View(viewModel);
        }

        // GET: Admin/AttributeInItem/Delete/5
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id == null) return NotFound();
            var attributeInItem = await _context.AttributeInItems
                .Include(x => x.ItemAttribute)
                .Include(x => x.StorageItem)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (attributeInItem == null) return NotFound();
            var viewModel = new AttributeInItemModel {AttributeInItem = attributeInItem};
            return View(viewModel);
        }

        // POST: Admin/AttributeInItem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(AttributeInItem attributeInItem)
        {
            if (attributeInItem == null)
            {
                return NotFound();
            }

            _context.AttributeInItems.Remove(attributeInItem);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        public async Task<AttributeInItemModel> SetUpViewModel(AttributeInItem attributeInItem)
        {
            var viewModel = new AttributeInItemModel
            {
                AttributeInItem = attributeInItem
            };
            viewModel.AttributeSelectList = new SelectList(
                await _context.ItemAttributes
                    .OrderBy(x => x.AttributeName)
                    .Select(x => new {x.Id, x.AttributeName})
                    .ToListAsync(),
                dataValueField: nameof(ItemAttribute.Id),
                dataTextField: nameof(ItemAttribute.AttributeName),
                viewModel.AttributeInItem.ItemAttributeId
            );
            viewModel.ItemSelectList = new SelectList(
                await _context.StorageItems
                    .OrderBy(x => x.ItemName)
                    .Select(x => new {x.Id, x.ItemName})
                    .ToListAsync(),
                dataValueField: nameof(StorageItem.Id),
                dataTextField: nameof(StorageItem.ItemName),
                viewModel.AttributeInItem.StorageItemId
            );
            return viewModel;
        }
    }
}