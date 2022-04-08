using Base.Contracts.DAL;
using Base.Contracts.Domain;
using Microsoft.EntityFrameworkCore;

//Base repository classes for Entity Framework
namespace Base.DAL;

public class BaseEntityRepository<TEntity, TDbContext> : BaseEntityRepository<TEntity, Guid, TDbContext>
    where TEntity : class, IDomainEntityId<Guid>
    where TDbContext : DbContext
{
    public BaseEntityRepository(TDbContext repositoryDbContext) : base(repositoryDbContext)
    {
    }
}

public class BaseEntityRepository<TEntity, TKey, TDbContext> : IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
    where TDbContext : DbContext
{
    protected readonly TDbContext RepositoryDbContext;
    protected readonly DbSet<TEntity> RepositoryDbSet;

    public BaseEntityRepository(TDbContext repositoryDbContext)
    {
        RepositoryDbContext = repositoryDbContext;
        RepositoryDbSet = repositoryDbContext.Set<TEntity>();
    }

    protected IQueryable<TEntity> CreateQuery(bool noTracking = true)
    {
        // TODO: entity ownership control

        var query = RepositoryDbSet.AsQueryable();
        if (noTracking)
        {
            query = query.AsNoTracking();
        }

        return query;
    }

    public bool Exists(TKey id)
    {
        return RepositoryDbSet.Local.Any(e => e.Id.Equals(id));
    }

    public bool Exists(TEntity entity)
    {
        return RepositoryDbSet.Local.Any(e => e.Equals(entity));
    }

    public IEnumerable<TEntity> GetAll(bool noTracking = true)
    {
        return CreateQuery(noTracking).ToList();
    }

    public TEntity? FirstOrDefault(TKey id, bool noTracking = true)
    {
        return CreateQuery(noTracking).FirstOrDefault(x => x.Id.Equals(id));
    }

    public TEntity Add(TEntity entity)
    {
        return RepositoryDbSet.Add(entity).Entity;
    }

    public TEntity Update(TEntity entity)
    {
        return RepositoryDbSet.Update(entity).Entity;
    }

    public TEntity Remove(TKey id)
    {
        var entity = FirstOrDefault(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }

        return RepositoryDbSet.Remove(entity).Entity;
    }

    public TEntity Remove(TEntity entity)
    {
        return RepositoryDbSet.Remove(entity).Entity;
    }

    public async Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true)
    {
        return await CreateQuery(noTracking).ToListAsync();
    }

    public async Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true)
    {
        return await CreateQuery(noTracking).FirstOrDefaultAsync(a => a.Id.Equals(id));
    }

    public async Task<bool> ExistsAsync(TKey id)
    {
        return await RepositoryDbSet.AnyAsync(a => a.Id.Equals(id));
    }

    public async Task<TEntity> RemoveAsync(TKey id)
    {
        var entity = await FirstOrDefaultAsync(id);
        if (entity == null)
        {
            throw new NullReferenceException($"Entity {typeof(TEntity).Name} with id {id} was not found");
        }
        return Remove(entity);

    }
}