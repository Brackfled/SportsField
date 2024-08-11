using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CourtReservationRepository : EfRepositoryBase<CourtReservation, Guid, BaseDbContext>, ICourtReservationRepository
{
    public CourtReservationRepository(BaseDbContext context) : base(context)
    {
    }
}