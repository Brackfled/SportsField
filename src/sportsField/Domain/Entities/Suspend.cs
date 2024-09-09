using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities;
public class Suspend: Entity<Guid>
{
    public Guid? UserId { get; set; }
    public DateTime SuspensionPeriod { get; set; }
    public string Reason { get; set; }

    public virtual User? User { get; set; }

    public Suspend()
    {
        Reason = string.Empty;
    }

    public Suspend(Guid? userId, DateTime suspensionPeriod, string reason, User? user)
    {
        UserId = userId;
        SuspensionPeriod = suspensionPeriod;
        Reason = reason;
        User = user;
    }
}
