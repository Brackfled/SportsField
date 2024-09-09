using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class SuspendRepository : EfRepositoryBase<Suspend, Guid, BaseDbContext>, ISuspendRepository
{
    public SuspendRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Suspend>> GetAllAsync(Expression<Func<Suspend, bool>>? predicate = null)
    {
        IQueryable<Suspend> query = Query();
        if(predicate != null)
            query = query.Where(predicate);
        query.AsNoTracking();
        return await query.ToListAsync();
    }
}