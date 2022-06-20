using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;

namespace Shoppers.Utility
{
    public class EmailSender : IEmailSender
    {
        private readonly SmtpConfiguration _smtpConfiguration;

        public EmailSender(IOptions<SmtpConfiguration> smtpConfiguration)
        {
            _smtpConfiguration = smtpConfiguration.Value;
        }

        public void Send(string subject, string body, string receiverEmail, string receiverName)
        {           
            
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress(_smtpConfiguration.SenderName,
                _smtpConfiguration.SenderEmail));
            message.To.Add(new MailboxAddress(receiverName, receiverEmail));
            message.Subject = subject;

            message.Body = new TextPart(MimeKit.Text.TextFormat.Html)
            {
                Text = body
            };

            using (var client = new SmtpClient())
            {
                client.Connect(_smtpConfiguration.Server, _smtpConfiguration.Port,
                    _smtpConfiguration.UseSSL);
                client.Authenticate(_smtpConfiguration.Username, _smtpConfiguration.Password);
                client.Send(message);
                client.Disconnect(true);
            }
        }

    }
}