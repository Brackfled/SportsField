using Domain.Enums;
using NArchitecture.Core.Application.Responses;

namespace Application.Features.Users.Queries.GetById;

public class GetByIdUserResponse : IResponse
{
    public Guid Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public bool Status { get; set; }
    public UserState UserState { get; set; }

    public GetByIdUserResponse()
    {
        FirstName = string.Empty;
        LastName = string.Empty;
        Email = string.Empty;
    }

    public GetByIdUserResponse(Guid ýd, string firstName, string lastName, string email, bool status, UserState userState)
    {
        Id = ýd;
        FirstName = firstName;
        LastName = lastName;
        Email = email;
        Status = status;
        UserState = userState;
    }
}
