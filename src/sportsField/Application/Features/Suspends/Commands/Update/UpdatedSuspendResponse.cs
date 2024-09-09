using NArchitecture.Core.Application.Responses;

namespace Application.Features.Suspends.Commands.Update;

public class UpdatedSuspendResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public TimeSpan SuspensionPeriod { get; set; }
    public string Reason { get; set; }
}