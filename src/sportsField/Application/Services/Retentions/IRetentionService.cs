using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Retentions;

public interface IRetentionService
{
    Task<Retention?> GetAsync(
        Expression<Func<Retention, bool>> predicate,
        Func<IQueryable<Retention>, IIncludableQueryable<Retention, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Retention>?> GetListAsync(
        Expression<Func<Retention, bool>>? predicate = null,
        Func<IQueryable<Retention>, IOrderedQueryable<Retention>>? orderBy = null,
        Func<IQueryable<Retention>, IIncludableQueryable<Retention, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Retention> AddAsync(Retention retention);
    Task<Retention> UpdateAsync(Retention retention);
    Task<Retention> DeleteAsync(Retention retention, bool permanent = false);
}
