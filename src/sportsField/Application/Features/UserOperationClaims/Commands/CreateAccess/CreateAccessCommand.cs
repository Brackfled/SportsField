using Amazon.Runtime.Internal;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Logging;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Persistence.Paging;
using NArchitecture.Core.Security.Constants;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateAccess;
public class CreateAccessCommand: MediatR.IRequest<bool>, ITransactionalRequest, ISecuredRequest
{
    public CreateAccessCommandDto CreateAccessDto { get; set; }

    public string[] Roles => [GeneralOperationClaims.Admin];

    public class CreateAccessCommandHandler: IRequestHandler<CreateAccessCommand, bool>
    {
        private readonly IUserOperationClaimRepository _userOperationClaimRepository;
        private readonly IUserService _userService;
        private readonly UserBusinessRules _userBusinessRules;

        public CreateAccessCommandHandler(IUserOperationClaimRepository userOperationClaimRepository, IUserService userService, UserBusinessRules userBusinessRules)
        {
            _userOperationClaimRepository = userOperationClaimRepository;
            _userService = userService;
            _userBusinessRules = userBusinessRules;
        }

        public async Task<bool> Handle(CreateAccessCommand request, CancellationToken cancellationToken)
        {

            User? user = await _userService.GetAsync(u => u.Id == request.CreateAccessDto.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            ICollection<UserOperationClaim>? useroc = await _userOperationClaimRepository.Query().AsNoTracking().Where(uoc => uoc.UserId == user!.Id).ToListAsync();
            await _userOperationClaimRepository.DeleteRangeAsync(useroc, true);

            foreach(int item in request.CreateAccessDto.ClaimIds)
            {
                UserOperationClaim claim = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user!.Id,
                    OperationClaimId = item
                };

                await _userOperationClaimRepository.AddAsync(claim);
            }

            user.UserState = request.CreateAccessDto.UserState;

            await _userService.UpdateAsync(user!);
            return true;
        }
    }
}
