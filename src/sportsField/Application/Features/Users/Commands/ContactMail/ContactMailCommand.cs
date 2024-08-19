using Amazon.Runtime.Internal;
using MailKit;
using MediatR;
using MimeKit;
using NArchitecture.Core.Mailing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Users.Commands.ContactMail;
public class ContactMailCommand: IRequest<bool>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Email { get; set; }
    public string Message { get; set; }

    public class ContactMailCommandHandler: IRequestHandler<ContactMailCommand, bool>
    {
        private readonly NArchitecture.Core.Mailing.IMailService _mailService;

        public ContactMailCommandHandler(NArchitecture.Core.Mailing.IMailService mailService)
        {
            _mailService = mailService;
        }

        public Task<bool> Handle(ContactMailCommand request, CancellationToken cancellationToken)
        {
            var toEmailList = new List<MailboxAddress> { new(name: "iletişim@flepix.com.tr", address: "iletişim@flepix.com.tr") };

            if (toEmailList == null)
                return Task.FromResult(false);

            _mailService.SendMail(
            new Mail
            {
                ToList = toEmailList,
                Subject = $"{request.FirstName} {request.LastName} - {request.Email} tarafından gönderilen mesaj.",
                HtmlBody = $"<p>{request.Message}</p>"
            }
            );

            return Task.FromResult(true);
        }
    }
}
