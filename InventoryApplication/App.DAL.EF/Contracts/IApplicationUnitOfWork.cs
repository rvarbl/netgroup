using App.DAL.EF.Contracts.Repositories;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts;

public interface IApplicationUnitOfWork : IUnitOfWork
{
    IStorageRepository Storages { get; }
    IStorageItemRepository StorageItems { get; }
    IItemAttributeRepository Attributes { get; }
    IAttributeInItemRepository AttributesInItem { get; }
}