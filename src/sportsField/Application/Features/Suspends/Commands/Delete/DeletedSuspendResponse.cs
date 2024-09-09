using NArchitecture.Core.Application.Responses;

namespace Application.Features.Suspends.Commands.Delete;

public class DeletedSuspendResponse : IResponse
{
    public Guid Id { get; set; }
}