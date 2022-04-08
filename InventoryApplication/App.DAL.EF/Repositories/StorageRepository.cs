using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;

namespace App.DAL.EF.Repositories;

public class StorageRepository:BaseEntityRepository<Storage,ApplicationDbContext>, IStorageRepository
{
    public StorageRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public IEnumerable<Storage> GetAllChildrenId(Guid parentId, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.StorageId == parentId).ToList();
    } 
}