using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Courts;

public interface ICourtService
{
    Task<Court?> GetAsync(
        Expression<Func<Court, bool>> predicate,
        Func<IQueryable<Court>, IIncludableQueryable<Court, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Court>?> GetListAsync(
        Expression<Func<Court, bool>>? predicate = null,
        Func<IQueryable<Court>, IOrderedQueryable<Court>>? orderBy = null,
        Func<IQueryable<Court>, IIncludableQueryable<Court, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Court> AddAsync(Court court);
    Task<Court> UpdateAsync(Court court);
    Task<Court> DeleteAsync(Court court, bool permanent = false);
    Task<ICollection<Court>> GetAllAsync(Expression<Func<Court, bool>>? predicate = null);
}
