using NArchitecture.Core.Application.Responses;

namespace Application.Features.Attiributes.Commands.Create;

public class CreatedAttiributeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}