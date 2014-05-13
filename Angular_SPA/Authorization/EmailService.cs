using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using System.Web;
using Microsoft.AspNet.Identity;

namespace Angular_SPA.Authorization {

      public class EmailService : IIdentityMessageService {
         
         public async Task SendAsync(IdentityMessage message) {
            // Plug in your email service here to send an email.
             MailMessage email = new MailMessage {
               Subject = message.Subject,
               Body = message.Body,
               IsBodyHtml = true
            };
            email.To.Add(message.Destination);

            using (var client = new SmtpClient()) {
               await client.SendMailAsync(email);
            }
         }
      }
}