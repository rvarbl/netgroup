using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IStorageItemRepository : IEntityRepository<StorageItem>
{
    public Task<List<StorageItem>> GetUserStorageItems(Guid uid, bool noTracking = false);
    public Task<StorageItem?> GetUserStorageItem(Guid uid, Guid itemId, bool noTracking = false);
}