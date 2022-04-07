#nullable disable
using App.DAL.EF;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebApp.Api.Dto.Inventory;

namespace WebApp.Api.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
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
        public async Task<ActionResult<ItemAttributeDto>> GetItemAttribute(Guid id)
        {
            var itemAttribute = await _context.ItemAttributes.FindAsync(id);

            if (itemAttribute == null)
            {
                return NotFound();
            }

            return MapToDto(itemAttribute);
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

            var entity =  _context.ItemAttributes.FirstOrDefault(x => x.Id == dto.Id);;
            if (entity == null) return BadRequest();
            entity.AttributeName = dto.AttributeName;
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
                Id = entity.Id,
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