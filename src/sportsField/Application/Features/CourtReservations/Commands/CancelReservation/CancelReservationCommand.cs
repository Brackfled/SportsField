using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using MimeKit;
using NArchitecture.Core.Application.Pipelines.Authorization;
using NArchitecture.Core.Application.Pipelines.Caching;
using NArchitecture.Core.Application.Pipelines.Transaction;
using NArchitecture.Core.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.CourtReservations.Commands.CancelReservation;
public class CancelReservationCommand: IRequest<CancelledReservationResponse>, ITransactionalRequest, ICacheRemoverRequest, ISecuredRequest
{
    public Guid UserId { get; set; }
    public Guid Id { get; set; }

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Cancel];

    public class CancelReservationCommandHandler: IRequestHandler<CancelReservationCommand, CancelledReservationResponse>
    {
        private readonly IUserService _userService;
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly IMailService _mailService;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public CancelReservationCommandHandler(IUserService userService, ICourtReservationRepository courtReservationRepository, IMailService mailService, UserBusinessRules userBusinessRules, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _userService = userService;
            _courtReservationRepository = courtReservationRepository;
            _mailService = mailService;
            _userBusinessRules = userBusinessRules;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<CancelledReservationResponse> Handle(CancelReservationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == request.Id, include: cr => cr.Include(opt => opt.Court!));
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);

            await _courtReservationBusinessRules.CourtReservationUserIdAndRequestIdMatched(courtReservation!, request.UserId, CourtReservationsOperationClaims.Cancel);

            courtReservation!.UserId = null;
            CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);

            User? courtOwner = await _userService.GetAsync(u => u.Id == courtReservation!.Court!.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(courtOwner);

            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress> { new MailboxAddress(name: courtOwner!.Email, courtOwner!.Email) };
            await _mailService.SendEmailAsync(new Mail
            {
                ToList = mailboxAddresses,
                Subject = "Randevu İptali",
                HtmlBody = $"<p>Merhabalar,<br/><b>{courtReservation.UpdatedDate!.Value.Date}</b> tarihinde <b>{user!.FirstName} {user!.LastName}</b> tarafından kiralanan <b>{courtReservation.AvailableDate}</b> tarihli; başlangıç saati <b>{courtReservation.StartTime}</b>, bitiş saati <b>{courtReservation.EndTime}</b> olan rezervasyonunuz kullanıcı tarafından <b>iptal</b> edilmiştir.<br/> Kullanıcı ile iletişime geçmek isterseniz <b>{user.Email}</b> mail' i üzerinden iletişime geçebilirisiniz.<br/> İyi Günler.</p>"
            });

            CancelledReservationResponse response = _mapper.Map<CancelledReservationResponse>(updatedCourtReservation);
            return response;
        }
    }
}
