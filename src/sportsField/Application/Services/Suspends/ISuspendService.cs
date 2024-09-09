using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Suspends;

public interface ISuspendService
{
    Task<Suspend?> GetAsync(
        Expression<Func<Suspend, bool>> predicate,
        Func<IQueryable<Suspend>, IIncludableQueryable<Suspend, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Suspend>?> GetListAsync(
        Expression<Func<Suspend, bool>>? predicate = null,
        Func<IQueryable<Suspend>, IOrderedQueryable<Suspend>>? orderBy = null,
        Func<IQueryable<Suspend>, IIncludableQueryable<Suspend, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Suspend> AddAsync(Suspend suspend);
    Task<Suspend> UpdateAsync(Suspend suspend);
    Task<Suspend> DeleteAsync(Suspend suspend, bool permanent = false);
}
