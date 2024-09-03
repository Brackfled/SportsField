using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class RetentionRepository : EfRepositoryBase<Retention, Guid, BaseDbContext>, IRetentionRepository
{
    public RetentionRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Retention>> GetAllListAsync(Expression<Func<Retention, bool>>? predicate = null)
    {
        IQueryable<Retention> retentions =  Query();

        if (predicate != null)
            retentions = retentions.Where(predicate);

        retentions = retentions.AsNoTracking();
        return await retentions.ToListAsync();
            
    }
}