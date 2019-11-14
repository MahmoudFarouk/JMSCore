using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace JMS.BLL.Common
{
    public static class EmailsHandler
    {
        public static void SendEmail(MailMessage mail)
        {
            try
            {
                mail.From = new MailAddress("y7oda@hotmail.com");

                using (SmtpClient mailer = new SmtpClient("smtp.live.com", 587))
                {
                    mailer.UseDefaultCredentials = false;
                    mailer.Credentials = new NetworkCredential("y7oda@hotmail.com", "vhUJ^~+b+pBv","hotmail.com");
                    mailer.EnableSsl = true;
                    mailer.Send(mail);
                }
            }
            catch (Exception ex)
            {
                ExceptionLogger.LogException(ex);
            }
        }
    }
}
