#nullable disable
using App.DAL.EF;
using App.Domain.Inventory;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Dto.Inventory;

namespace WebApp.Api.Controllers.Inventory
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StorageItemController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StorageItemController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/StorageItem
        [HttpGet]
        public IEnumerable<StorageItemDto> GetStorageItems()
        {
            var storages = _context.StorageItems.ToList();
            var response = storages.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/StorageItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageItemDto>> GetStorageItem(Guid id)
        {
            var storageItem = await _context.StorageItems
                .Where(x=>x.Id == id)
                .Include(x => x.ItemAttributes)
                .FirstOrDefaultAsync();

            if (storageItem == null)
            {
                return NotFound();
            }

            return MapToDto(storageItem);
        }

        // PUT: api/StorageItem/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutStorageItem(Guid id, StorageItemDto dto)
        {
            if (id != dto.Id) return BadRequest();
            if (!StorageItemExists(id)) return NotFound();

            //TODO:Validate DTO.
            var entity = await _context.StorageItems
                .Where(x=>x.Id == id)
                .Include(x => x.ItemAttributes)
                .FirstOrDefaultAsync();
            if (entity == null) return BadRequest();

            entity.ItemName = dto.ItemName;
            entity.StorageId = dto.StorageId;

            _context.StorageItems.Update(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/StorageItem
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<StorageItem>> PostStorageItem(StorageItemDto dto)
        {
            //TODO:Validate DTO.

            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();

            _context.StorageItems.Add(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/StorageItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageItem(Guid id)
        {
            var entity = await _context.StorageItems
                .Where(x=>x.Id == id)
                .Include(x => x.ItemAttributes)
                .FirstOrDefaultAsync();
            if (entity == null) return BadRequest();
            // //TODO Fix this!!
            if (entity.ItemAttributes != null) _context.AttributeInItems.RemoveRange(entity.ItemAttributes);

            _context.StorageItems.Remove(entity);
            _context.SaveChanges();

            return Ok();
        }

        private bool StorageItemExists(Guid id)
        {
            return _context.StorageItems.Any(e => e.Id == id);
        }

        public StorageItemDto MapToDto(StorageItem entity)
        {
            return new StorageItemDto
            {
                Id = entity.Id,
                StorageId = entity.StorageId,
                ItemAttributes = entity.ItemAttributes?.Select(x => x.ItemAttributeId).ToList(),
                ItemName = entity.ItemName,
            };
        }

        public StorageItem MapToEntity(StorageItemDto dto)
        {
            return new StorageItem
            {
                StorageId = dto.StorageId,
                ItemAttributes = _context.AttributeInItems.Where(x => x.StorageItemId == dto.Id).ToList(),
                ItemName = dto.ItemName
            };
        }
    }
}