using MailKit.Net.Smtp;
using MailKit.Security;
using MimeKit;
using System.Security.Authentication;

namespace Application.Services.EmailSends
{
    public class EmailService : IEmailSender
    {
        private readonly EmailSenderConfiguration _emailConfig;
        public EmailService(EmailSenderConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage); ;
        }

        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Author, "test@test.com"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:blue;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // TODO:

                    //client.ServerCertificateValidationCallback = (s, c, h, e) => true;
                    //client.CheckCertificateRevocation = false;
                    //client.SslProtocols = SslProtocols.Tls | SslProtocols.Tls11 | SslProtocols.Tls12 | SslProtocols.Tls13;
                    //client.SslProtocols = System.Security.Authentication.SslProtocols.Tls;
                    client.Connect(_emailConfig.SMTPServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2");
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    client.Send(mailMessage);
                }
                catch
                {
                    //log an error message or throw an exception or both.
                    throw;
                }
                finally
                {
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }

        //public async Task SendEmailAsync(EmailMessage message)
        //{
        //    var mailMessage = CreateEmailMessage(message);

        //    await SendAsync(mailMessage);
        //}
    }
}
