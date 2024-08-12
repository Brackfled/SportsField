using Amazon.Runtime.Internal;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
using Domain.Entities;
using Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Persistence.Paging;
using Nest;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.UserOperationClaims.Commands.CreateAccess;
public class CreateAccessCommand: MediatR.IRequest<bool>, ITransactionalRequest//, ISecuredRequest
{
    public Guid UserId { get; set; }
    public UserState UserState { get; set; }

    public string[] Roles => [];

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
            int[] selectedOperationClaims = [];

            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            ICollection<UserOperationClaim>? useroc = await _userOperationClaimRepository.Query().AsNoTracking().Where(uoc => uoc.UserId == user!.Id).ToListAsync();
            await _userOperationClaimRepository.DeleteRangeAsync(useroc, true);

            if (request.UserState == UserState.Admin)
            {
                selectedOperationClaims = [24, 18, 30, 36, 42, 33, 34, 35, 31, 39, 40, 41, 43, 44, 45];
                user!.UserState = UserState.Admin;
            }


            if (request.UserState == UserState.CourtOwner)
            {
                selectedOperationClaims = [33, 34, 35, 31, 39, 40, 41, 43, 44, 37, 45];
                user.UserState = UserState.CourtOwner;
            }


            foreach(int item in selectedOperationClaims)
            {
                UserOperationClaim claim = new()
                {
                    Id = Guid.NewGuid(),
                    UserId = user!.Id,
                    OperationClaimId = item
                };

                await _userOperationClaimRepository.AddAsync(claim);
            }

            await _userService.UpdateAsync(user!);
            return true;
        }
    }
}
