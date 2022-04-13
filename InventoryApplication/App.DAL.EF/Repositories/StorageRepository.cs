using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StorageRepository:BaseEntityRepository<Storage,ApplicationDbContext>, IStorageRepository
{
    public StorageRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
    
    public Task<List<Storage>> GetAllChildrenId(Guid parentId, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.StorageId == parentId).ToListAsync();
    }

    public IEnumerable<Storage> GetAllUserStorages(Guid uid, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.ApplicationUserId.Equals(uid)).ToList();
    }

    public Task<Storage?> GetStorageWithChildren(Guid storageId, Guid uid, bool noTracking = false)
    {
        return CreateQuery(noTracking)
            .Where(x => x.ApplicationUserId.Equals(uid) && x.Id.Equals(storageId))
            .Include(x => x.ChildStorages)
            .Include(x => x.Items)
            .FirstOrDefaultAsync();

    }
}