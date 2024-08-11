using NArchitecture.Core.Application.Dtos;
using Domain.Enums;

namespace Application.Features.Courts.Queries.GetList;

public class GetListCourtListItemDto : IDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public CourtType CourtType { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public string Lat { get; set; }
    public string Lng { get; set; }
    public string FormattedAddress { get; set; }
}