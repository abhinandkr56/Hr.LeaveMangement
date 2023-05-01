using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Persistence.DatabaseContext;
using Microsoft.EntityFrameworkCore;
using Shouldly;

namespace HR.LeaveManagement.Persistence.IntegrationTests;

public class HrDbContextTests
{
    private readonly HrDatabaseContext _hrDatabaseContext;

    public HrDbContextTests()
    {
        var dboptions = new DbContextOptionsBuilder<HrDatabaseContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;

        _hrDatabaseContext = new HrDatabaseContext(dboptions);
    }
    
    [Fact]
    public async void Save_SetDateCreatedValue()
    {
        var leaveType = new LeaveType()
        {
            Id = 1,
            DefaultDays = 10,
            Name = "vacation"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();
        
        
        leaveType.DateCreated.ShouldNotBeNull();
    }
    
    [Fact]
    public async Task Save_SetDateModifiedValue()
    { 
        var leaveType = new LeaveType()
        {
            Id = 1,
            DefaultDays = 10,
            Name = "vacation test"
        };

        await _hrDatabaseContext.LeaveTypes.AddAsync(leaveType);
       await _hrDatabaseContext.SaveChangesAsync();


        leaveType.DefaultDays = 11;
         _hrDatabaseContext.LeaveTypes.Update(leaveType);
        await _hrDatabaseContext.SaveChangesAsync();
        
        
        leaveType.DateModified.ShouldNotBeNull();
    }
}