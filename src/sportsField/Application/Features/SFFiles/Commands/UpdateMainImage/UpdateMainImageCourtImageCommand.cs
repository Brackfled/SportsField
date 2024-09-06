using Amazon.Runtime.Internal;
using Application.Features.Courts.Rules;
using Application.Features.SFFiles.Constants;
using Application.Features.SFFiles.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using AutoMapper;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Commands.UpdateMainImage;
public class UpdateMainImageCourtImageCommand: IRequest<UpdateMainImageCourtImageResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid CourtId { get; set; }
    public Guid Id { get; set; }

    public string[] Roles => [SFFilesOperationClaims.Admin, SFFilesOperationClaims.Create];

    public bool BypassCache {  get;}

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourts"];

    public class UpdateMainImageCourtImageCommandHandler: IRequestHandler<UpdateMainImageCourtImageCommand, UpdateMainImageCourtImageResponse>
    {
        private readonly ICourtImageRepository _courtImageRepository;
        private readonly ICourtService _courtService;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly SFFileBusinessRules _sFFileBusinessRules;
        private IMapper _mapper;

        public UpdateMainImageCourtImageCommandHandler(ICourtImageRepository courtImageRepository, ICourtService courtService, CourtBusinessRules courtBusinessRules, SFFileBusinessRules sFFileBusinessRules, IMapper mapper)
        {
            _courtImageRepository = courtImageRepository;
            _courtService = courtService;
            _courtBusinessRules = courtBusinessRules;
            _sFFileBusinessRules = sFFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<UpdateMainImageCourtImageResponse> Handle(UpdateMainImageCourtImageCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtService.GetAsync(c => c.Id == request.CourtId);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, SFFilesOperationClaims.Admin);

            CourtImage? courtImage = await _courtImageRepository.GetAsync(ci => ci.Id == request.Id);
            await _sFFileBusinessRules.SFFileShouldExistWhenSelected(courtImage);

            ICollection<CourtImage> courtImages = await _courtImageRepository.GetAllAsync(ci => ci.CourtId == court!.Id && ci.IsMainImage == true);

            foreach (CourtImage item in courtImages)
            {
                item.IsMainImage = false;
                await _courtImageRepository.UpdateAsync(item);
            }

            courtImage!.IsMainImage = true;
            CourtImage updatedCourtImage = await _courtImageRepository.UpdateAsync(courtImage);
            UpdateMainImageCourtImageResponse response = _mapper.Map<UpdateMainImageCourtImageResponse>(updatedCourtImage);
            return response;
        }
    }
}
