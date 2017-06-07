using System;
using System.Net;
using System.Net.Mail;
using System.Linq;

namespace KingRichard.Communication
{
    public static class EMail
    {
        public static void SendMail(string smtpServer, string from, string password,
        string mailto, string caption, string message, string attachFile = null)
        {
            try
            {
                MailMessage mail = new MailMessage();
                mail.From = new MailAddress(from);
                mail.To.Add(new MailAddress(mailto));
                mail.Subject = caption;
                mail.Body = message;

                if (!String.IsNullOrEmpty(attachFile))
                    mail.Attachments.Add(new Attachment(attachFile));

                SmtpClient client = new SmtpClient();
                client.Host = smtpServer;
                client.Port = 587;
                client.EnableSsl = true;
                client.Credentials = new NetworkCredential(from.Split(EMailConstants.SplitSymbol).First(), password);
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.Send(mail);

                mail.Dispose();
            }
            catch (Exception e)
            {
                throw new Exception(nameof(EMail.SendMail) + e.Message);
            }
        }
    }
}
