using HR.LeaveManagement.UI.Models;
using HR.LeaveManagement.UI.Services.Base;

namespace HR.LeaveManagement.UI.Contracts;

public interface ILeaveTypeRepository
{
    Task<Response<List<LeaveTypeDto>>> GetLeaveTypes();

    Task<Response<LeaveTypeDto>> GetLeaveTypeDetails(int id);

    Task<Response<Guid>> CreateLeaveType(LeaveTypeDto leaveType);
    
    Task<Response<Guid>> UpdateLeaveType(LeaveTypeDto leaveType);
    
    Task<Response<Guid>> DeleteLeaveType(int Id);
    
}