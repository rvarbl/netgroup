using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IStorageRepository : IEntityRepository<Storage>
{
    public Task<List<Storage>> GetAllChildrenId(Guid parentId, bool noTracking = false);
    public IEnumerable<Storage> GetAllUserStorages(Guid uid, bool noTracking = false);
}