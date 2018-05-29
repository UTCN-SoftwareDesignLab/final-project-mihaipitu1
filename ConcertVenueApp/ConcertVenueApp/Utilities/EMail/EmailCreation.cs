using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;

namespace ConcertVenueApp.Utilities.EMail
{
    public static class EmailCreation
    {
        public static void SendEMail(string attachment,string toEmail)
        {
            try
            {
                MailMessage email = new MailMessage("mihai.pitu13@gmail.com", toEmail);
                SmtpClient client = new SmtpClient("smtp.gmail.com");
                email.Subject = "Ticket Purchase";
                Attachment attach = new Attachment(attachment);
                email.Attachments.Add(attach);
                email.Body = "Congratulations for purchasing!\nAttached to this e-mail are your tickets in downloadable format.\n Have fun and have a nice day! \n\n Best regards,\nAdmin";

                client.Port = 587;
                client.Credentials = new System.Net.NetworkCredential("user", "pass");
                client.Send(email);
                email.Dispose();
                client.Dispose();
            }
            catch(Exception e)
            {
                return;
            }
        }
    }
}
