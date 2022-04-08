#nullable disable
using App.DAL.EF;
using App.DAL.EF.Contracts;
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
        private readonly IApplicationUnitOfWork _unitOfWork;

        public StorageItemController(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/StorageItem
        [HttpGet]
        public IEnumerable<StorageItemDto> GetStorageItems()
        {
            var storages = _unitOfWork.StorageItems.GetAll();
            var response = storages.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/StorageItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageItemDto>> GetStorageItem(Guid id)
        {
            var storageItem = await _unitOfWork.StorageItems.FirstOrDefaultAsync(id);
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
            var entity = await _unitOfWork.StorageItems.FirstOrDefaultAsync(id);
            if (entity == null) return BadRequest();

            entity.ItemName = dto.ItemName;
            entity.StorageId = dto.StorageId;

            _unitOfWork.StorageItems.Update(entity);
            await _unitOfWork.SaveChangesAsync();

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

            _unitOfWork.StorageItems.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/StorageItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorageItem(Guid id)
        {
            var entity = await _unitOfWork.StorageItems.FirstOrDefaultAsync(id);
            if (entity == null) return BadRequest();
            // //TODO Fix this!!
            // if (entity.ItemAttributes != null) _unitOfWork.Attributes.RemoveRange(entity.ItemAttributes);

            _unitOfWork.StorageItems.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        private bool StorageItemExists(Guid id)
        {
            return _unitOfWork.StorageItems.Exists(id);
        }

        public StorageItemDto MapToDto(StorageItem entity)
        {
            return new StorageItemDto
            {
                Id = entity.Id,
                StorageId = entity.StorageId,
                ItemAttributes = entity.AttributesInItem?.Select(x => x.ItemAttributeId).ToList(),
                ItemName = entity.ItemName,
            };
        }

        public StorageItem MapToEntity(StorageItemDto dto)
        {
            var attributes = _unitOfWork.AttributesInItem.GetAll()
                .Where(x => x.StorageItemId == dto.Id);
            
            return new StorageItem
            {
                StorageId = dto.StorageId,
                AttributesInItem = attributes,
                ItemName = dto.ItemName
            };
        }
    }
}