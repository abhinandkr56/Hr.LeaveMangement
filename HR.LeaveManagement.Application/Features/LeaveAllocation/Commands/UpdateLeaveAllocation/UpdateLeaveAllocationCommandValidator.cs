using FluentValidation;

namespace HR.LeaveManagement.Application.Features.LeaveAllocation.Commands.UpdateLeaveAllocation;

public class UpdateLeaveAllocationCommandValidator:AbstractValidator<UpdateLeaveAllocationCommand>
{
    public UpdateLeaveAllocationCommandValidator()
    {
        RuleFor(x => x.NumberOfDays)
            .GreaterThan(0)
            .WithMessage("{PropertyName} must greater than {ComparisonValue}");
        RuleFor(p => p.Period).GreaterThan(DateTime.Now.Year)
            .WithMessage("{PropertyName} should be after {ComparisonValue}");
        
    }
}