using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface ICourtRepository : IAsyncRepository<Court, Guid>, IRepository<Court, Guid>
{
    public Task<ICollection<Court>> GetAllAsync(Expression<Func<Court, bool>>? predicate = null);
}