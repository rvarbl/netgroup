using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IStorageItemRepository : IEntityRepository<StorageItem>
{
    public Task<List<StorageItem>> GetAllUserStorageItems(Guid uid, bool noTracking = false);
    public Task<StorageItem?> GetUserStorageItem(Guid itemId, Guid uid, bool noTracking = false);
    public Task<List<StorageItem>> GetItemsOwnedByStorage(Guid storageId, bool noTracking = false);
}