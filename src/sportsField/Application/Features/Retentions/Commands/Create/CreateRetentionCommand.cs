using Application.Features.Retentions.Constants;
using Application.Features.Retentions.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using static Application.Features.Retentions.Constants.RetentionsOperationClaims;
using Domain.Dtos;
using System.Text.Json;

namespace Application.Features.Retentions.Commands.Create;

public class CreateRetentionCommand : IRequest<CreatedRetentionResponse>, ISecuredRequest, ITransactionalRequest
{
    public  Guid UserId { get; set; }
    public CreateRetentionCommandDto CreateRetentionCommandDto { get; set; }

    public string[] Roles => [Admin, RetentionsOperationClaims.Create];

    public class CreateRetentionCommandHandler : IRequestHandler<CreateRetentionCommand, CreatedRetentionResponse>
    {
        private readonly IMapper _mapper;
        private readonly IRetentionRepository _retentionRepository;
        private readonly RetentionBusinessRules _retentionBusinessRules;

        public CreateRetentionCommandHandler(IMapper mapper, IRetentionRepository retentionRepository,
                                         RetentionBusinessRules retentionBusinessRules)
        {
            _mapper = mapper;
            _retentionRepository = retentionRepository;
            _retentionBusinessRules = retentionBusinessRules;
        }

        public async Task<CreatedRetentionResponse> Handle(CreateRetentionCommand request, CancellationToken cancellationToken)
        {
            IList<string> days = new List<string>();
            foreach (DateTime item in request.CreateRetentionCommandDto.CreateCourtReservationCommandDto.ReservationDates)
            {
                string day = item.DayOfWeek.ToString();
                days.Add(day);
            }

            RetentionCommandDto retentionCommandDto = new()
            {
                CourtIds = request.CreateRetentionCommandDto.CreateCourtReservationCommandDto.CourtIds,
                ReservationDays = days,
                ReservationDetailDtos = request.CreateRetentionCommandDto.CreateCourtReservationCommandDto.ReservationDetailDtos,
            };

            string seriliazedCommand = JsonSerializer.Serialize(retentionCommandDto);

            Retention retention = new()
            {
                Id = Guid.NewGuid(),
                Name = request.CreateRetentionCommandDto.Name,
                Command = seriliazedCommand,
                UserId = request.UserId
            };

            Retention addedRetention = await _retentionRepository.AddAsync(retention);
            CreatedRetentionResponse response = _mapper.Map<CreatedRetentionResponse>(addedRetention);
            return response;
        }
    }
}