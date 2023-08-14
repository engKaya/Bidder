using AutoMapper;
using Bidder.Notification.Application.DTOs;
using System.Net.Mail;

namespace Bidder.Notification.EventIntegration.AutoMapper.Resolver
{
    public class MailToMailMessageToResolver : IValueResolver<MailParameters, MailMessage, MailAddressCollection>
    {
        public MailAddressCollection Resolve(MailParameters source, MailMessage destination, MailAddressCollection destMember, ResolutionContext context)
        {
            ArgumentNullException.ThrowIfNull(source, "source");
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
            if (source.Cc is null)
                return null;
            destination.CC.Add(string.Join(",", source.Cc));
            return destination.CC;
        }
    }
    public class MailToMailMessageBCCResolver : IValueResolver<MailParameters, MailMessage, MailAddressCollection>
    {
        public MailAddressCollection Resolve(MailParameters source, MailMessage destination, MailAddressCollection destMember, ResolutionContext context)
        {
            if (source.Bcc is null)
                return null;
            destination.Bcc.Add(string.Join(",", source.Bcc));
            return destination.Bcc;
        }
    }
}
