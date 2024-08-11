using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Attiributes;

public interface IAttiributeService
{
    Task<Attiribute?> GetAsync(
        Expression<Func<Attiribute, bool>> predicate,
        Func<IQueryable<Attiribute>, IIncludableQueryable<Attiribute, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<Attiribute>?> GetListAsync(
        Expression<Func<Attiribute, bool>>? predicate = null,
        Func<IQueryable<Attiribute>, IOrderedQueryable<Attiribute>>? orderBy = null,
        Func<IQueryable<Attiribute>, IIncludableQueryable<Attiribute, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<Attiribute> AddAsync(Attiribute attiribute);
    Task<Attiribute> UpdateAsync(Attiribute attiribute);
    Task<Attiribute> DeleteAsync(Attiribute attiribute, bool permanent = false);
}
