using NArchitecture.Core.Application.Responses;

namespace Application.Features.Courts.Commands.Delete;

public class DeletedCourtResponse : IResponse
{
    public Guid Id { get; set; }
}