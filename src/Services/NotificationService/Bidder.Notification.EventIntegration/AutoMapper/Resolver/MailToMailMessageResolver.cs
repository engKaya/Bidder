using AutoMapper;
using Bidder.Notification.Application.DTOs;
using System.Net.Mail;

namespace Bidder.Notification.EventIntegration.AutoMapper.Resolver
{
    public class MailToMailMessageToResolver : IValueResolver<MailParameters, MailMessage, MailAddressCollection>
    {
        public MailAddressCollection Resolve(MailParameters source, MailMessage destination, MailAddressCollection destMember, ResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(source.To, "source");
            MailAddressCollection mailAddresses= new MailAddressCollection();
            foreach (var item in source.To)
            {
                mailAddresses.Add(item);
            }

            return mailAddresses;
        } 
    }
    public class MailToMailMessageCCResolver : IValueResolver<MailParameters, MailMessage, MailAddressCollection>
    {
        public MailAddressCollection Resolve(MailParameters source, MailMessage destination, MailAddressCollection destMember, ResolutionContext context)
        {
            if (source.Cc is null || source.Cc.Count == 0)
                return null;
            MailAddressCollection mailAddresses = new MailAddressCollection();
            foreach (var item in source.Cc)
            {
                mailAddresses.Add(item);
            }

            return mailAddresses;
        }
    }
    public class MailToMailMessageBCCResolver : IValueResolver<MailParameters, MailMessage, MailAddressCollection>
    {
        public MailAddressCollection Resolve(MailParameters source, MailMessage destination, MailAddressCollection destMember, ResolutionContext context)
        {
            if (source.Bcc is null || source.Bcc.Count == 0)
                return null;


            MailAddressCollection mailAddresses = new MailAddressCollection();
            foreach (var item in source.Bcc)
            {
                mailAddresses.Add(item);
            }

            return mailAddresses;
        }
    }
}
