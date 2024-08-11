using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class CourtRepository : EfRepositoryBase<Court, Guid, BaseDbContext>, ICourtRepository
{
    public CourtRepository(BaseDbContext context) : base(context)
    {
    }
}