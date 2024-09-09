using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System.Linq.Expressions;

namespace Application.Services.Repositories;

public interface IUserRepository : IAsyncRepository<User, Guid>, IRepository<User, Guid> 
{
    Task<ICollection<User>> GetAllAsync(Expression<Func<User,bool>>? predicate= null);
}
