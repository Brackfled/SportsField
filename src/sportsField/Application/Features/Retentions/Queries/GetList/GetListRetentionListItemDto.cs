using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Retentions.Queries.GetList;

public class GetListRetentionListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Command { get; set; }
}