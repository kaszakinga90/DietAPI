using MailKit.Net.Smtp;
using MimeKit;
using System.Diagnostics;

namespace Application.Services.EmailSends
{
    // Serwis służący do wysyłania wiadomości e-mail
    public class EmailService : IEmailSender
    {
        private readonly EmailSenderConfiguration _emailConfig;

        // Inicjalizacja danych konfiguracyjnych serwera SMTP
        public EmailService(EmailSenderConfiguration emailConfig)
        {
            _emailConfig = emailConfig;
        }

        // Metoda realizująca wysładnie wiadomości e-mail
        public void SendEmail(EmailMessage message)
        {
            var emailMessage = CreateEmailMessage(message);
            Send(emailMessage);
        }

        // Prywatna metoda do tworzenia obiektu MimeMessage na podstawie danych wiadomości
        // Metoda do utworzenia wiadomości i jej treści
        private MimeMessage CreateEmailMessage(EmailMessage message)
        {
            var emailMessage = new MimeMessage();
            // Ustawienie przykładowej nazwy autora wiadomości e-mail
            emailMessage.From.Add(new MailboxAddress(_emailConfig.Author, "test@test.com"));
            emailMessage.To.AddRange(message.To);
            emailMessage.Subject = message.Subject;
            // Ustawienie treści wiadomości jako HTML z zastosowaniem podstawowej stylistyki
            emailMessage.Body = new TextPart(MimeKit.Text.TextFormat.Html) { Text = string.Format("<h2 style='color:blue;'>{0}</h2>", message.Content) };

            return emailMessage;
        }

        // Metoda do wysłania wiadomości e-mail wykorzystująca w tym celu klienta SMTP
        private void Send(MimeMessage mailMessage)
        {
            using (var client = new SmtpClient())
            {
                try
                {
                    // Nawiązanie połączenia z serwerem SMTP i uwierzytelnianie
                    client.Connect(_emailConfig.SMTPServer, _emailConfig.Port, false);
                    client.AuthenticationMechanisms.Remove("XOAUTH2"); // Usunięcie mechanizmu XOAUTH2, jeśli nie jest używany
                    client.Authenticate(_emailConfig.Username, _emailConfig.Password);

                    // Wysłanie wiadomości e-mail
                    client.Send(mailMessage);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine(ex.Message, "Błąd podczas wysyłania wiadomości email. Temat: {0}, Odbiorca: {1}", mailMessage.Subject, mailMessage.To);
                    throw;
                }
                finally
                {
                    // Zakończenie połączenia i zwolnienie zasobów
                    client.Disconnect(true);
                    client.Dispose();
                }
            }
        }
    }
}