using MimeKit.Text;
using MimeKit;
using Galaxi.Bus.Message;
using SmtpClient = MailKit.Net.Smtp.SmtpClient;

namespace Galaxi.Email.API.Service.Service
{
    public class EmailService : IEmailService
    {
        private readonly IConfiguration _config;
        private readonly ILogger<EmailService> _log;

        public EmailService(IConfiguration config, ILogger<EmailService> log)
        {
            _config = config;
            _log = log;
        }

        public void SendEmail(TickedCreated ticked)
        {
            try
            {
                _log.LogInformation("Starting to compose the email.");
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(_config.GetSection("Email:UserName").Value));
                
                email.To.Add(MailboxAddress.Parse(ticked.Email));
                _log.LogTrace($"Recipient email set: {ticked.Email}");
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
                _log.LogInformation("SMTP server connection established.");

                smtp.Authenticate(_config.GetSection("Email:UserName").Value, _config.GetSection("Email:PassWord").Value);
                _log.LogInformation("SMTP authentication successfully");

                smtp.Send(email);
                _log.LogTrace($"Email successfully sent to {ticked.Email}");
                smtp.Disconnect(true);
                _log.LogInformation("SMTP server disconnected.");
            }
            catch (Exception ex)
            {
                _log.LogError(ex, $"An error occurred while sending email to {ticked.Email}");
                throw;
            }


        }
    }
}
