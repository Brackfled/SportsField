using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class CourtReservationRepository : EfRepositoryBase<CourtReservation, Guid, BaseDbContext>, ICourtReservationRepository
{
    public CourtReservationRepository(BaseDbContext context) : base(context)
    {
    }

    public async Task<ICollection<CourtReservation>> GetAllAsync(Expression<Func<CourtReservation, bool>>? predicate = null)
    {
        IQueryable<CourtReservation> query = Query();

        if (predicate != null)
            query = query.Where(predicate);

        query = query.AsNoTracking();
        return await query.ToListAsync();
    }
}