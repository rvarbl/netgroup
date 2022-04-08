#nullable disable
using App.DAL.EF;
using App.DAL.EF.Contracts;
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
        private readonly IApplicationUnitOfWork _unitOfWork;

        public ItemAttributeController(IApplicationUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        // GET: api/ItemAttribute
        [HttpGet]
        public async Task<IEnumerable<ItemAttributeDto>> GetItemAttributes()
        {
            var attributes = await _unitOfWork.Attributes.GetAllAsync();
            var response = attributes.Select(x => MapToDto(x));
            return response;
        }

        // GET: api/ItemAttribute/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ItemAttributeDto>> GetItemAttribute(Guid id)
        {
            var itemAttribute = await _unitOfWork.Attributes.FirstOrDefaultAsync(id);

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

            var entity =  await _unitOfWork.Attributes.FirstOrDefaultAsync(dto.Id);
            if (entity == null) return BadRequest();
            
            entity.AttributeName = dto.AttributeName;
            _unitOfWork.Attributes.Update(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // POST: api/ItemAttribute
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ItemAttribute>> PostItemAttribute(ItemAttributeDto dto)
        {
            var entity = MapToEntity(dto);
            if (entity == null) return BadRequest();
            
            _unitOfWork.Attributes.Add(entity);
            await _unitOfWork.SaveChangesAsync();

            return Ok();
        }

        // DELETE: api/ItemAttribute/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteItemAttribute(Guid id)
        {
            var itemAttribute = await _unitOfWork.Attributes.FirstOrDefaultAsync(id);
            if (itemAttribute == null)
            {
                return NotFound();
            }

            _unitOfWork.Attributes.Remove(itemAttribute);
            await _unitOfWork.SaveChangesAsync();

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
                Id = dto.Id,
                AttributeName = dto.AttributeName
            };

            return entity;
        }
    }
}