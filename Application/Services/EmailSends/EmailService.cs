using MailKit.Net.Smtp;
using MimeKit;
using System.Diagnostics;

namespace Application.Services.EmailSends
{
    // Serwis do wysyłania wiadomości email.
    public class EmailService : IEmailSender
    {
        private readonly EmailSenderConfiguration _emailConfig;

        // Konstruktor przyjmujący konfigurację serwera SMTP.
        public EmailService(EmailSenderConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        // Metoda do wysyłania emaila.
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        // Prywatna metoda do tworzenia obiektu MimeMessage na podstawie danych wiadomości.
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Author, "test@test.com"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            // Ustawienie treści wiadomości jako HTML.
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:blue;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        // Prywatna metoda do wysyłania emaila przy użyciu klienta SMTP.
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Łączenie z serwerem SMTP i uwierzytelnianie.
                    client.Connect(_emailConfig.SMTPServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2"); // Usunięcie mechanizmu XOAUTH2, jeśli nie jest używany.
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    // Wysyłanie wiadomości email.
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    // Logowanie wyjątków w przypadku błędu.
                    Debug.WriteLine(ex.Message, "Błąd podczas wysyłania wiadomości email. Temat: {0}, Odbiorca: {1}", mailMessage.Subject, mailMessage.To);
                    throw;
                }
                finally
                {
                    // Zakończenie połączenia i zwolnienie zasobów.
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}