using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Retention: Entity<Guid>
{
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Command { get; set; }

    public virtual User? User { get; set; }

    public Retention()
    {
        Name = string.Empty;
        Command = string.Empty;
    }

    public Retention(Guid userId, string name, string command)
    {
        UserId = userId;
        Name = name;
        Command = command;
    }
}
