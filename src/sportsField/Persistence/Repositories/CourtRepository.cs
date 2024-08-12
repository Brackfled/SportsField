using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class CourtRepository : EfRepositoryBase<Court, Guid, BaseDbContext>, ICourtRepository
{
    public CourtRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<Court>> GetAllAsync(Expression<Func<Court, bool>>? predicate = null)
    {
        IQueryable<Court> query = Query();

        if(predicate != null)
            query = query.Where(predicate);

        query = query.AsNoTracking();
        return await query.ToListAsync();       
    }
}