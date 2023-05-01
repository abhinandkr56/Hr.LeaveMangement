using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.Repositories;

public class LeaveAllocationRepository : GenericRepository<LeaveAllocation> , ILeaveAllocationRepository
{
    public LeaveAllocationRepository(HrDatabaseContext context) : base(context)
    {
    }

    public async Task<LeaveAllocation> GetLeaveAllocationWithDetails(int id)
    {
        return await _context.LeaveAllocations
            .Include(x=>x.LeaveType)
            .FirstOrDefaultAsync(x => x.Id == id);
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails()
    {
        return await _context.LeaveAllocations
            .Include(x => x.LeaveType)
            .ToListAsync();
    }

    public async Task<List<LeaveAllocation>> GetLeaveAllocationWithDetails(string userId)
    {
        return await _context.LeaveAllocations.Where(x => x.EmployeeId == userId)
            .Include(x=>x.LeaveType).ToListAsync();
    }

    public async Task<bool> AllocationExists(string userId, int leaveTypeId, int period)
    {
        return await _context.LeaveAllocations.AnyAsync(q => q.EmployeeId == userId
                                                             && q.LeaveTypeId == leaveTypeId
                                                             && q.Period == period);
    }

    public async Task AddAllocations(List<LeaveAllocation> leaveAllocations)
    {
        await _context.AddRangeAsync(leaveAllocations);
    }

    public async Task<LeaveAllocation> GetUserAllocation(string userId, int leaveTypeId)
    {
        return await _context.LeaveAllocations.FirstOrDefaultAsync(x =>
            x.EmployeeId == userId && x.LeaveTypeId == leaveTypeId);
    }
}