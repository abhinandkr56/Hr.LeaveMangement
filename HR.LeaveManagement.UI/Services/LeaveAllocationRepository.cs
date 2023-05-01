namespace HR.LeaveManagement.UI.Services;

public class LeaveAllocationRepository
{
    private readonly HrHttpClient _hrHttpClient;

    public LeaveAllocationRepository(HrHttpClient hrHttpClient)
    {
        _hrHttpClient = hrHttpClient;
    }
    
    
}