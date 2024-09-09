using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface ISuspendRepository : IAsyncRepository<Suspend, Guid>, IRepository<Suspend, Guid>
{
    Task<ICollection<Suspend>> GetAllAsync(Expression<Func<Suspend, bool>>? predicate = null);
}