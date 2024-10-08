﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Queries.GetListByUserId;
public class GetListByUserIdCourtReservationListItemDto
{
    public Guid Id { get; set; }
    public Guid CourtId { get; set; }
    public string CourtName { get; set; }
    public string CourtDescription { get; set; }
    public string CourtLat {  get; set; }
    public string CourtLng { get; set; }
    public string CourtFormattedAddress { get; set; }
    public string CourtPhoneNumber { get; set; }
    public Guid? UserId { get; set; }
    public DateTime AvailableDate { get; set; }
    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }
    public long CreatedTime { get; set; }
    public bool IsActive { get; set; }
    public int Price { get; set; }
}
