using System.ComponentModel.DataAnnotations;
using System.Reflection;
namespace Web.App.Extensions;

public static class ValidationContextExtensions
{
    public static PropertyInfo GetOtherPropertyOrThrow(this ValidationContext validationContext, string otherProperty)
    {
        var otherPropertyInfo = validationContext.ObjectType.GetRuntimeProperty(otherProperty);
        if (otherPropertyInfo == null)
        {
            throw new ValidationException($"Could not find a property named {otherProperty}.");
        }

        if (otherPropertyInfo.GetIndexParameters().Length > 0)
        {
            throw new ArgumentException(
                $"The property {validationContext.ObjectType.FullName}.{otherProperty} could not be found.");
        }

        return otherPropertyInfo;
    }

    public static T? GetOtherPropertyValueOrThrow<T>(this ValidationContext validationContext, string otherProperty) where T : class
    {
        var otherPropertyInfo = validationContext.GetOtherPropertyOrThrow(otherProperty);
        return otherPropertyInfo.GetValue(validationContext.ObjectInstance, null) as T;
    }
}