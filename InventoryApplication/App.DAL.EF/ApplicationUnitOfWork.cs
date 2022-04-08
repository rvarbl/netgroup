using App.DAL.EF.Contracts;
using App.DAL.EF.Contracts.Repositories;
using App.DAL.EF.Repositories;
using Base.DAL;

namespace App.DAL.EF;

//UserStore invokes SaveChanges in nearly every method call by default, which makes it easy to prematurely commit a unit of work
public class ApplicationUnitOfWork : BaseUnitOfWork<ApplicationDbContext>, IApplicationUnitOfWork
{
    private IStorageRepository? _storages;
    private IStorageItemRepository? _storageItems;
    private IItemAttributeRepository? _attributes;
    private IAttributeInItemRepository? _attributesInItem;

    public ApplicationUnitOfWork(ApplicationDbContext dbContext) : base(dbContext)
    {
    }

    //TODO: factory
    public IStorageRepository Storages => _storages ??= new StorageRepository(UowDbContext);
    public IStorageItemRepository StorageItems => _storageItems ??= new StorageItemRepository(UowDbContext);
    public IItemAttributeRepository Attributes => _attributes ??= new ItemAttributeRepository(UowDbContext);

    public IAttributeInItemRepository AttributesInItem =>
        _attributesInItem ??= new AttributeInItemRepository(UowDbContext);
}