using NArchitecture.Core.Application.Responses;

namespace Application.Features.Suspends.Commands.Create;

public class CreatedSuspendResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public TimeSpan SuspensionPeriod { get; set; }
    public string Reason { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UpdatedDate { get; set; }
}