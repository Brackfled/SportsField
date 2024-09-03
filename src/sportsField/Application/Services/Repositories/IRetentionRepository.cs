using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IRetentionRepository : IAsyncRepository<Retention, Guid>, IRepository<Retention, Guid>
{
    Task<ICollection<Retention>> GetAllListAsync(Expression<Func<Retention, bool>>? predicate = null);
}