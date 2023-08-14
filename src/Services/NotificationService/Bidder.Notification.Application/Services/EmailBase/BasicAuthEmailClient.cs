using AutoMapper; 
using Bidder.Notification.Application.Abstraction.EmailBase;
using Bidder.Notification.Application.DTOs;
using Bidder.Notification.Application.Response;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Net;
using System.Net.Mail;

namespace Bidder.Notification.Application.Services.EmailBase
{
    public class BasicAuthEmailClient : IEmailService
    {
        private readonly ILogger<BasicAuthEmailClient> logger;
        private readonly IMapper mapper;
        private readonly string mailHost;
        private readonly int mailPort;
        private readonly string mailUserName;
        private readonly string mailPassword;
        private readonly string mailFrom;
        private readonly string mailFromName; 
        public BasicAuthEmailClient(ILogger<BasicAuthEmailClient> logger, IConfiguration conf, IMapper mapper)
        {
            this.logger = logger;
            this.mapper = mapper;
            mailHost = conf["MailSettings:Host"];
            mailPort = int.Parse(conf["MailSettings:Port"]);
            mailUserName = conf["MailSettings:UserName"];
            mailPassword = conf["MailSettings:Password"];
            mailFrom = conf["MailSettings:From"];
            mailFromName = conf["MailSettings:FromName"];
        }
        public Task<EmailResponse> SendEmailAsync(MailParameters parameters)
        {
            try
            {
                parameters.MailFromAddress = mailFrom;
                parameters.MailFromName = mailFromName;
                MailMessage mailMessage = mapper.Map<MailMessage>(parameters);
                SendEmail(mailMessage);
                return Task.FromResult(new EmailResponse(true, mailMessage.Body));
            }
            catch (Exception ex)
            {
                logger.LogError("An error occured when trying to send e-mail", ex.StackTrace);
                throw;
            }
        }
        private void SendEmail(MailMessage mail)
        {
            var client = new SmtpClient();
            client.Host = mailHost;
            client.Port = mailPort;
            client.EnableSsl = true;
            client.UseDefaultCredentials = false;
            client.Credentials = new NetworkCredential(mailUserName, mailPassword);
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            //client.Send(mail);

            logger.LogInformation($"Email sent successfully, Email: {mail.Body}");
        }
    }
}