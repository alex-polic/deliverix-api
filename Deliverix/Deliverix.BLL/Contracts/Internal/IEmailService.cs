namespace Deliverix.BLL.Contracts.Internal;

public interface IEmailService
{
    void SendEmailNotification(string messageBody, string email);
}