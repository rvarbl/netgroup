using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;

namespace App.DAL.EF.Repositories;

public class AttributeInItemRepository : BaseEntityRepository<AttributeInItem, ApplicationDbContext>,
    IAttributeInItemRepository
{
    public AttributeInItemRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}