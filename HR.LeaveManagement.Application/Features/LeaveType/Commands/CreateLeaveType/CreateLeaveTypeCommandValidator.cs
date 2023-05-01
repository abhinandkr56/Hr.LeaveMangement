using FluentValidation;
using HR.LeaveManagement.Application.Contracts.Persistence;

namespace HR.LeaveManagement.Application.Features.LeaveType.Commands.CreateLeaveType;

public class CreateLeaveTypeCommandValidator: AbstractValidator<CreateLeaveTypeCommand>
{
    private readonly ILeaveTypeRepository _leaveTypeRepository;

    public CreateLeaveTypeCommandValidator(ILeaveTypeRepository leaveTypeRepository)
    {
        _leaveTypeRepository = leaveTypeRepository;
        
        RuleFor(p => p.Name)
            .NotEmpty().WithMessage("{PropertyName} is required")
            .NotNull()
            .MaximumLength(50);
        RuleFor(p => p.DefaultDays)
            .LessThan(100).WithMessage("{PropertyName} cannot be greater than 100")
            .GreaterThan(1).WithMessage("{PropertyName} cannot be less than 1");

        RuleFor(p => p)
            .MustAsync(LeaveTypeUnique)
            .WithMessage("Name should be unique");
    }

    private async Task<bool> LeaveTypeUnique(CreateLeaveTypeCommand request, CancellationToken token)
    {
        return await _leaveTypeRepository.IsLeaveTypeUnique(request.Name);
    }
}