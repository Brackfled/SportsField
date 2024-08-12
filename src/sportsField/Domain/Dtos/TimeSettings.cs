using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Dtos;
public class TimeSettings
{
    public int ResetPasswordTTL { get; set; }
    public int LastLoginTTL { get; set; }
    public int CompletePaymentTTL { get; set; }
}
