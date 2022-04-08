using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IStorageItemRepository: IEntityRepository<StorageItem>
{
    
}