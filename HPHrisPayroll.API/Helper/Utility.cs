using System.Net.Mail;
using System.Threading.Tasks;

namespace HPHrisPayroll.API.Helper
{
    public static class Utility
    {
        static string _smtpServer = "MailServer";
        static string _fromEmailAccount = "fromEmailAddress@email.com";
        static string _password = "password";
        static string _emailDisplayName = "Display Name";
        public static async Task SendEmail(string toEmail, string subject, string body, string ccEmail = "")
        {
            SmtpClient client = new SmtpClient(_smtpServer);

            MailMessage mail = new MailMessage();

            client.Port = 587;
            client.EnableSsl = true;
            client.Credentials = new System.Net.NetworkCredential(_fromEmailAccount, _password);
            mail.From = new MailAddress(_fromEmailAccount, _emailDisplayName);
            mail.To.Add(toEmail);
            if (!string.IsNullOrEmpty(ccEmail)) mail.CC.Add(ccEmail);
            mail.Subject = subject;
            mail.Body = body;
            mail.IsBodyHtml = true;

            await client.SendMailAsync(mail);
        }
    }
}