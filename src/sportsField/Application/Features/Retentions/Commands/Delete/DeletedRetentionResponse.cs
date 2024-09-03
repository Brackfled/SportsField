using NArchitecture.Core.Application.Responses;

namespace Application.Features.Retentions.Commands.Delete;

public class DeletedRetentionResponse : IResponse
{
    public Guid Id { get; set; }
}