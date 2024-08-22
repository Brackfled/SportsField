using Domain.Entities;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Courts.Queries.GetListByUserId;
public class GetListByUserIdCourtListItemDto
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
    public int Price { get; set; }
    public string PhoneNumber { get; set; }
    public ICollection<Attiribute>? Attiributes { get; set; } = default!;
    public ICollection<CourtImage>? CourtImages { get; set; } = default!;
}
