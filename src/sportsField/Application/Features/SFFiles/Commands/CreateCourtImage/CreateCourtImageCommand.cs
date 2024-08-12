using Amazon.Runtime.Internal;
using Application.Features.Courts.Rules;
using Application.Features.SFFiles.Constants;
using Application.Features.SFFiles.Rules;
using Application.Services.Courts;
using Application.Services.Repositories;
using Application.Services.Stroage;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.SFFiles.Commands.CreateCourtImage;
public class CreateCourtImageCommand: IRequest<CreatedCourtImageResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid CourtId { get; set; }
    public IList<IFormFile> FormFiles { get; set; }

    public string[] Roles => [SFFilesOperationClaims.Admin, SFFilesOperationClaims.Create];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourts"];

    public class CreateCourtImageCommandHandler: IRequestHandler<CreateCourtImageCommand, CreatedCourtImageResponse>
    {
        private readonly ICourtImageRepository _courtImageRepository;
        private readonly ICourtService _courtService;
        private readonly IStroageService _stroageService;
        private readonly SFFileBusinessRules _sFFileBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;

        public CreateCourtImageCommandHandler(ICourtImageRepository courtImageRepository, ICourtService courtService, IStroageService stroageService, SFFileBusinessRules sFFileBusinessRules, CourtBusinessRules courtBusinessRules)
        {
            _courtImageRepository = courtImageRepository;
            _courtService = courtService;
            _stroageService = stroageService;
            _sFFileBusinessRules = sFFileBusinessRules;
            _courtBusinessRules = courtBusinessRules;
        }

        public async Task<CreatedCourtImageResponse> Handle(CreateCourtImageCommand request, CancellationToken cancellationToken)
        {
            Court? court = await _courtService.GetAsync(c => c.Id == request.CourtId);
            await _courtBusinessRules.CourtShouldExistWhenSelected(court);
            await _courtBusinessRules.UserIdNotMatchedCourtUserId(court!.Id, request.UserId, SFFilesOperationClaims.Admin);

            await _sFFileBusinessRules.FileIsImageFiles(request.FormFiles);
            await _sFFileBusinessRules.FileShouldBeMaxCount(request.FormFiles, 8);

            ICollection<CourtImage> courtImageList = new List<CourtImage>();

            foreach (IFormFile item in request.FormFiles)
            {
                await _sFFileBusinessRules.FileSizeIsCorrect(item, 700, 700);
                Domain.Dtos.FileOptions fileOptions = await _stroageService.UploadFileAsync(item, "sf-court-images");

                CourtImage courtImage = await _courtImageRepository.AddAsync(new CourtImage
                {
                    Id = Guid.NewGuid(),
                    CourtId = court!.Id,
                    FileName = fileOptions.FileName,
                    FilePath = fileOptions.BucketName,
                    FileUrl = fileOptions.FileUrl,
                });

                courtImageList.Add(courtImage);
            }

            CreatedCourtImageResponse response = new() { CourtImages = courtImageList };
            return response;
        }
    }
}
