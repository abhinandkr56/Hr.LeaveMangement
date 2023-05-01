using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Queries.GetAllLeaveTypes;

public record GetAllLeaveTypesQuery : IRequest<List<LeaveTypeDto>>
{
    
}