// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Net;
// using System.Net.Mail;
// using System.Threading.Tasks;

// namespace BankingSystem.Repository
// {
//     public class EmailRepository : IEmailService
//     {
//         private readonly string _smtpServer = "smtp.mailtrap.io"; // Mailtrap SMTP server
//         private readonly int _smtpPort = 587; // Mailtrap SMTP port
//         private readonly string _smtpUser = "your_mailtrap_username"; // Mailtrap username
//         private readonly string _smtpPass = "your_mailtrap_password"; // Mailtrap password

//         public async Task SendEmailAsync(IEnumerable<string> to, IEnumerable<string> from, string subject, string body)
//         {
//             var smtpClient = new SmtpClient(_smtpServer)
//             {
//                 Port = _smtpPort,
//                 Credentials = new NetworkCredential(_smtpUser, _smtpPass),
//                 EnableSsl = true,
//             };

//             var mailMessage = new MailMessage
//             {
//                 Subject = subject,
//                 Body = body,
//                 IsBodyHtml = true,
//             };

//             // Add "from" addresses
//             foreach (var fromAddress in from)
//             {
//                 mailMessage.From = new MailAddress(fromAddress);
//             }

//             // Add "to" addresses
//             foreach (var toAddress in to)
//             {
//                 mailMessage.To.Add(toAddress);
//             }

//             await smtpClient.SendMailAsync(mailMessage);
//         }
//     }
// }