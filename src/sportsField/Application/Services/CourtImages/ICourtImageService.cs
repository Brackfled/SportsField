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
public interface ICourtImageService
{
    Task<CourtImage?> GetAsync(
        Expression<Func<CourtImage, bool>> predicate,
        Func<IQueryable<CourtImage>, IIncludableQueryable<CourtImage, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<IPaginate<CourtImage>?> GetListAsync(
        Expression<Func<CourtImage, bool>>? predicate = null,
        Func<IQueryable<CourtImage>, IOrderedQueryable<CourtImage>>? orderBy = null,
        Func<IQueryable<CourtImage>, IIncludableQueryable<CourtImage, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );

    Task<CourtImage> AddAsync(CourtImage courtImage);
    Task<CourtImage> UpdateAsync(CourtImage courtImage);
    Task<CourtImage> DeleteAsync(CourtImage courtImage, bool permanent = false);
}
