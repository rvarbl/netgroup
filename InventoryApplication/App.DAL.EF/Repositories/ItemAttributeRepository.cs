using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;

namespace App.DAL.EF.Repositories;

public class ItemAttributeRepository : BaseEntityRepository<ItemAttribute, ApplicationDbContext>,
    IItemAttributeRepository
{
    public ItemAttributeRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}