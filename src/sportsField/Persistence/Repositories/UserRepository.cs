using Application.Services.Repositories;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System.Linq.Expressions;

namespace Persistence.Repositories;

public class UserRepository : EfRepositoryBase<User, Guid, BaseDbContext>, IUserRepository
{
    public UserRepository(BaseDbContext context)
        : base(context) { }

    public async Task<ICollection<User>> GetAllAsync(Expression<Func<User, bool>>? predicate = null)
    {
        IQueryable<User> users = Query();
        if(predicate != null)
            users = users.Where(predicate);
        users.AsNoTracking();
        return await users.ToListAsync();
    }
}
