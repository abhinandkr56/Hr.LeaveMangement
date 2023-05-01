using FluentValidation.Results;

namespace HR.LeaveManagement.Application.Exceptions;

public class BadRequestException :Exception
{
    public BadRequestException(string message): base(message)
    {
        
    }
    
    public BadRequestException(string message, ValidationResult result): base(message)
    {
        ValidationErrors = result.ToDictionary();
    }
    public IDictionary<string, string[]> ValidationErrors { get; set; }
}