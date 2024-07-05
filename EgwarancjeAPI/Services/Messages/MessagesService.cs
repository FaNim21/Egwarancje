using MailKit.Security;
using MimeKit.Text;
using MimeKit;
using MailKit.Net.Smtp;

namespace EgwarancjeAPI.Services.Messages
{
    public class MessagesService : IMessagesService
    {
        private readonly IConfiguration config;
        private readonly ILogger<MessagesService> logger;


        public MessagesService(IConfiguration config, ILogger<MessagesService> logger)
        {
            this.config = config;
            this.logger = logger;
        }
        public bool Email(string subject, string body, string to)
        {
            try
            {
                var email = new MimeMessage();
                email.From.Add(MailboxAddress.Parse(config.GetSection("Email:Username").Value));
                email.To.Add(MailboxAddress.Parse(to));
                email.Subject = subject;
                email.Body = new TextPart(TextFormat.Html) { Text = body };

                using var smtp = new SmtpClient();
                smtp.Connect(config.GetSection("Email:Host").Value, int.Parse(config.GetSection("Email:Port").Value!), SecureSocketOptions.SslOnConnect);
                smtp.Authenticate(config.GetSection("Email:Username").Value, config.GetSection("Email:Password").Value);
                smtp.Send(email);
                smtp.Disconnect(true);

                return true;
            }
            catch (Exception error)
            {
                logger.LogError(error.Message);
                return false;
            }
        }
    }
}