using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.Api.Dto.Inventory;

public class StorageDto: DomainEntityId
{
    public Guid ApplicationUserId { get; set; } = default!;
    public Guid? ParentStorageId { get; set; }
    public IEnumerable<Guid>? ChildStorages { get; set; }

    [StringLength(maximumLength:128, MinimumLength = 3, ErrorMessage = "Wrong length on Storage Name")]
    public string StorageName { get; set; } = default!;
}