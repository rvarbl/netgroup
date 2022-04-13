using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class AttributeInItemRepository : BaseEntityRepository<AttributeInItem, ApplicationDbContext>,
    IAttributeInItemRepository
{
    public AttributeInItemRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public IEnumerable<AttributeInItem> GetItemAttributesByItemId(Guid itemId, bool noTracking = false)
    {
        return CreateQuery(noTracking)
            .Where(x => x.StorageItemId == itemId)
            .Include(x => x.ItemAttribute)
            .ToList();
    }

    public Task<AttributeInItem?> GetUsersItemAttributeById(Guid id, Guid uid, bool noTracking = false)
    {
        return CreateQuery(noTracking)
            .Include(x => x.StorageItem.Storage)
            .Include(x => x.ItemAttribute)
            .Where(x => x.Id == id && x.StorageItem.Storage.ApplicationUserId == uid)
            .FirstOrDefaultAsync();
    }

    public Task<List<AttributeInItem>> GetAllUsersItemAttributes(Guid uid, bool noTracking = false)
    {
        return CreateQuery(noTracking)
            .Include(x => x.StorageItem.Storage)
            .Where(x => x.StorageItem.Storage.ApplicationUserId == uid)
            .ToListAsync();
    }
}