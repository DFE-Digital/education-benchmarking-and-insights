using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Web.App.Infrastructure.Apis;

[ExcludeFromCodeCoverage]
public class ValidationError(Severity severity, string propertyName, string errorMessage)
{
    public Severity Severity { get; } = severity;
    public string PropertyName { get; } = propertyName;
    public string ErrorMessage { get; } = errorMessage;
}