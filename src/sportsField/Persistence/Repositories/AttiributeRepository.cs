using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class AttiributeRepository : EfRepositoryBase<Attiribute, int, BaseDbContext>, IAttiributeRepository
{
    public AttiributeRepository(BaseDbContext context) : base(context)
    {
    }
}