using System.ComponentModel.DataAnnotations;
using Base.Contracts.Domain;

namespace Base.Domain;

public abstract class DomainEntityIdMeta : DomainEntityIdMeta<Guid>, IDomainEntityId
{
}

public abstract class DomainEntityIdMeta<TKey> : DomainEntityId<TKey>, IDomainEntityMeta where TKey : IEquatable<TKey>
{
    /*Implemented properties*/
    [MaxLength(255)] public string? CreatedBy { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    [MaxLength(255)] public string? UpdatedBy { get; set; }
    public DateTime? UpdatedAt { get; set; }

    [MaxLength(255)] public string? Comment { get; set; }
}