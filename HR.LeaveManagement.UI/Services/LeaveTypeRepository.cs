using HR.LeaveManagement.UI.Contracts;
using HR.LeaveManagement.UI.Models;
using HR.LeaveManagement.UI.Services.Base;

namespace HR.LeaveManagement.UI.Services;

public class LeaveTypeRepository:ILeaveTypeRepository
{
    private readonly IHrHttpClient _hrHttpClient;

    public LeaveTypeRepository(IHrHttpClient hrHttpClient)
    {
        _hrHttpClient = hrHttpClient;
    }
    
    public async Task<Response<List<LeaveTypeDto>>> GetLeaveTypes()
    {
        return await _hrHttpClient.GetAsync<List<LeaveTypeDto>>("api/LeaveTypes");
    }

    public async Task<Response<LeaveTypeDto>> GetLeaveTypeDetails(int id)
    {
        return await _hrHttpClient.GetAsync<LeaveTypeDto>("api/get-by-id?id="+id);
    }

    public async Task<Response<Guid>> CreateLeaveType(LeaveTypeDto leaveType)
    {
        return await _hrHttpClient.PostAsync<Guid>("api/LeaveTypes",leaveType);
    }

    public async Task<Response<Guid>> UpdateLeaveType(LeaveTypeDto leaveType)
    {
        return await _hrHttpClient.PutAsync<Guid>("api/LeaveTypes",leaveType);
    }

    public async Task<Response<Guid>> DeleteLeaveType(int Id)
    {
        return await _hrHttpClient.DeletAsync<Guid>("api/LeaveTypes?id="+Id);
    }
}