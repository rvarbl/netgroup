using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IAttributeInItemRepository: IEntityRepository<AttributeInItem>
{
    public IEnumerable<AttributeInItem> GetItemAttributesByItemId(Guid itemId, bool noTracking = false);
}