using NArchitecture.Core.Application.Responses;

namespace Application.Features.Attiributes.Commands.Delete;

public class DeletedAttiributeResponse : IResponse
{
    public int Id { get; set; }
}