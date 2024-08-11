using NArchitecture.Core.Application.Responses;

namespace Application.Features.Attiributes.Queries.GetById;

public class GetByIdAttiributeResponse : IResponse
{
    public int Id { get; set; }
    public string Name { get; set; }
}