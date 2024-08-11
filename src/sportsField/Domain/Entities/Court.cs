using Domain.Enums;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Court: Entity<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public CourtType CourtType { get; set; }
    public string Description { get; set; }
    public bool IsActive { get; set; }
    public string Lat {  get; set; }
    public string Lng { get; set; }
    public string FormattedAddress { get; set; }
    
    public virtual User? User { get; set; }
    public ICollection<CourtImage>? CourtImages { get; set; } = default!;
    public ICollection<CourtReservation>? CourtReservations { get; set; } = default!;
    public ICollection<Attiribute>? Attiributes { get; set; } = default!;

    public Court()
    {
        Name = string.Empty;
        Description = string.Empty;
        IsActive = false;
        Lat = string.Empty;
        Lng = string.Empty;
        FormattedAddress = string.Empty;
    }

    public Court(Guid userId, string name, CourtType courtType, string description, bool ısActive, string lat, string lng, string formattedAddress, User? user, ICollection<CourtImage>? courtImages, ICollection<CourtReservation>? courtReservations, ICollection<Attiribute>? attiributes)
    {
        UserId = userId;
        Name = name;
        CourtType = courtType;
        Description = description;
        IsActive = ısActive;
        Lat = lat;
        Lng = lng;
        FormattedAddress = formattedAddress;
        User = user;
        CourtImages = courtImages;
        CourtReservations = courtReservations;
        Attiributes = attiributes;
    }
}
