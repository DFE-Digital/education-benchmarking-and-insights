using System.ComponentModel.DataAnnotations;
using Web.App.Extensions;
namespace Web.App.Attributes;

public class RequiredDependsAttribute(string otherProperty, string? otherValue = null) : CompareAttribute(otherProperty)
{
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyValue = validationContext.GetOtherPropertyValueOrThrow<string>(OtherProperty);
        var propertyValue = value?.ToString();

        // other property is not set or not the otherValue, so ignore this value
        var otherPropertyValueIsSetOrMatches = string.IsNullOrWhiteSpace(otherValue)
            ? !string.IsNullOrWhiteSpace(propertyValue)
            : otherPropertyValue == otherValue;
        if (!otherPropertyValueIsSetOrMatches)
        {
            return null;
        }

        // other property is set, but so is this one
        if (!string.IsNullOrWhiteSpace(propertyValue))
        {
            return null;
        }

        var memberNames = validationContext.MemberName != null
            ? new[]
            {
                validationContext.MemberName
            }
            : null;
        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
    }
}