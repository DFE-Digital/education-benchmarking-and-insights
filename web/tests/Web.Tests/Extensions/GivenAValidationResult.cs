using AutoFixture;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.App.Extensions;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAValidationResult
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ShouldAddErrorsToModelState()
    {
        var modelState = new ModelStateDictionary();

        var errors = _fixture.Build<ValidationFailure>().CreateMany().ToList();
        var validationResult = new ValidationResult
        {
            Errors = errors
        };

        validationResult.AddToModelState(modelState);

        Assert.Equal(errors.Count, modelState.ErrorCount);
        foreach (var validationFailure in errors)
        {
            Assert.True(modelState.HasError(validationFailure.PropertyName));
            Assert.Contains(validationFailure.ErrorMessage, modelState[validationFailure.PropertyName]?.Errors.Select(e => e.ErrorMessage) ?? []);
        }
    }
}