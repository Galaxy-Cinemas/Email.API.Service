using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Galaxi.Email.API.Service.Models;
using Galaxi.Bus.Message;
using System;

namespace Galaxi.Email.API.Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail(TickedCreated ticked)
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse("juan.vega@pevaar.com"));
            email.To.Add(MailboxAddress.Parse("juanguativa07@gmail.com"));
            email.Subject = "Ticket Purchased - GalaXinema";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "Se creo el ticke para la funcion" + ticked.FunctionId
            };

            using var smtp = new SmtpClient();
            smtp.Connect(
                _config.GetSection("Email:Host").Value,
               Convert.ToInt32(_config.GetSection("Email:Port").Value)
                );

            smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);

            smtp.Send(email);
            smtp.Disconnect(true);
        }
    }
}
