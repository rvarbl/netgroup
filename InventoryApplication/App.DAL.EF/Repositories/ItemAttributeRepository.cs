using App.DAL.EF.Contracts.Repositories;
using App.Domain.Inventory;
using Base.Contracts.DAL;
using Base.DAL;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class ItemAttributeRepository : BaseEntityRepository<ItemAttribute, ApplicationDbContext>,
    IItemAttributeRepository
{
    public ItemAttributeRepository(ApplicationDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }

    public Task<ItemAttribute?> GetItemAttributeByName(string param, bool noTracking = false)
    {
        return CreateQuery(noTracking).Where(x => x.AttributeName.Equals(param)).FirstOrDefaultAsync();
    }
}