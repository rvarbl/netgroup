﻿using Base.Contracts.Domain;

namespace Base.Contracts.DAL;

public interface IEntityRepository<TEntity> : IEntityRepository<TEntity, Guid>
    where TEntity : class, IDomainEntityId
{
}

public interface IEntityRepository<TEntity, TKey>
    where TEntity : class, IDomainEntityId<TKey>
    where TKey : IEquatable<TKey>
{
    bool Exists(TKey id);
    bool Exists(TEntity entity);
    
    IEnumerable<TEntity> GetAll(bool noTracking = true);
    TEntity? FirstOrDefault(TKey id, bool noTracking = true);
    TEntity Add(TEntity entity);
    TEntity Update(TEntity entity);
    TEntity Remove(TKey id);
    TEntity Remove(TEntity entity);
    
    Task<IEnumerable<TEntity>> GetAllAsync(bool noTracking = true);
    Task<TEntity?> FirstOrDefaultAsync(TKey id, bool noTracking = true);
    Task<bool> ExistsAsync(TKey id);
    Task<TEntity> RemoveAsync(TKey id);
}