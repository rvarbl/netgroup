using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace WebApp.Api.Dto;

public class ItemAttributeDto: DomainEntityId
{
    [MaxLength(128)] public string AttributeName { get; set; } = default!;
}