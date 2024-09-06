using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Nest;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace Persistence.Repositories;
public class CourtImageRepository: EfRepositoryBase<CourtImage, Guid, BaseDbContext>, ICourtImageRepository
{
    public CourtImageRepository(BaseDbContext context): base(context)
    {
        
    }

    public async Task<ICollection<CourtImage>> GetAllAsync(Expression<Func<CourtImage, bool>>? predicate = null)
    {
        IQueryable<CourtImage> query = Query();

        if(predicate != null)
            query = query.Where(predicate);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}
