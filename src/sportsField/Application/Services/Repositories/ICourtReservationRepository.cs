using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface ICourtReservationRepository : IAsyncRepository<CourtReservation, Guid>, IRepository<CourtReservation, Guid>
{
    Task<ICollection<CourtReservation>> GetAllAsync(Expression<Func<CourtReservation, bool>>? predicate = null);
}