using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.API.Models;

public class CustomValidationProblemDetails : ProblemDetails
{
    public IDictionary<string, string[]> Errors { get; set; }
}