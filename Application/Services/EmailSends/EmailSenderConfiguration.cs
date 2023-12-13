namespace Application.Services.EmailSends
{
    public class EmailSenderConfiguration
    {
        public string Author { get; set; }
        public string SMTPServer { get; set; }
        public int Port { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
