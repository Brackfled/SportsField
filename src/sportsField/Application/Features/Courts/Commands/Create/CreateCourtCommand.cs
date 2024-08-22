using Application.Features.Courts.Constants;
using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using MediatR;
using Domain.Enums;
using static Application.Features.Courts.Constants.CourtsOperationClaims;
using Domain.Dtos;
using Application.Services.Attiributes;

namespace Application.Features.Courts.Commands.Create;

public class CreateCourtCommand : IRequest<CreatedCourtResponse>, ISecuredRequest, ICacheRemoverRequest, ITransactionalRequest
{
    public required Guid UserId { get; set; }
    public CreateCourtCommandDto CreateCourtCommandDto { get; set; }

    public string[] Roles => [Admin, CourtsOperationClaims.Create];

    public bool BypassCache { get; }
    public string? CacheKey { get; }
    public string[]? CacheGroupKey => ["GetCourts"];

    public class CreateCourtCommandHandler : IRequestHandler<CreateCourtCommand, CreatedCourtResponse>
    {
        private readonly IMapper _mapper;
        private readonly ICourtRepository _courtRepository;
        private readonly IAttiributeService _attiributeService;
        private readonly CourtBusinessRules _courtBusinessRules;

        public CreateCourtCommandHandler(IMapper mapper, ICourtRepository courtRepository, IAttiributeService attiributeService, CourtBusinessRules courtBusinessRules)
        {
            _mapper = mapper;
            _courtRepository = courtRepository;
            _attiributeService = attiributeService;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<CreatedCourtResponse> Handle(CreateCourtCommand request, CancellationToken cancellationToken)
        {
            Court court = new()
            {
                Id = Guid.NewGuid(),
                UserId = request.UserId,
                CourtType = CourtType.Football,
                IsActive = false,
                Description = request.CreateCourtCommandDto.Description,
                Lat = request.CreateCourtCommandDto.Lat,
                Lng = request.CreateCourtCommandDto.Lng,
                FormattedAddress = request.CreateCourtCommandDto.FormattedAddress,
                Name = request.CreateCourtCommandDto.Name,
                Price = request.CreateCourtCommandDto.Price,
                PhoneNumber = request.CreateCourtCommandDto.PhoneNumber,
                Attiributes = new List<Attiribute>()
            };

            Court addedCourt = await _courtRepository.AddAsync(court);

            foreach (string item in request.CreateCourtCommandDto.AttiributeNames)
            {
                Attiribute? attiribute = await _attiributeService.GetAsync(a => a.Name == item);

                if(attiribute != null)
                    addedCourt!.Attiributes!.Add(attiribute);

                if(attiribute == null)
                {
                    Attiribute newAttiribute = new()
                    {
                        Name = item,
                        Courts = new List<Court>()
                    };

                    newAttiribute!.Courts!.Add(court);
                    Attiribute addedAttiribute = await _attiributeService.AddAsync(newAttiribute);

                    addedCourt!.Attiributes!.Add(addedAttiribute);
                }
            }

            Court updatedCourt = await _courtRepository.UpdateAsync(addedCourt);

            CreatedCourtResponse response = _mapper.Map<CreatedCourtResponse>(updatedCourt);
            return response;
        }
    }
}