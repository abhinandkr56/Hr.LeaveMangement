using HR.LeaveManagement.Domain;
using HR.LeaveManagement.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace HR.LeaveManagement.Persistence.DatabaseContext;

public class HrDatabaseContext : DbContext
{
    public HrDatabaseContext(DbContextOptions<HrDatabaseContext> options): base(options)
    {
        
    }
    
    public DbSet<LeaveType> LeaveTypes { get; set; }
    public DbSet<LeaveAllocation> LeaveAllocations { get; set; }
    public DbSet<LeaveRequest> LeaveRequests { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(HrDatabaseContext).Assembly);
        base.OnModelCreating(modelBuilder);
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        foreach (var entry in base.ChangeTracker.Entries<BaseEntity>()
                     .Where(x=> x.State ==  EntityState.Added || x.State == EntityState.Modified))
        {
            entry.Entity.DateModified = DateTime.Now;
            if (entry.State == EntityState.Added)
            {
                entry.Entity.DateCreated = DateTime.Now;
            }
        }
        return base.SaveChangesAsync(cancellationToken);
    }
}