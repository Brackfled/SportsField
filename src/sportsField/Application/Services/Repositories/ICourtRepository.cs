using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface ICourtRepository : IAsyncRepository<Court, Guid>, IRepository<Court, Guid>
{
}