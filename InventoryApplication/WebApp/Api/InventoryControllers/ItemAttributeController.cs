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
    public class ItemAttributeController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public ItemAttributeController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/ItemAttribute
        [HttpGet]
        public Task<IEnumerable<ItemAttributeDto>> GetItemAttributes()
        {
            var attributes = _context.ItemAttributes.ToList();
            var response = attributes.Select(x => MapToDto(x));
            return Task.FromResult(response);
        }

        // GET: api/ItemAttribute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemAttribute>> GetItemAttribute(Guid id)
        {
            var itemAttribute = await _context.ItemAttributes.FindAsync(id);

            if (itemAttribute == null)
            {
                return NotFound();
            }

            return itemAttribute;
        }

        // PUT: api/ItemAttribute/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutItemAttribute(Guid id, ItemAttributeDto dto)
        {
            if (id != dto.Id)
            {
                return BadRequest();
            }

            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();

            _context.ItemAttributes.Update(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/ItemAttribute
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemAttribute>> PostItemAttribute(ItemAttributeDto dto)
        {
            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();
            
            _context.ItemAttributes.Add(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ItemAttribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAttribute(Guid id)
        {
            var itemAttribute = await _context.ItemAttributes.FindAsync(id);
            if (itemAttribute == null)
            {
                return NotFound();
            }

            _context.ItemAttributes.Remove(itemAttribute);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        
        public ItemAttributeDto MapToDto(ItemAttribute entity)
        {
            return new ItemAttributeDto
            {
                AttributeName = entity.AttributeName
            };
        }

        public ItemAttribute MapToEntity(ItemAttributeDto dto)
        {
            var entity = new ItemAttribute
            {
                AttributeName = dto.AttributeName
            };

            return entity;
        }
    }
}