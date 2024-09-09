using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Suspends.Queries.GetList;

public class GetListSuspendListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public TimeSpan SuspensionPeriod { get; set; }
    public string Reason { get; set; }
}