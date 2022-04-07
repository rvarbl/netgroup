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
    public class StorageController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public StorageController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: api/Storage
        [HttpGet]
        public IEnumerable<StorageDto> GetStorages()
        {
            var storages = _context.Storages.ToList();
            var response = storages.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/Storage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageDto>> GetStorage(Guid id)
        {
            var storage = await _context.Storages.FindAsync(id);

            if (storage == null)
            {
                return NotFound();
            }

            return MapToDto(storage);
        }

        // PUT: api/Storage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStorage(StorageDto dto)
        {
            if (!StorageExists(dto.Id)) return NotFound();
            
            //TODO:Validate DTO.

            var entity = _context.Storages.FirstOrDefault(x => x.Id == dto.Id);
            if (entity == null) return BadRequest();
            
            entity.StorageName = dto.StorageName;
            entity.StorageId = dto.ParentStorageId;

            _context.Storages.Update(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Storage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Storage>> PostStorage(StorageDto dto)
        {
            //TODO:Validate DTO.

            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();
            
            _context.Storages.Add(entity);
            await _context.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Storage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage(Guid id)
        {
            var storage = await _context.Storages.FindAsync(id);
            if (storage == null)
            {
                return NotFound();
            }

            _context.Storages.Remove(storage);
            await _context.SaveChangesAsync();

            return Ok();
        }

        private bool StorageExists(Guid id)
        {
            return _context.Storages.Any(e => e.Id == id);
        }

        public StorageDto MapToDto(Storage entity)
        {
            return new StorageDto
            {
                Id = entity.Id,
                ApplicationUserId = entity.ApplicationUserId,
                ParentStorageId = entity.StorageId,
                ChildStorages = entity.ChildStorages?.Select(x => x.Id),
                StorageName = entity.StorageName
            };
        }

        public Storage MapToEntity(StorageDto dto)
        {
            var entity = new Storage();
            var user = _context.Users.FirstOrDefaultAsync(x => x.Id == dto.ApplicationUserId).Result;
            var parent = _context.Storages.FirstOrDefaultAsync(x => x.Id == dto.ParentStorageId).Result;
            var children = _context.Storages.Where(x => x.StorageId == dto.Id).ToList();

            if (user == null) return null;

            entity.ApplicationUser = user;
            entity.ParentStorage = parent;
            entity.ChildStorages = children;
            entity.StorageName = dto.StorageName;
            return entity;
        }
    }
}