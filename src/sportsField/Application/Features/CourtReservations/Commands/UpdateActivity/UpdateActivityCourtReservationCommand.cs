using Amazon.Runtime.Internal;
using Application.Features.CourtReservations.Constants;
using Application.Features.CourtReservations.Rules;
using Application.Features.Courts.Rules;
using Application.Features.Users.Rules;
using Application.Services.Repositories;
using Application.Services.UsersService;
using AutoMapper;
using Domain.Entities;
using MailKit;
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

namespace Application.Features.CourtReservations.Commands.UpdateActivity;
public class UpdateActivityCourtReservationCommand: IRequest<ICollection<UpdatedActivityCourtReservationResponse>>, ITransactionalRequest, ISecuredRequest, ICacheRemoverRequest
{
    public Guid UserId { get; set; }
    public IList<Guid> Ids { get; set; }
    public bool IsActive { get; set; }

    public string[] Roles => [CourtReservationsOperationClaims.Admin, CourtReservationsOperationClaims.Update];

    public bool BypassCache { get; set; }

    public string? CacheKey => "";

    public string[]? CacheGroupKey => ["GetCourtReservations"];

    public class UpdateActivityCourtReservationCommandHandler: IRequestHandler<UpdateActivityCourtReservationCommand, ICollection<UpdatedActivityCourtReservationResponse>>
    {
        private readonly ICourtReservationRepository _courtReservationRepository;
        private readonly IUserService _userService;
        private readonly UserBusinessRules _userBusinessRules;
        private readonly NArchitecture.Core.Mailing.IMailService _mailService;
        private readonly CourtReservationBusinessRules _courtReservationBusinessRules;
        private readonly CourtBusinessRules _courtBusinessRules;
        private IMapper _mapper;

        public UpdateActivityCourtReservationCommandHandler(ICourtReservationRepository courtReservationRepository, NArchitecture.Core.Mailing.IMailService mailService, CourtReservationBusinessRules courtReservationBusinessRules, CourtBusinessRules courtBusinessRules, IMapper mapper)
        {
            _courtReservationRepository = courtReservationRepository;
            _mailService = mailService;
            _courtReservationBusinessRules = courtReservationBusinessRules;
            _courtBusinessRules = courtBusinessRules;
            _mapper = mapper;
        }

        public async Task<ICollection<UpdatedActivityCourtReservationResponse>> Handle(UpdateActivityCourtReservationCommand request, CancellationToken cancellationToken)
        {
            ICollection<CourtReservation> courtReservations = new List<CourtReservation>();
            foreach (Guid id in request.Ids)
            {
                CourtReservation? courtReservation = await _courtReservationRepository.GetAsync(cr => cr.Id == id, include:cr => cr.Include(opt => opt.User!));
                await _courtReservationBusinessRules.CourtReservationShouldExistWhenSelected(courtReservation);
                await _courtBusinessRules.UserIdNotMatchedCourtUserId(courtReservation!.CourtId, request.UserId, CourtReservationsOperationClaims.Admin);
                courtReservation.IsActive = request.IsActive;
                courtReservations.Add(courtReservation);


                if (courtReservation.UserId != null)
                {
                    User? user = await _userService.GetAsync(u => u.Id == courtReservation.UserId);
                    await _userBusinessRules.UserShouldBeExistsWhenSelected(user);

                    List<MailboxAddress> mailboxAddresses = new List<MailboxAddress> { new MailboxAddress(name: user!.Email, user!.Email) };

                    await _mailService.SendEmailAsync(new Mail
                    {
                        ToList = mailboxAddresses,
                        Subject = "Randevu İptali",
                        HtmlBody = $"<p>Merhabalar,<br/><b>{courtReservation.AvailableDate}</b> günü başlama saati <b>{courtReservation.StartTime}</b>, bitiş saati <b>{courtReservation.EndTime}</b> olan randevunuz saha sahini tarafından <b>iptal edilmiştir</b>.<br/>Detaylı bilgi için saha sahibi ile iletişime geçiniz.</p>"
                    });
                }
            }

            ICollection<CourtReservation> updatedCourtReservations =  await _courtReservationRepository.UpdateRangeAsync(courtReservations);


            ICollection<UpdatedActivityCourtReservationResponse> response = _mapper.Map<ICollection<UpdatedActivityCourtReservationResponse>>(updatedCourtReservations);
            return response;
        }
    }
}
