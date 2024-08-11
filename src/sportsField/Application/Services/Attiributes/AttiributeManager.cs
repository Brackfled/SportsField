using Application.Features.Attiributes.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.Attiributes;

public class AttiributeManager : IAttiributeService
{
    private readonly IAttiributeRepository _attiributeRepository;
    private readonly AttiributeBusinessRules _attiributeBusinessRules;

    public AttiributeManager(IAttiributeRepository attiributeRepository, AttiributeBusinessRules attiributeBusinessRules)
    {
        _attiributeRepository = attiributeRepository;
        _attiributeBusinessRules = attiributeBusinessRules;
    }

    public async Task<Attiribute?> GetAsync(
        Expression<Func<Attiribute, bool>> predicate,
        Func<IQueryable<Attiribute>, IIncludableQueryable<Attiribute, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        Attiribute? attiribute = await _attiributeRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return attiribute;
    }

    public async Task<IPaginate<Attiribute>?> GetListAsync(
        Expression<Func<Attiribute, bool>>? predicate = null,
        Func<IQueryable<Attiribute>, IOrderedQueryable<Attiribute>>? orderBy = null,
        Func<IQueryable<Attiribute>, IIncludableQueryable<Attiribute, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<Attiribute> attiributeList = await _attiributeRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return attiributeList;
    }

    public async Task<Attiribute> AddAsync(Attiribute attiribute)
    {
        Attiribute addedAttiribute = await _attiributeRepository.AddAsync(attiribute);

        return addedAttiribute;
    }

    public async Task<Attiribute> UpdateAsync(Attiribute attiribute)
    {
        Attiribute updatedAttiribute = await _attiributeRepository.UpdateAsync(attiribute);

        return updatedAttiribute;
    }

    public async Task<Attiribute> DeleteAsync(Attiribute attiribute, bool permanent = false)
    {
        Attiribute deletedAttiribute = await _attiributeRepository.DeleteAsync(attiribute);

        return deletedAttiribute;
    }
}
