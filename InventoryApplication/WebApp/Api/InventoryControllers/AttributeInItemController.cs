#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using App.DAL.EF;
using App.Domain.Inventory;
using WebApp.Api.Dto;

namespace WebApp.Api.InventoryControllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AttributeInItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public AttributeInItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/AttributeInItem
        [HttpGet]
        public IEnumerable<AttributeInItemDto>GetAttributeInItems()
        {
            var attributeInItems = _context.AttributeInItems.ToList();
            var response = attributeInItems.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/AttributeInItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeInItemDto>> GetAttributeInItem(Guid id)
        {
            var attributeInItems = await _context.AttributeInItems.FindAsync(id);

            if (attributeInItems == null)
            {
                return NotFound();
            }

            return MapToDto(attributeInItems);
        }

        // PUT: api/AttributeInItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutAttributeInItem(Guid id, AttributeInItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!AttributeInItemExists(id)) return NotFound();
            
            //TODO:Validate DTO.

            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();
            
            _context.AttributeInItems.Update(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/AttributeInItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<AttributeInItem>> PostAttributeInItem(AttributeInItemDto dto)
        {
            //TODO:Validate DTO.

            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();
            
            _context.AttributeInItems.Add(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/AttributeInItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeInItem(Guid id)
        {
            var attributeInItem = await _context.AttributeInItems.FindAsync(id);
            if (attributeInItem == null)
            {
                return NotFound();
            }

            _context.AttributeInItems.Remove(attributeInItem);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool AttributeInItemExists(Guid id)
        {
            return _context.AttributeInItems.Any(e => e.Id == id);
        }

        public AttributeInItemDto MapToDto(AttributeInItem entity)
        {
            return new AttributeInItemDto
            {
                AttributeId = entity.ItemAttributeId,
                ItemId = entity.StorageItemId,
                AttributeValue = entity.AttributeValue
            };
        }

        public AttributeInItem MapToEntity(AttributeInItemDto dto)
        {
            var entity = new AttributeInItem();
            var attribute = _context.ItemAttributes.FirstOrDefaultAsync(x => x.Id == dto.AttributeId).Result;
            var item = _context.StorageItems.FirstOrDefaultAsync(x => x.Id == dto.ItemId).Result;

            if (attribute == null || item == null) return null;

            entity.ItemAttribute = attribute;
            entity.StorageItem = item;
            entity.AttributeValue = dto.AttributeValue;
            return entity;
        }
    }
}