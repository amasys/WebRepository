using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;

namespace PanArabInternationalApp.EmailConfig
{
    public class CustomizeMessageSentToEmail
    {

      
        public void SentMail(Exception ex)
        {
            string tempName = ex.GetBaseException().ToString();
            MailMessage mail = new MailMessage();
            mail.Subject = "Pan Arab International ";
            mail.Body = tempName;
            Mail(mail);

        }

        private void Mail(MailMessage mail)
        {
            try
            {

                SmtpClient SmtpServer = new SmtpClient("smtp.gmail.com");

                mail.From = new MailAddress("PanArab@gmail.com");
                mail.To.Add("demo.iiuc2017@gmail.com");

                SmtpServer.Port = 587;
                SmtpServer.Credentials = new System.Net.NetworkCredential("demo.iiuc2017@gmail.com", "samsung123");
                SmtpServer.EnableSsl = true;

                SmtpServer.Send(mail);

            }
            catch (Exception ex)
            {

            }
        }
    }
}