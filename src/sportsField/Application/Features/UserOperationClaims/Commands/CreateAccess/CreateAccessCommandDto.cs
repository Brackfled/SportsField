using Domain.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateAccess;
public class CreateAccessCommandDto
{
    public Guid UserId { get; set; }
    public IList<int> ClaimIds { get; set; }
    public UserState UserState { get; set; }
}
