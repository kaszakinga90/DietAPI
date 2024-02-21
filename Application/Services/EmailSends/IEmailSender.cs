namespace Application.Services.EmailSends
{
    public interface IEmailSender
    {
        void SendEmail(EmailMessage message);
    }
}
