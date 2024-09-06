using Application.Features.CourtReservations.Rules;
using Application.Services.Repositories;
using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CourtReservations;

public class CourtReservationManager : ICourtReservationService
{
    private readonly ICourtReservationRepository _courtReservationRepository;
    private readonly CourtReservationBusinessRules _courtReservationBusinessRules;

    public CourtReservationManager(ICourtReservationRepository courtReservationRepository, CourtReservationBusinessRules courtReservationBusinessRules)
    {
        _courtReservationRepository = courtReservationRepository;
        _courtReservationBusinessRules = courtReservationBusinessRules;
    }

    public async Task<CourtReservation?> GetAsync(
        Expression<Func<CourtReservation, bool>> predicate,
        Func<IQueryable<CourtReservation>, IIncludableQueryable<CourtReservation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(predicate, include, withDeleted, enableTracking, cancellationToken);
        return courtReservation;
    }

    public async Task<IPaginate<CourtReservation>?> GetListAsync(
        Expression<Func<CourtReservation, bool>>? predicate = null,
        Func<IQueryable<CourtReservation>, IOrderedQueryable<CourtReservation>>? orderBy = null,
        Func<IQueryable<CourtReservation>, IIncludableQueryable<CourtReservation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    )
    {
        IPaginate<CourtReservation> courtReservationList = await _courtReservationRepository.GetListAsync(
            predicate,
            orderBy,
            include,
            index,
            size,
            withDeleted,
            enableTracking,
            cancellationToken
        );
        return courtReservationList;
    }

    public async Task<CourtReservation> AddAsync(CourtReservation courtReservation)
    {
        CourtReservation addedCourtReservation = await _courtReservationRepository.AddAsync(courtReservation);

        return addedCourtReservation;
    }

    public async Task<CourtReservation> UpdateAsync(CourtReservation courtReservation)
    {
        CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);

        return updatedCourtReservation;
    }

    public async Task<CourtReservation> DeleteAsync(CourtReservation courtReservation, bool permanent = false)
    {
        CourtReservation deletedCourtReservation = await _courtReservationRepository.DeleteAsync(courtReservation);

        return deletedCourtReservation;
    }

    public async Task<ICollection<CourtReservation>> DeleteOldReservationsAsync()
    {
        ICollection<CourtReservation> courtReservations = await _courtReservationRepository.GetAllAsync();

        ICollection<CourtReservation> deletedCourtReservations = courtReservations.Where(cr => cr.AvailableDate < DateTime.UtcNow.Date || (cr.AvailableDate <= DateTime.UtcNow.Date && cr.StartTime < DateTime.UtcNow.TimeOfDay)).ToList();
        await _courtReservationRepository.DeleteRangeAsync(deletedCourtReservations, true);
        return deletedCourtReservations;
    }
}
