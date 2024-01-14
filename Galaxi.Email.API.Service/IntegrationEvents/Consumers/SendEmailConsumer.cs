using Galaxi.Bus.Message;
using Galaxi.Email.API.Service.Service;
using MassTransit;

namespace Galaxi.Email.API.Service.IntegrationEvents.Consumers
{
    public class SendEmailConsumer : IConsumer<TickedCreated>
    {
        private readonly IEmailService _emailService;
        public SendEmailConsumer(IEmailService emailService)
        {
            _emailService = emailService;
        }
        public async Task Consume(ConsumeContext<TickedCreated> context)
         {
            _emailService.SendEmail();  
        }
    }
}
