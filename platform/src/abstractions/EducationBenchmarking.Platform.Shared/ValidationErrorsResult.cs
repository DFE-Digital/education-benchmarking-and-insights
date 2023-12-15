using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Platform.Shared;

public class ValidationErrorsResult : BadRequestObjectResult
{
    public ValidationErrorsResult(IEnumerable<ValidationFailure> failures) : base(failures.Select(e => new ValidationError(e.Severity, e.PropertyName, e.ErrorMessage)))
    {
            
    }
}