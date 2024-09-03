using Application.Features.Auth.Rules;
using Application.Services.AuthService;
using Application.Services.Repositories;
using Application.Services.UserOperationClaims;
using Domain.Dtos;
using Domain.Entities;
using MediatR;
using NArchitecture.Core.Application.Dtos;
using NArchitecture.Core.Security.Hashing;
using NArchitecture.Core.Security.JWT;

namespace Application.Features.Auth.Commands.Register;

public class RegisterCommand : IRequest<RegisteredResponse>
{
    public RegisterCommandDto RegisterCommandDto { get; set; }
    public string IpAddress { get; set; }

    public RegisterCommand()
    {
        RegisterCommandDto = null!;
        IpAddress = string.Empty;
    }

    public RegisterCommand(RegisterCommandDto userForRegisterDto, string ipAddress)
    {
        RegisterCommandDto = userForRegisterDto;
        IpAddress = ipAddress;
    }

    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, RegisteredResponse>
    {
        private readonly IUserRepository _userRepository;
        private readonly IAuthService _authService;
        private readonly IUserOperationClaimService _userOperationClaimService;
        private readonly AuthBusinessRules _authBusinessRules;

        public RegisterCommandHandler(IUserRepository userRepository, IAuthService authService, IUserOperationClaimService userOperationClaimService, AuthBusinessRules authBusinessRules)
        {
            _userRepository = userRepository;
            _authService = authService;
            _userOperationClaimService = userOperationClaimService;
            _authBusinessRules = authBusinessRules;
        }

        public async Task<RegisteredResponse> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            await _authBusinessRules.UserEmailShouldBeNotExists(request.RegisterCommandDto.Email);

            HashingHelper.CreatePasswordHash(
                request.RegisterCommandDto.Password,
                passwordHash: out byte[] passwordHash,
                passwordSalt: out byte[] passwordSalt
            );
            User newUser =
                new()
                {
                    FirstName = request.RegisterCommandDto.FirstName,
                    LastName = request.RegisterCommandDto.LastName,
                    Email = request.RegisterCommandDto.Email,
                    PasswordHash = passwordHash,
                    PasswordSalt = passwordSalt,
                };
            User createdUser = await _userRepository.AddAsync(newUser);

            await _userOperationClaimService.AddAsync(new UserOperationClaim
            {
                Id = Guid.NewGuid(),
                UserId = createdUser.Id,
                OperationClaimId = 51
            });
            
            await _userOperationClaimService.AddAsync(new UserOperationClaim
            {
                Id = Guid.NewGuid(),
                UserId = createdUser.Id,
                OperationClaimId = 52
            });

            AccessToken createdAccessToken = await _authService.CreateAccessToken(createdUser);

            Domain.Entities.RefreshToken createdRefreshToken = await _authService.CreateRefreshToken(
                createdUser,
                request.IpAddress
            );
            Domain.Entities.RefreshToken addedRefreshToken = await _authService.AddRefreshToken(createdRefreshToken);

            RegisteredResponse registeredResponse = new() { AccessToken = createdAccessToken, RefreshToken = addedRefreshToken };
            return registeredResponse;
        }
    }
}
