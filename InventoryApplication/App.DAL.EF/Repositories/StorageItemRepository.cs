using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class StorageItemRepository : BaseEntityRepository<StorageItem, ApplicationDbContext>, IStorageItemRepository
{
    public StorageItemRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public Task<List<StorageItem>> GetAllUserStorageItems(Guid uid, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.Storage.ApplicationUserId.Equals(uid)).ToListAsync();
    }

    public Task<StorageItem?> GetUserStorageItem(Guid uid, Guid itemId, bool noTracking = false)
    {
        return CreateQuery(noTracking)
            .Where(x => x.Storage.ApplicationUserId.Equals(uid) && x.Id.Equals(itemId))
            .Include(x => x.Storage)
            .FirstOrDefaultAsync();
    }

    public Task<List<StorageItem>> GetItemsOwnedByStorage(Guid storageId, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.Storage.Id.Equals(storageId)).ToListAsync();
    }
}