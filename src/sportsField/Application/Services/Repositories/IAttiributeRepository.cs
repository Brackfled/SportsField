using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;

namespace Application.Services.Repositories;

public interface IAttiributeRepository : IAsyncRepository<Attiribute, int>, IRepository<Attiribute, int>
{
}