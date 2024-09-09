using Application.Features.Suspends.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Suspends;

public class SuspendManager : ISuspendService
{
    private readonly ISuspendRepository _suspendRepository;
    private readonly SuspendBusinessRules _suspendBusinessRules;

    public SuspendManager(ISuspendRepository suspendRepository, SuspendBusinessRules suspendBusinessRules)
    {
        _suspendRepository = suspendRepository;
        _suspendBusinessRules = suspendBusinessRules;
    }

    public async Task<Suspend?> GetAsync(
        Expression<Func<Suspend, bool>> predicate,
        Func<IQueryable<Suspend>, IIncludableQueryable<Suspend, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Suspend? suspend = await _suspendRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return suspend;
    }

    public async Task<IPaginate<Suspend>?> GetListAsync(
        Expression<Func<Suspend, bool>>? predicate = null,
        Func<IQueryable<Suspend>, IOrderedQueryable<Suspend>>? orderBy = null,
        Func<IQueryable<Suspend>, IIncludableQueryable<Suspend, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Suspend> suspendList = await _suspendRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return suspendList;
    }

    public async Task<Suspend> AddAsync(Suspend suspend)
    {
        Suspend addedSuspend = await _suspendRepository.AddAsync(suspend);

        return addedSuspend;
    }

    public async Task<Suspend> UpdateAsync(Suspend suspend)
    {
        Suspend updatedSuspend = await _suspendRepository.UpdateAsync(suspend);

        return updatedSuspend;
    }

    public async Task<Suspend> DeleteAsync(Suspend suspend, bool permanent = false)
    {
        Suspend deletedSuspend = await _suspendRepository.DeleteAsync(suspend);

        return deletedSuspend;
    }
}
