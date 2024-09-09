using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
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

namespace Application.Features.CourtReservations.Commands.RentReservation;
public class RentReservationCommand: IRequest<RentReservationResponse>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public Guid Id {  get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Rent];

    public bool BypassCache {  get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class RentReservationCommandHandler: IRequestHandler<RentReservationCommand, RentReservationResponse>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly IUserService _userService;
        private readonly IMailService _mailService;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly UserBusinessRules _userBusinessRules;
        private IMapper _mapper;

        public RentReservationCommandHandler(ICourtReservationRepository courtReservationRepository, IUserService userService, IMailService mailService, CourtReservationBusinessRules courtReservationBusinessRules, UserBusinessRules userBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _userService = userService;
            _mailService = mailService;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _userBusinessRules = userBusinessRules;
            _mapper = mapper;
        }

        public async Task<RentReservationResponse> Handle(RentReservationCommand request, CancellationToken cancellationToken)
        {
            User? user = await _userService.GetAsync(u => u.Id == request.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

            CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == request.Id, include: cr => cr.Include(opt => opt.Court!));
            await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);
            
            await _courtReservationBusinessRules.CourtReservationShouldBeActiveAndNotRented(courtReservation!);

            courtReservation!.UserId = user!.Id;
            CourtReservation updatedCourtReservation = await _courtReservationRepository.UpdateAsync(courtReservation);

            User? courtOwner = await _userService.GetAsync(u => u.Id == courtReservation.Court!.UserId);
            await _userBusinessRules.UserShouldBeExistsWhenSelected(courtOwner);
            List<MailboxAddress> mailboxAddresses = new List<MailboxAddress> { new MailboxAddress(name: courtOwner!.Email, courtOwner!.Email) };
            await _mailService.SendEmailAsync(new Mail
            {
                ToList = mailboxAddresses,
                Subject = "Randevu İptali",
                HtmlBody = $"<p>Merhabalar,<br/> <b>{courtReservation.AvailableDate}</b> tarihli; başlangıç saati <b>{courtReservation.StartTime}</b>, bitiş saati <b>{courtReservation.EndTime}</b> olan rezervasyonunuz <b>{user.FirstName} {user.LastName}</b> tarafından <b>kiralanmışdır</b>.<br/> Kullanıcı ile iletişime geçmek isterseniz <b>Kiralanan Randevular</b> kısımına bakabilirisiniz. <br/> İyi Günler.</p>"
            });

            RentReservationResponse response = _mapper.Map<RentReservationResponse>(updatedCourtReservation);
            return response;
        }
    }
}
