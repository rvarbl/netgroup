using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;

namespace App.DAL.EF.Repositories;

public class StorageItemRepository : BaseEntityRepository<StorageItem, ApplicationDbContext>, IStorageItemRepository
{
    public StorageItemRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}