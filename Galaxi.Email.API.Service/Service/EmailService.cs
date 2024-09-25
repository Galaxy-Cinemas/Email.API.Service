using MimeKit.Text;
using MimeKit;
using Galaxi.Bus.Message;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

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
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));

                email.To.Add(MailboxAddress.Parse(ticked.Email));
                email.Subject = "Ticket Purchased - GalaXinema";
                email.Body = new TextPart(TextFormat.Html)
                {

                    Text = $@"
                            <html>
                                <body>
                                  <p>Dear {ticked.UserName},</p>
                                  <p>Your ticket has been successfully confirmed for the cinema. Please present this confirmation upon arrival at the venue for admission. 
                                     We are excited to welcome you and ensure a smooth entry process.</p>
                                  <p>Thank you for choosing us, and we look forward to providing you with an enjoyable cinematic experience.</p>

                                  <p>Sincerely,</p>
                                  <p>Galaxinema</p>
                                </body>
                            </html>
                            "
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
            catch (Exception ex)
            {

                throw;
            }


        }
    }
}
