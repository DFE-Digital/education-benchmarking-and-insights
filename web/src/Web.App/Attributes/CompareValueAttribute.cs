using System.ComponentModel.DataAnnotations;
using System.Reflection;
using Web.App.Extensions;
namespace Web.App.Attributes;

public abstract class CompareValueAttribute<T>(string otherProperty, Operator operatorType, string errorFormatString)
    : CompareAttribute(otherProperty)
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

        return string.Format(errorFormatString, name, error, (OtherPropertyDisplayName ?? OtherProperty).ToLower());
    }

    protected abstract bool GreaterThan(T? propertyValue, T? otherPropertyValue);
    protected abstract bool GreaterThanOrEqualTo(T? propertyValue, T? otherPropertyValue);
    protected abstract bool LessThan(T? propertyValue, T? otherPropertyValue);
    protected abstract bool LessThanOrEqualTo(T? propertyValue, T? otherPropertyValue);

    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        if (value == null)
        {
            return null;
        }

        var otherPropertyInfo = validationContext.GetOtherPropertyOrThrow(OtherProperty);
        var propertyValue = value is T ? (T)value : default;
        var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance, null);
        var otherPropertyValue = otherValue is T ? (T)otherValue : default;
        switch (OperatorType)
        {
            case Operator.EqualTo:
                if (Equals(value, otherPropertyValue))
                {
                    return null;
                }

                break;
            case Operator.GreaterThan:
                if (GreaterThan(propertyValue, otherPropertyValue))
                {
                    return null;
                }

                break;
            case Operator.GreaterThanOrEqualTo:
                if (GreaterThanOrEqualTo(propertyValue, otherPropertyValue))
                {
                    return null;
                }

                break;
            case Operator.LessThan:
                if (LessThan(propertyValue, otherPropertyValue))
                {
                    return null;
                }

                break;
            case Operator.LessThanOrEqualTo:
                if (LessThanOrEqualTo(propertyValue, otherPropertyValue))
                {
                    return null;
                }

                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(OperatorType));
        }

        OtherPropertyDisplayName ??= GetDisplayNameForProperty(otherPropertyInfo);

        var memberNames = validationContext.MemberName != null
            ? new[]
            {
                validationContext.MemberName
            }
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