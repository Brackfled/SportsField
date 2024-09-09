using NArchitecture.Core.Application.Responses;

namespace Application.Features.Suspends.Queries.GetById;

public class GetByIdSuspendResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public TimeSpan SuspensionPeriod { get; set; }
    public string Reason { get; set; }
}