using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services.Repositories;
public interface ICourtImageRepository: IAsyncRepository<CourtImage, Guid>, IRepository<CourtImage, Guid>
{
    Task<ICollection<CourtImage>> GetAllAsync(Expression<Func<CourtImage,bool>>? predicate = null);
}
