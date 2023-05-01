namespace HR.LeaveManagement.Application.Models;

public class RegistrationRequest
{
    public string FirstName { get; set; }
    public string Lastname { get; set; }
    public string Email { get; set; }
    public string UserName { get; set; }
    public string Password { get; set; }
}