using AutoMapper;
using Bidder.Notification.Application.DTOs;
using Bidder.Notification.EventIntegration.AutoMapper.Resolver;
using Microsoft.Extensions.Configuration;
using System.Net.Mail;

namespace Bidder.Notification.EventIntegration.AutoMapper
{
    public class MailMappingProfile : Profile
    {
        public MailMappingProfile(IConfiguration conf)
        {
            CreateMap<MailParameters, MailMessage>()
                .ForMember(fp => fp.IsBodyHtml, to => to.MapFrom(x => x.IsHtml))
                .ForMember(fp => fp.To, to => to.MapFrom<MailToMailMessageToResolver>()    )
                .ForMember(fp => fp.CC, to => to.MapFrom<MailToMailMessageCCResolver>() )
                .ForMember(fp => fp.Bcc, to => to.MapFrom<MailToMailMessageBCCResolver>())
                .ForMember(fp => fp.Subject, to => to.MapFrom(x => x.Subject))
                .ForMember(fp => fp.Body, to => to.MapFrom(x => x.Body))
                .ForMember(fp => fp.IsBodyHtml, to => to.MapFrom(x => x.IsHtml))
                .ForMember(fp => fp.From, to => to.MapFrom(x => new MailAddress(x.MailFromAddress ?? conf["MailSettings:From"], x.MailFromName ?? conf["MailSettings:FromName"]))) 
                .ForMember(fp => fp.Attachments, to => to.MapFrom(x => x.Attachments));
        }
    }
}
