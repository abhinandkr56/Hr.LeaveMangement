using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveRequestRepository : GenericRepository<LeaveRequest>, ILeaveRequestRepository
{
    public LeaveRequestRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveRequest> GetLeaveRequestWithDetails(int id)
    {
        return await _context.LeaveRequests
            .Include(q => q.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveRequest>> GetLeaveRequestWithDetails()
    {
        return await _context.LeaveRequests.Include(x => x.LeaveType).ToListAsync();
    }

    public async Task<List<LeaveRequest>> GetUserLeaveRequestWithDetails(string userId)
    {
        return await _context.LeaveRequests.Where(x => x.RequestingEmployeeId == userId)
            .Include(x => x.LeaveType)
            .ToListAsync();
    }
}