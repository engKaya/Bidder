using AutoMapper;
using Bidder.Notification.Application.Abstraction.EmailBase;
using Bidder.Notification.Application.Enum;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Bidder.Notification.Application.Services.EmailBase
{
    public class EmailFactory : IEmailFactory
    {
        private readonly ILogger<BasicAuthEmailClient> logger;
        private readonly IConfiguration conf;
        private readonly IMapper mapper;
        private  IEmailService _emailService;

        public EmailFactory(ILogger<BasicAuthEmailClient> logger, IConfiguration conf, IMapper mapper)
        {
            this.logger = logger;
            this.conf = conf;
            this.mapper = mapper;
        }

        public IEmailService EmailService 
        {
            get {
                if (_emailService is null)
                {
                    _emailService = GenerateEmailService(EmailClients.BasicAuthWithSmtpClient);
                    return _emailService;
                }
                else
                    return _emailService;
            }
        }
        
        public IEmailService GenerateEmailService(EmailClients clientType)
        {
            switch (clientType)
            {
                case EmailClients.BasicAuthWithSmtpClient:
                    _emailService = new BasicAuthEmailClient(logger,conf, mapper);
                    return _emailService;
                case EmailClients.ModernAuth:
                    _emailService = new BasicAuthEmailClient(logger, conf, mapper);
                    return _emailService;
                default:
                    throw new NotImplementedException();
            }
        }
         

    }
}
