using Application.Services.Repositories;
using Domain.Entities;
using NArchitecture.Core.Persistence.Repositories;
using Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistence.Repositories;
public class CourtImageRepository: EfRepositoryBase<CourtImage, Guid, BaseDbContext>, ICourtImageRepository
{
    public CourtImageRepository(BaseDbContext context): base(context)
    {
        
    }
}
