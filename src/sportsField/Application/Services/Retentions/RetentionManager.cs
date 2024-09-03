using Application.Features.Retentions.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Retentions;

public class RetentionManager : IRetentionService
{
    private readonly IRetentionRepository _retentionRepository;
    private readonly RetentionBusinessRules _retentionBusinessRules;

    public RetentionManager(IRetentionRepository retentionRepository, RetentionBusinessRules retentionBusinessRules)
    {
        _retentionRepository = retentionRepository;
        _retentionBusinessRules = retentionBusinessRules;
    }

    public async Task<Retention?> GetAsync(
        Expression<Func<Retention, bool>> predicate,
        Func<IQueryable<Retention>, IIncludableQueryable<Retention, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Retention? retention = await _retentionRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return retention;
    }

    public async Task<IPaginate<Retention>?> GetListAsync(
        Expression<Func<Retention, bool>>? predicate = null,
        Func<IQueryable<Retention>, IOrderedQueryable<Retention>>? orderBy = null,
        Func<IQueryable<Retention>, IIncludableQueryable<Retention, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Retention> retentionList = await _retentionRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return retentionList;
    }

    public async Task<Retention> AddAsync(Retention retention)
    {
        Retention addedRetention = await _retentionRepository.AddAsync(retention);

        return addedRetention;
    }

    public async Task<Retention> UpdateAsync(Retention retention)
    {
        Retention updatedRetention = await _retentionRepository.UpdateAsync(retention);

        return updatedRetention;
    }

    public async Task<Retention> DeleteAsync(Retention retention, bool permanent = false)
    {
        Retention deletedRetention = await _retentionRepository.DeleteAsync(retention);

        return deletedRetention;
    }
}
