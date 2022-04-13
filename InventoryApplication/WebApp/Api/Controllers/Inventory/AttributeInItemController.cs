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
    public class AttributeInItemController : ControllerBase
    {
        private readonly IApplicationUnitOfWork _unitOfWork;

        public AttributeInItemController(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/AttributeInItem
        [HttpGet]
        public async Task<IEnumerable<AttributeInItemDto>> GetAttributeInItems()
        {
            var attributeInItems = await _unitOfWork.AttributesInItem
                .GetAllUsersItemAttributes(User.GetUserId());
            var response = attributeInItems.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/AttributeInItem/5
        [HttpGet("{id}")]
        public async Task<ActionResult<AttributeInItemDto>> GetAttributeInItem(Guid id)
        {
            var attributeInItems = await _unitOfWork.AttributesInItem
                .GetUsersItemAttributeById(id, User.GetUserId());

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

            var entity = await _unitOfWork.AttributesInItem.FirstOrDefaultAsync(dto.Id);
            if (entity == null) return BadRequest();

            entity.ItemAttributeId = dto.AttributeId;
            entity.StorageItemId = dto.ItemId;
            entity.AttributeValue = dto.AttributeValue;

            _unitOfWork.AttributesInItem.Update(entity);
            
            await _unitOfWork.SaveChangesAsync();

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

            _unitOfWork.AttributesInItem.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/AttributeInItem/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAttributeInItem(Guid id)
        {
            var attributeInItem = await _unitOfWork.AttributesInItem.FirstOrDefaultAsync(id);
            if (attributeInItem == null)
            {
                return NotFound();
            }

            _unitOfWork.AttributesInItem.Remove(attributeInItem);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        private bool AttributeInItemExists(Guid id)
        {
            return _unitOfWork.AttributesInItem.Exists(id);
        }

        public AttributeInItemDto MapToDto(AttributeInItem entity)
        {
            
            return new AttributeInItemDto
            {
                Id = entity.Id,
                AttributeId = entity.ItemAttributeId,
                ItemId = entity.StorageItemId,
                AttributeName = entity.ItemAttribute.AttributeName,
                AttributeValue = entity.AttributeValue
            };
        }

        public AttributeInItem MapToEntity(AttributeInItemDto dto)
        {
            var entity = new AttributeInItem();

            entity.Id = dto.Id;
            entity.ItemAttributeId = dto.AttributeId;
            entity.StorageItemId = dto.ItemId;
            entity.AttributeValue = dto.AttributeValue;
            return entity;
        }
    }
}