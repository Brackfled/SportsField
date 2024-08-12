using Amazon.Runtime.Internal;
using Amazon.S3.Model;
using Application.Features.Courts.Rules;
using Application.Features.SFFiles.Constants;
using Application.Features.SFFiles.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using Application.Services.Stroage;
using AutoMapper;
using Domain.Dtos;
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

namespace Application.Features.SFFiles.Commands.DeleteCourtImage;
public class DeleteCourtImageCommand: IRequest<DeletedCourtImageResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    public string[] Roles => [SFFilesOperationClaims.Admin, SFFilesOperationClaims.Delete];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourts"];

    public class DeleteCourtImageCommandHandler: IRequestHandler<DeleteCourtImageCommand, DeletedCourtImageResponse>
    {
        private readonly ICourtService _courtService;
        private readonly ICourtImageRepository _courtImageRepository;
        private readonly IStroageService _stroageService;
        private readonly CourtBusinessRules _courtBusinessRules;
        private readonly SFFileBusinessRules _sfFileBusinessRules;
        private IMapper _mapper;

        public DeleteCourtImageCommandHandler(ICourtService courtService, ICourtImageRepository courtImageRepository, IStroageService stroageService, CourtBusinessRules courtBusinessRules, SFFileBusinessRules sfFileBusinessRules, IMapper mapper)
        {
            _courtService = courtService;
            _courtImageRepository = courtImageRepository;
            _stroageService = stroageService;
            _courtBusinessRules = courtBusinessRules;
            _sfFileBusinessRules = sfFileBusinessRules;
            _mapper = mapper;
        }

        public async Task<DeletedCourtImageResponse> Handle(DeleteCourtImageCommand request, CancellationToken cancellationToken)
        {
            CourtImage? courtImage = await _courtImageRepository.GetAsync(ci => ci.Id == request.Id);
            await _sfFileBusinessRules.SFFileShouldExistWhenSelected(courtImage);

            await _courtBusinessRules.UserIdNotMatchedCourtUserId(courtImage!.CourtId, request.UserId, SFFilesOperationClaims.Admin);

            await _stroageService.DeleteFileAsync(courtImage.FilePath, courtImage.FileName);
            CourtImage deletedCourtImage = await _courtImageRepository.DeleteAsync(courtImage, true);

            DeletedCourtImageResponse response = _mapper.Map<DeletedCourtImageResponse>(deletedCourtImage);
            return response;
        }
    }
}
