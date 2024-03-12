using System.Diagnostics.CodeAnalysis;
using FluentValidation;

namespace Platform.Functions;

[ExcludeFromCodeCoverage]
public class ValidationError
{
    public Severity Severity { get; }
    public string PropertyName { get; }
    public string ErrorMessage { get; }

    public ValidationError(Severity severity, string propertyName, string errorMessage)
    {
        Severity = severity;
        PropertyName = propertyName;
        ErrorMessage = errorMessage;
    }
}