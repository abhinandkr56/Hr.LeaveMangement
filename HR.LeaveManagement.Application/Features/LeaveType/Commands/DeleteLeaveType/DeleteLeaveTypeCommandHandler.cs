using HR.LeaveManagement.Application.Contracts.Persistence;
using HR.LeaveManagement.Application.Exceptions;
using MediatR;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.DeleteLeaveType;

public class DeleteLeaveTypeCommandHandler : IRequestHandler<DeleteLeaveTypeCommand, Unit>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public DeleteLeaveTypeCommandHandler(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
    }
    public async Task<Unit> Handle(DeleteLeaveTypeCommand request, CancellationToken cancellationToken)
    {
        var leaveTypeToRemove = await _leaveTypeRepository.GetByIdAsync(request.id);

        if (leaveTypeToRemove is null)
        {
            throw new NotFoundException(nameof(Domain.LeaveType), request.id);
        }
        
        await _leaveTypeRepository.DeleteAsync(leaveTypeToRemove);
        
        return Unit.Value;
    }
}