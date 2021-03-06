#nullable disable
using App.DAL.EF;
using App.DAL.EF.Contracts;
using App.Domain.Identity;
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
    public class StorageController : ControllerBase
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public StorageController(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/Storage
        [HttpGet]
        public IEnumerable<StorageDto> GetStorages()
        {
            var id = User.GetUserId();
            //GET ALL USER STORAGES
            var storages = _unitOfWork.Storages.GetAllUserStorages(User.GetUserId());
            var response = storages.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/Storage/5
        [HttpGet("{id}")]
        public async Task<ActionResult<StorageDto>> GetStorage(Guid id)
        {
            var entity = await _unitOfWork.Storages.FirstOrDefaultAsync(id);

            if (entity == null) return NotFound();
            if (entity.ApplicationUserId != User.GetUserId()) return Unauthorized();

            return MapToDto(entity);
        }

        // PUT: api/Storage/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut]
        public async Task<IActionResult> PutStorage(StorageDto dto)
        {
            if (!StorageExists(dto.Id)) return NotFound();

            //TODO:Validate DTO.

            var entity = await _unitOfWork.Storages.FirstOrDefaultAsync(dto.Id);
            if (entity == null) return BadRequest();
            if (entity.ApplicationUserId != User.GetUserId()) return Unauthorized();

            entity.StorageName = dto.StorageName;
            if (entity.StorageId == dto.ParentStorageId) return BadRequest();

            entity.StorageId = dto.ParentStorageId;

            _unitOfWork.Storages.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // POST: api/Storage
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Storage>> PostStorage(StorageDto dto)
        {
            //TODO:Validate DTO.

            var entity = await MapToEntity(dto);
            if (entity == null) return BadRequest();

            _unitOfWork.Storages.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/Storage/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteStorage(Guid id)
        {
            var entity = await _unitOfWork.Storages.GetStorageWithChildren(id, User.GetUserId());
            if (entity == null) return NotFound();
            if (entity.ApplicationUserId != User.GetUserId()) return Unauthorized();

            _unitOfWork.Storages.Remove(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        private bool StorageExists(Guid id)
        {
            return _unitOfWork.Storages.Exists(id);
        }

        public StorageDto MapToDto(Storage entity)
        {
            var children = _unitOfWork.Storages.GetAllChildrenId(entity.Id).Result;
            var items = _unitOfWork.StorageItems.GetItemsOwnedByStorage(entity.Id).Result;
            return new StorageDto
            {
                Id = entity.Id,
                ApplicationUserId = entity.ApplicationUserId,
                ParentStorageId = entity.StorageId,
                ChildStorages = children.Select(x => MapToDto(x)),
                StorageItems = items.Select(x => MapToDtoStorageItem(x)),
                StorageName = entity.StorageName
            };
        }

        public async Task<Storage> MapToEntity(StorageDto dto)
        {
            var entity = new Storage();
            var children = await _unitOfWork.Storages.GetAllChildrenId(dto.Id);

            entity.ApplicationUserId = User.GetUserId();
            entity.StorageId = dto.ParentStorageId;
            entity.ChildStorages = children.ToList();
            entity.StorageName = dto.StorageName;
            return entity;
        }

        public StorageItemDto MapToDtoStorageItem(StorageItem entity)
        {
            return new StorageItemDto
            {
                Id = entity.Id,
                StorageId = entity.StorageId,
                ItemName = entity.ItemName,
            };
        }

        public StorageItem MapToEntityStorageItem(StorageItemDto dto)
        {
            var attributes = _unitOfWork.AttributesInItem.GetItemAttributesByItemId(dto.Id);
            return new StorageItem
            {
                StorageId = dto.StorageId,
                AttributesInItem = attributes,
                ItemName = dto.ItemName
            };
        }
    }
}