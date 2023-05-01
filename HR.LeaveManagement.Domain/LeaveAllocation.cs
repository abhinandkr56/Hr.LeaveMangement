using System.ComponentModel.DataAnnotations.Schema;
using HR.LeaveManagement.Domain.Common;

namespace HR.LeaveManagement.Domain;

public class LeaveAllocation: BaseEntity
{
    public int Type { get; set; }
    public string Name { get; set; }
    public LeaveType LeaveType { get; set; }
    public int LeaveTypeId { get; set; }
    public int Period { get; set; }
    public string EmployeeId { get; set; }
}