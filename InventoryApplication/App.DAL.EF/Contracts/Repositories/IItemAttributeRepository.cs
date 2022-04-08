using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Contracts.DAL;

namespace App.DAL.EF.Contracts.Repositories;

public interface IItemAttributeRepository: IEntityRepository<ItemAttribute>
{
    public Task<ItemAttribute?> GetItemAttributeByName(string param, bool noTracking = false);
}