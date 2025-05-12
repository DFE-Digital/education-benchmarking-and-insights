using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging.Abstractions;
using Moq;
using Web.App;
using Web.App.Attributes;
using Web.App.Validators;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenAValidateUrnAttribute
{
    private readonly Mock<IValidator<OrganisationIdentifier>> _validator = new();

    [Theory]
    [InlineData("urn", "123456")]
    [InlineData("urn", "")]
    [InlineData("urn", null)]
    [InlineData("other", "abc")]
    public void ReturnsOkWhenValidUrnProvidedOrMissing(string argument, string? urn)
    {
        // arrange
        var validationResult = new ValidationResult();
        _validator.Setup(v => v.Validate(It.Is<OrganisationIdentifier>(i => i.Value == urn && i.Type == OrganisationTypes.School)))
            .Returns(validationResult);

        var (filter, context) = BuildFilterAndContext(argument, urn);

        // act
        filter.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<OkResult>(context.Result);
    }

    [Theory]
    [InlineData("urn", "invalid")]
    public void ReturnsNotFoundWhenUrnValidationFails(string argument, string? urn)
    {
        // arrange
        var validationResult = new ValidationResult([new ValidationFailure("code", "LA code is invalid")]);
        _validator.Setup(v => v.Validate(It.Is<OrganisationIdentifier>(i => i.Value == urn && i.Type == OrganisationTypes.School)))
            .Returns(validationResult);

        var (filter, context) = BuildFilterAndContext(argument, urn);

        // act
        filter.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<NotFoundResult>(context.Result);
    }

    private (ValidateArgumentFilter filter, ActionExecutingContext context) BuildFilterAndContext(string argument, object? companyNumber)
    {
        var actionArguments = new Dictionary<string, object?>
        {
            { argument, companyNumber }
        };

        var context = new ActionExecutingContext(
            new ActionContext(
                Mock.Of<HttpContext>(),
                Mock.Of<RouteData>(),
                Mock.Of<ActionDescriptor>(),
                Mock.Of<ModelStateDictionary>()
            ),
            new List<IFilterMetadata>(),
            actionArguments,
            Mock.Of<Controller>())
        {
            Result = new OkResult()
        };

        var attribute = new ValidateUrnAttribute();
        var filter = new ValidateArgumentFilter(
            new NullLogger<ValidateArgumentFilter>(),
            _validator.Object,
            attribute.ArgumentName,
            attribute.Type,
            attribute.TypeName);

        return (filter, context);
    }
}