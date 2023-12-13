namespace Application.Services.EmailSends
{
    // TODO : send email async
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
        //Task SendEmailAsync(EmailMessage message);
    }
}
