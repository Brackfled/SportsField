using Application.Features.Courts.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Courts;

public class CourtManager : ICourtService
{
    private readonly ICourtRepository _courtRepository;
    private readonly CourtBusinessRules _courtBusinessRules;

    public CourtManager(ICourtRepository courtRepository, CourtBusinessRules courtBusinessRules)
    {
        _courtRepository = courtRepository;
        _courtBusinessRules = courtBusinessRules;
    }

    public async Task<Court?> GetAsync(
        Expression<Func<Court, bool>> predicate,
        Func<IQueryable<Court>, IIncludableQueryable<Court, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Court? court = await _courtRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return court;
    }

    public async Task<IPaginate<Court>?> GetListAsync(
        Expression<Func<Court, bool>>? predicate = null,
        Func<IQueryable<Court>, IOrderedQueryable<Court>>? orderBy = null,
        Func<IQueryable<Court>, IIncludableQueryable<Court, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Court> courtList = await _courtRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return courtList;
    }

    public async Task<Court> AddAsync(Court court)
    {
        Court addedCourt = await _courtRepository.AddAsync(court);

        return addedCourt;
    }

    public async Task<Court> UpdateAsync(Court court)
    {
        Court updatedCourt = await _courtRepository.UpdateAsync(court);

        return updatedCourt;
    }

    public async Task<Court> DeleteAsync(Court court, bool permanent = false)
    {
        Court deletedCourt = await _courtRepository.DeleteAsync(court);

        return deletedCourt;
    }

    public async Task<ICollection<Court>> GetAllAsync(Expression<Func<Court, bool>>? predicate = null)
    {
        ICollection<Court>? courts = await _courtRepository.GetAllAsync(predicate);
        return courts;
    }
}
