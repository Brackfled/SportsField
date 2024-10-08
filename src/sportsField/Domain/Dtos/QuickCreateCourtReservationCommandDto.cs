﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class QuickCreateCourtReservationCommandDto
{
    public Guid CourtId { get; set; }
    public DateTime AvailableDate { get; set; }
    public string Times { get; set; }
    public bool IsActive { get; set; }
    public int Price { get; set; }
}
