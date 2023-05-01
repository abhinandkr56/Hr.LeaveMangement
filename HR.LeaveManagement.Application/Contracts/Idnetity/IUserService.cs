
using HR.LeaveManagement.Application.Models;

namespace HR.LeaveManagement.Application.Contracts.Idnetity;

public interface IUserService
{
    Task<List<Employee>> GetEmployees();
    Task<Employee> GetEmployee(string userId);
}