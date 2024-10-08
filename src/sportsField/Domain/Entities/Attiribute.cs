﻿using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Attiribute: Entity<int>
{
    public string Name { get; set; }

    [JsonIgnore]
    public ICollection<Court>? Courts { get; set; }
}
