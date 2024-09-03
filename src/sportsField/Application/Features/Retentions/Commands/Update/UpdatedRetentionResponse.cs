using NArchitecture.Core.Application.Responses;

namespace Application.Features.Retentions.Commands.Update;

public class UpdatedRetentionResponse : IResponse
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public string Name { get; set; }
    public string Command { get; set; }
}