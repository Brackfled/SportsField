﻿using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IOperationClaimRepository : IAsyncRepository<OperationClaim, int>, IRepository<OperationClaim, int> 
{
    public Task<ICollection<OperationClaim>> GetAllAsync();
}
