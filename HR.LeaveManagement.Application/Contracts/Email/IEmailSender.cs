namespace HR.LeaveManagement.Application.Contracts.Email;

public interface IEmailSender
{
    Task<bool> SendEmail(Models.EmailMessage emailMessage);
}