using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using App.Domain.Inventory;
using Base.Domain;

namespace WebApp.Api.Dto;

public class StorageDto: DomainEntityId
{
    public Guid ApplicationUserId { get; set; } = default!;
    public Guid? ParentStorageId { get; set; }
    public IEnumerable<Guid>? ChildStorages { get; set; }

    [MaxLength(128)] 
    public string StorageName { get; set; } = default!;
}