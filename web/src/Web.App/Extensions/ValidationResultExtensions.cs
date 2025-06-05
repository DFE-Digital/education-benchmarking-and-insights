using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Web.App.Extensions;

public static class ValidationResultExtensions
{
    // see https://docs.fluentvalidation.net/en/latest/aspnet.html#manual-validation and
    // https://github.com/FluentValidation/FluentValidation/issues/1959 for commentary
    // around the deprecation of the `FluentValidation.AspNetCore` package
    public static void AddToModelState(this ValidationResult result, ModelStateDictionary modelState)
    {
        foreach (var error in result.Errors)
        {
            modelState.AddModelError(error.PropertyName, error.ErrorMessage);
        }
    }
}