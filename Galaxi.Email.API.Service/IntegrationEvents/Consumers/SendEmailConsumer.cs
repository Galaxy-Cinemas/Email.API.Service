using Galaxi.Bus.Message;
using Galaxi.Email.API.Service.Service;
using MassTransit;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Net.NetworkInformation;
using System.Threading;

namespace Galaxi.Email.API.Service.IntegrationEvents.Consumers
{
    public class SendEmailConsumer : IConsumer<TickedCreated>
    {
        private readonly IEmailService _emailService;
        private readonly IRequestClient<MovieDetails> _client;
        public SendEmailConsumer(IEmailService emailService, IRequestClient<MovieDetails> client)
        {
            _emailService = emailService;
            _client = client;
        }
        public async Task Consume(ConsumeContext<TickedCreated> context)
         {
            //var response = await _client.GetResponse<MovieDetails>(new TickedCreated
            //{
            //    FunctionId = context.Message.FunctionId
            //});

            var ticket = new TickedCreated {
             FunctionId = context.Message.FunctionId,
             Email = context.Message.Email,
             UserName = context.Message.UserName,
             NumSeat = context.Message.NumSeat,
            };

            _emailService.SendEmail(ticket);  
        }
    }
}
