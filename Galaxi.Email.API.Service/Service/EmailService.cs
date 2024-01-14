using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;
using Galaxi.Email.API.Service.Models;
using Galaxi.Bus.Message;

namespace Galaxi.Email.API.Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        public EmailService(IConfiguration config)
        {
            _config = config;
        }

        public void SendEmail()
        {
            var email = new MimeMessage();
            email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
            email.To.Add(MailboxAddress.Parse("juan.vega@pevaar.com"));
            email.To.Add(MailboxAddress.Parse("juanguativa07@gmail.com"));
            email.To.Add(MailboxAddress.Parse("saraguativa@gmail.com"));
            email.Subject = "CORREO PARA SARA VEGA";
            email.Body = new TextPart(TextFormat.Html)
            {
                Text = "Sara vega "
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
