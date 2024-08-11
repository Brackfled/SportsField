using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICourtReservationRepository : IAsyncRepository<CourtReservation, Guid>, IRepository<CourtReservation, Guid>
{
}