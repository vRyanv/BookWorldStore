using System.Net.Mail;
using System.Net;

namespace BookWorldStore.Helper
{
    public class MailHelper
    {
        public async Task SendEmail(string recipient, string subject, string message)
        {
            var fromAddress = new MailAddress("khangok1610@gmail.com", "Sender");
            var toAddress = new MailAddress(recipient, "Recipient");
            var smtp = new SmtpClient
            {
                Host = "https://mail.google.com/",
                Port = 587,
                EnableSsl = true,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential("khangok1610@gmail.com", "Kt21082002")
            };
            using (var mailMessage = new MailMessage(fromAddress, toAddress)
            {
                Subject = subject,
                Body = message
            })
            {
                await smtp.SendMailAsync(mailMessage);
            }
        }
    }
}



