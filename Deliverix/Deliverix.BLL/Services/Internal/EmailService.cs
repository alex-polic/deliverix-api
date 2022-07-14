using System.Net;
using System.Net.Mail;
using Deliverix.BLL.Contracts.Internal;

namespace Deliverix.BLL.Services.Internal;

public class EmailService : IEmailService
{
    public void SendEmailNotification(string messageBody, string email)
    {
        var fromAddress = new MailAddress("alpolic8@gmail.com", "Deliverix");
        var toAddress = new MailAddress(email);
        const string fromPassword = "rlvjysknepfvmfsc";
        const string subject = "Deliverix notification";

        var smtp = new SmtpClient
        {
            Host = "smtp.gmail.com",
            EnableSsl = true,
            Port = 587,
            DeliveryMethod = SmtpDeliveryMethod.Network,
            UseDefaultCredentials = false,
            Credentials = new NetworkCredential(fromAddress.Address, fromPassword)
        };
        using (var message = new MailMessage(fromAddress, toAddress)
               {
                   Subject = subject,
                   Body = messageBody
               })
        {
            smtp.Send(message);
        }
    }
}