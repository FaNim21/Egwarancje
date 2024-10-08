﻿using MailKit.Security;
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

        public bool SendAccountConfirmationEmail(string to, string userName, string confirmationLink)
        {
            string body = $@"
                <html>
                <body>
                    <p>Dear {userName},</p>
                    <p>Thank you for registering. Please <a href='{confirmationLink}'>click here</a> to confirm your account.</p>
                    <p>Regards,<br>Your Company</p>
                </body>
                </html>";
            string subject = "Confirm your account";

            return Email(subject, body, to);
        }

        public bool SendNewPassword(string to, string userName, string newPassword)
        {
            string body = $@"
                <html>
                <body>
                    <p>Dear {userName},</p>
                    <p>Here is your new temporary password: {newPassword}</p>
                    <p>Regards,<br>Your Company</p>
                </body>
                </html>";
            string subject = "Password has been reset";

            return Email(subject, body, to);
        }
    }
}