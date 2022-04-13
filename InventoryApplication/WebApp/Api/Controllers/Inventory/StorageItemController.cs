#nullable disable
using App.DAL.EF;
using App.DAL.EF.Contracts;
using App.Domain.Inventory;
using Base.Helpers.Identity;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApp.Api.Dto.Inventory;

namespace WebApp.Api.Controllers.Inventory
{
    [EnableCors]
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
        public async Task<IEnumerable<StorageItemDto>> GetStorageItems()
        {
            var storages = await _unitOfWork.StorageItems.GetAllUserStorageItems(User.GetUserId());
            var response = storages.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/StorageItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageItemDto>> GetStorageItem(Guid id)
        {
            var storageItem = await _unitOfWork.StorageItems.GetUserStorageItem(id, User.GetUserId());
            if (storageItem == null) return NotFound();
            
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
            var entity = await _unitOfWork.StorageItems.GetUserStorageItem(dto.Id, User.GetUserId());
            if (entity == null) return BadRequest();
            if (entity.Storage.ApplicationUserId != User.GetUserId()) return Unauthorized();
            
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
            var entity = await _unitOfWork.StorageItems.GetUserStorageItem(id, User.GetUserId());
            if (entity == null) return BadRequest();
            if (entity.Storage.ApplicationUserId != User.GetUserId()) return Unauthorized();

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
            var attributes = _unitOfWork.AttributesInItem.GetItemAttributesByItemId(entity.Id);
            return new StorageItemDto
            {
                Id = entity.Id,
                StorageId = entity.StorageId,
                ItemName = entity.ItemName,
                ItemAttributes = attributes.Select(x=>MapToDtoAttribute(x))
            };
        }

        public StorageItem MapToEntity(StorageItemDto dto)
        {
            return new StorageItem
            {
                StorageId = dto.StorageId,
                ItemName = dto.ItemName
            };
        }
        public AttributeInItemDto MapToDtoAttribute(AttributeInItem entity)
        {
            return new AttributeInItemDto
            {
                Id = entity.Id,
                AttributeId = entity.ItemAttributeId,
                AttributeName = entity.ItemAttribute.AttributeName,
                ItemId = entity.StorageItemId,
                AttributeValue = entity.AttributeValue
            };
        }

        public AttributeInItem MapToEntityAttribute(AttributeInItemDto dto)
        {
            var entity = new AttributeInItem();
            var attribute = _unitOfWork.Attributes.FirstOrDefaultAsync(dto.AttributeId).Result;
            var item = _unitOfWork.StorageItems.FirstOrDefaultAsync(dto.ItemId).Result;

            if (attribute == null || item == null) return null;

            entity.Id = dto.Id;
            entity.ItemAttribute = attribute;
            entity.StorageItem = item;
            entity.AttributeValue = dto.AttributeValue;
            return entity;
        }
    }
}