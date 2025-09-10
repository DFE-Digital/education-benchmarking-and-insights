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

public class GivenAValidateIdAttribute
{
    private readonly Mock<IValidator<OrganisationIdentifier>> _validator = new();

    [Theory]
    [InlineData("id", "123456", "type", OrganisationTypes.School)]
    [InlineData("id", "", "type", OrganisationTypes.School)]
    [InlineData("id", null, "type", OrganisationTypes.School)]
    [InlineData("id", "12345678", "type", OrganisationTypes.Trust)]
    [InlineData("id", "", "type", OrganisationTypes.Trust)]
    [InlineData("id", "123", "type", OrganisationTypes.LocalAuthority)]
    [InlineData("id", "", "type", OrganisationTypes.LocalAuthority)]
    [InlineData("id", null, "type", OrganisationTypes.LocalAuthority)]
    [InlineData("other", "abc", "type", OrganisationTypes.School)]
    public void ReturnsOkWhenValidIdAndTypeProvidedOrMissing(string idArgument, string? id, string typeArgument, string? type)
    {
        // arrange
        var validationResult = new ValidationResult();
        _validator.Setup(v => v.Validate(It.Is<OrganisationIdentifier>(i => i.Value == id && i.Type == type)))
            .Returns(validationResult);

        var (filter, context) = BuildFilterAndContext(idArgument, id, typeArgument, type);

        // act
        filter.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<OkResult>(context.Result);
    }

    [Theory]
    [InlineData("id", "123456", "type", "other")]
    public void ReturnsNotFoundWhenValidationFails(string idArgument, string? id, string typeArgument, string? type)
    {
        // arrange
        var validationResult = new ValidationResult([new ValidationFailure(idArgument, "Value is invalid")]);
        _validator.Setup(v => v.Validate(It.Is<OrganisationIdentifier>(i => i.Value == id && i.Type == type)))
            .Returns(validationResult);

        var (filter, context) = BuildFilterAndContext(idArgument, id, typeArgument, type);

        // act
        filter.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<NotFoundResult>(context.Result);
    }

    private (ValidateArgumentFilter filter, ActionExecutingContext context) BuildFilterAndContext(string idArgument, string? id, string typeArgument, string? type)
    {
        var actionArguments = new Dictionary<string, object?>
        {
            { idArgument, id },
            { typeArgument, type }
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

        var attribute = new ValidateIdAttribute();
        var filter = new ValidateArgumentFilter(
            new NullLogger<ValidateArgumentFilter>(),
            _validator.Object,
            attribute.ArgumentName,
            attribute.Type,
            attribute.TypeName);

        return (filter, context);
    }
}