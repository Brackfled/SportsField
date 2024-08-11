using NArchitecture.Core.Application.Dtos;

namespace Application.Features.Attiributes.Queries.GetList;

public class GetListAttiributeListItemDto : IDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}