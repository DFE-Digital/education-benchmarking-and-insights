using System.Diagnostics.CodeAnalysis;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Platform.Functions;

[ExcludeFromCodeCoverage]
public class ValidationErrorsResult : BadRequestObjectResult
{
    public ValidationErrorsResult(IEnumerable<ValidationFailure> failures) : base(failures.Select(e => new ValidationError(e.Severity, e.PropertyName, e.ErrorMessage)))
    {

    }
}