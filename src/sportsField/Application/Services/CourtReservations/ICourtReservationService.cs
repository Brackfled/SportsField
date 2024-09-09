using NArchitecture.Core.Persistence.Paging;
using Domain.Entities;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Application.Services.CourtReservations;

public interface ICourtReservationService
{
    Task<CourtReservation?> GetAsync(
        Expression<Func<CourtReservation, bool>> predicate,
        Func<IQueryable<CourtReservation>, IIncludableQueryable<CourtReservation, object>>? include = null,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<IPaginate<CourtReservation>?> GetListAsync(
        Expression<Func<CourtReservation, bool>>? predicate = null,
        Func<IQueryable<CourtReservation>, IOrderedQueryable<CourtReservation>>? orderBy = null,
        Func<IQueryable<CourtReservation>, IIncludableQueryable<CourtReservation, object>>? include = null,
        int index = 0,
        int size = 10,
        bool withDeleted = false,
        bool enableTracking = true,
        CancellationToken cancellationToken = default
    );
    Task<CourtReservation> AddAsync(CourtReservation courtReservation);
    Task<CourtReservation> UpdateAsync(CourtReservation courtReservation);
    Task<CourtReservation> DeleteAsync(CourtReservation courtReservation, bool permanent = false);
    Task<ICollection<CourtReservation>> DeleteOldReservationsAsync();
    Task<ICollection<CourtReservation>> GetAllAsync(Expression<Func<CourtReservation, bool>>? predicate = null);
}
