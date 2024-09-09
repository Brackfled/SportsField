using Application.Features.Suspends.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Suspends.Commands.Update;

public class UpdateSuspendCommand : IRequest<UpdatedSuspendResponse>
{
    public Guid Id { get; set; }
    public required Guid UserId { get; set; }
    public required TimeSpan SuspensionPeriod { get; set; }
    public required string Reason { get; set; }

    public class UpdateSuspendCommandHandler : IRequestHandler<UpdateSuspendCommand, UpdatedSuspendResponse>
    {
        private readonly IMapper _mapper;
        private readonly ISuspendRepository _suspendRepository;
        private readonly SuspendBusinessRules _suspendBusinessRules;

        public UpdateSuspendCommandHandler(IMapper mapper, ISuspendRepository suspendRepository,
                                         SuspendBusinessRules suspendBusinessRules)
        {
            _mapper = mapper;
            _suspendRepository = suspendRepository;
            _suspendBusinessRules = suspendBusinessRules;
        }

        public async Task<UpdatedSuspendResponse> Handle(UpdateSuspendCommand request, CancellationToken cancellationToken)
        {
            Suspend? suspend = await _suspendRepository.GetAsync(predicate: s => s.Id == request.Id, cancellationToken: cancellationToken);
            await _suspendBusinessRules.SuspendShouldExistWhenSelected(suspend);
            suspend = _mapper.Map(request, suspend);

            await _suspendRepository.UpdateAsync(suspend!);

            UpdatedSuspendResponse response = _mapper.Map<UpdatedSuspendResponse>(suspend);
            return response;
        }
    }
}