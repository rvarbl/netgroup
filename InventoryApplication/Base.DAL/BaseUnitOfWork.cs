using Base.Contracts.DAL;
using Microsoft.EntityFrameworkCore;

namespace Base.DAL;

public class BaseUnitOfWork<TDbContext> : IUnitOfWork
    where TDbContext : DbContext
{
    protected readonly TDbContext UowDbContext;

    protected BaseUnitOfWork(TDbContext dbContext)
    {
        UowDbContext = dbContext;
    }
    
    public int SaveChanges()
    {
        return UowDbContext.SaveChanges();
    }

    public async Task<int> SaveChangesAsync()
    {
        return await UowDbContext.SaveChangesAsync();
    }
}