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

        public async Task sendEmail(string recipient, string subject, string message)
        {
            //var fromAddress = new MailAddress("khangok1610@gmail.com", "Sender");
            //var toAddress = new MailAddress(recipient, "Recipient");
            //var smtp = new SmtpClient
            //{
            //    Host = "smtp.gmail.com",
            //    Port = 587,
            //    EnableSsl = true,
            //    DeliveryMethod = SmtpDeliveryMethod.Network,
            //    UseDefaultCredentials = false,
            //    Credentials = new NetworkCredential("khangok1610@gmail.com", "Kt21082002")
            //};
            //using (var mailMessage = new MailMessage(fromAddress, toAddress)
            //{
            //    Subject = subject,
            //    Body = message
            //})
            //{
            //    await smtp.SendMailAsync(mailMessage);
            //}
        }

        public async Task SendEmail(string to, string subject, string message)
        {
            var emailMessage = new MimeMessage();

            emailMessage.From.Add(new MailboxAddress("FPTBook", "khangok1610@gmail.com"));
            emailMessage.To.Add(new MailboxAddress("", to));
            emailMessage.Subject = subject;

            emailMessage.Body = new TextPart("plain")
            {
                Text = message
            };

            using (var client = new SmtpClient())
            {
                client.Connect("smtp.gmail.com", 587, false);
                client.Authenticate("khangok1610@gmail.com", "tnosirzivqxactdm");
                client.Send(emailMessage);
                client.Disconnect(true);
            }
        }
    }
}



