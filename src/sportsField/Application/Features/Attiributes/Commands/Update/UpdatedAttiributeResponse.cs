using NArchitecture.Core.Application.Responses;

namespace Application.Features.Attiributes.Commands.Update;

public class UpdatedAttiributeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}