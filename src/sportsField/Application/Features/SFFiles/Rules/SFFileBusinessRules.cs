using Application.Features.SFFiles.Constants;
using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using NArchitecture.Core.Application.Rules;
using NArchitecture.Core.CrossCuttingConcerns.Exception.Types;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using System.Drawing;

namespace Application.Features.SFFiles.Rules;
public class SFFileBusinessRules: BaseBusinessRules
{
    private readonly ICourtImageRepository _courtImageRepository;

    public SFFileBusinessRules(ICourtImageRepository courtImageRepository)
    {
        _courtImageRepository = courtImageRepository;
    }

    public async Task SFFileShouldExistWhenSelected(SFFile? sFFile)
    {
        if (sFFile == null)
            throw new BusinessException(SFFilesBusinessMessages.FileNotExists);
    }

    public async Task CourtImageIdShouldExistWhenSelected(Guid id, CancellationToken cancellationToken)
    {
        CourtImage? courtImage = await _courtImageRepository.GetAsync(
            predicate: hkf => hkf.Id == id,
            enableTracking: false,
            cancellationToken: cancellationToken
        );
        await SFFileShouldExistWhenSelected(courtImage);
    }

    public Task FileIsImageFiles(IList<IFormFile> formFiles)
    {
        string[] extensionList = { ".gif", ".png", ".jpg", ".jpeg" };

        foreach (IFormFile formFile in formFiles)
        {
            bool isSuccess = extensionList.Any(extensions => extensions.Contains(formFile.FileName.Substring(formFile.FileName.LastIndexOf("."))));
            if (!isSuccess)
                throw new BusinessException(SFFilesBusinessMessages.FileIsNotImageFile);
        }
        return Task.CompletedTask;
    }

    public Task FileShouldBeMaxCount(IList<IFormFile> formFiles, int maxCount)
    {
        if (formFiles.Count >= maxCount)
            throw new BusinessException(SFFilesBusinessMessages.FilesCountGreatherThanFive);
        return Task.CompletedTask;
    }

    public async Task<bool> FileSizeIsCorrect(IFormFile formFile, int maxWidth, int maxHeight)
    {
        bool result = false;

        using (var stream = formFile.OpenReadStream())
        {
            using (var image = System.Drawing.Image.FromStream(stream))
            {
                if (image.Width <= maxWidth || image.Height <= maxHeight)
                    result = true;
            }
        }

        return result;
    }
}
