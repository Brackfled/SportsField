using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using NArchitecture.Core.Persistence.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.CourtImages;
public class CourtImageManager : ICourtImageService
{
    private readonly ICourtImageRepository _courtImageRepository;

    public async Task<CourtImage> AddAsync(CourtImage courtImage)
    {
        CourtImage addedCourtImage = await _courtImageRepository.AddAsync(courtImage);
        return addedCourtImage;
    }

    public async Task<CourtImage> DeleteAsync(CourtImage courtImage, bool permanent = false)
    {
        CourtImage deletedCourtImage = await _courtImageRepository.DeleteAsync(courtImage, permanent);
        return deletedCourtImage;
    }

    public async Task<CourtImage?> GetAsync(Expression<Func<CourtImage, bool>> predicate, Func<IQueryable<CourtImage>, IIncludableQueryable<CourtImage, object>>? include = null, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        CourtImage? courtImage = await _courtImageRepository.GetAsync(
            predicate, include, withDeleted, enableTracking,cancellationToken
            );

        return courtImage;
    }

    public async Task<IPaginate<CourtImage>?> GetListAsync(Expression<Func<CourtImage, bool>>? predicate = null, Func<IQueryable<CourtImage>, IOrderedQueryable<CourtImage>>? orderBy = null, Func<IQueryable<CourtImage>, IIncludableQueryable<CourtImage, object>>? include = null, int index = 0, int size = 10, bool withDeleted = false, bool enableTracking = true, CancellationToken cancellationToken = default)
    {
        IPaginate<CourtImage>? courtImages = await _courtImageRepository.GetListAsync(
                predicate, orderBy, include, size, index, withDeleted, enableTracking,cancellationToken
            );

        return courtImages;
    }

    public async Task<CourtImage> UpdateAsync(CourtImage courtImage)
    {
        CourtImage updatedCourtImage = await _courtImageRepository.UpdateAsync(courtImage);
        return updatedCourtImage;
    }
}
