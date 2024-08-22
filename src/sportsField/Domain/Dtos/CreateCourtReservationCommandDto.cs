﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class CreateCourtReservationCommandDto
{
    public IList<Guid> CourtIds { get; set; }
    public IList<DateTime> ReservationDates { get; set; }
    public IList<string> ReservationTimes { get; set; }
    public int Price { get; set; }
}
