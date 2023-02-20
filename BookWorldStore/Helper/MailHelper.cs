using MailKit.Net.Smtp;
using MimeKit;

namespace BookWorldStore.Helper
{
    public class MailHelper
    {
        private static MailHelper _instance;
        public static MailHelper Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new MailHelper();
                }
                return _instance;
            }
        }
        private MailHelper() { }

        public async Task SendEmail(string to, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("FPTBook", "khangak1999@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("html")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("khangak1999@gmail.com", "nmdpdbaenvufwrxm");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}



