using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;

namespace Persistence.Repositories;

public class OperationClaimRepository : EfRepositoryBase<OperationClaim, int, BaseDbContext>, IOperationClaimRepository
{
    public OperationClaimRepository(BaseDbContext context)
        : base(context) { }

    public async Task<ICollection<OperationClaim>> GetAllAsync()
    {
        ICollection<OperationClaim>? operationClaims = await Query()
            .AsNoTracking()
            .ToListAsync();

        return operationClaims;
    }
}
