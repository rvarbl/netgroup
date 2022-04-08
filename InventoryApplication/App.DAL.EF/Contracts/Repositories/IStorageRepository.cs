using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IStorageRepository: IEntityRepository<Storage>
{
    public IEnumerable<Storage> GetAllChildrenId(Guid parentId, bool noTracking = false);

}