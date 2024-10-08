﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class CourtImage: SFFile
{
    public Guid CourtId { get; set; }
    public bool IsMainImage { get; set; }

    [JsonIgnore]
    public virtual Court? Court { get; set; }
}
