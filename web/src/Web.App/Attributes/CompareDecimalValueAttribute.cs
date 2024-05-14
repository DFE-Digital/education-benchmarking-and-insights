using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace Web.App.Attributes;

public class CompareDecimalValueAttribute(string otherProperty, Operator operatorType) : CompareAttribute(otherProperty)
{
    private new string? OtherPropertyDisplayName { get; set; }

    public Operator OperatorType { get; } = operatorType;

    public override string FormatErrorMessage(string name)
    {
        var error = OperatorType switch
        {
            Operator.EqualTo => "should be equal to",
            Operator.GreaterThan => "cannot be less than or equal to",
            Operator.GreaterThanOrEqualTo => "cannot be less than",
            Operator.LessThan => "cannot be greater than or equal to",
            Operator.LessThanOrEqualTo => "cannot be greater than",
            _ => throw new ArgumentOutOfRangeException(nameof(OperatorType))
        };

        return $"{name} {error} {(OtherPropertyDisplayName ?? OtherProperty).ToLower()}";
    }

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(OtherProperty);
        if (otherPropertyInfo == null)
        {
            return new ValidationResult($"Could not find a property named {OtherProperty}.");
        }

        if (otherPropertyInfo.GetIndexParameters().Length > 0)
        {
            throw new ArgumentException(
                $"The property {validationContext.ObjectType.FullName}.{OtherProperty} could not be found.");
        }

        var propertyValue = value as decimal?;
        var otherPropertyValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null) as decimal?;
        switch (OperatorType)
        {
            case Operator.EqualTo:
                if (Equals(value, otherPropertyValue))
                {
                    return null;
                }

                break;
            case Operator.GreaterThan:
                if (propertyValue > otherPropertyValue)
                {
                    return null;
                }

                break;
            case Operator.GreaterThanOrEqualTo:
                if (propertyValue >= otherPropertyValue)
                {
                    return null;
                }

                break;
            case Operator.LessThan:
                if (propertyValue < otherPropertyValue)
                {
                    return null;
                }

                break;
            case Operator.LessThanOrEqualTo:
                if (propertyValue <= otherPropertyValue)
                {
                    return null;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(OperatorType));
        }

        OtherPropertyDisplayName ??= GetDisplayNameForProperty(otherPropertyInfo);

        var memberNames = validationContext.MemberName != null
            ? new[] { validationContext.MemberName }
            : null;
        return new ValidationResult(FormatErrorMessage(validationContext.DisplayName), memberNames);
    }

    private string? GetDisplayNameForProperty(PropertyInfo property)
    {
        var attributes = CustomAttributeExtensions.GetCustomAttributes(property, true);
        foreach (var attribute in attributes)
        {
            if (attribute is DisplayAttribute display)
            {
                return display.GetName();
            }
        }

        return OtherProperty;
    }
}

public enum Operator
{
    EqualTo,
    GreaterThan,
    GreaterThanOrEqualTo,
    LessThan,
    LessThanOrEqualTo
}