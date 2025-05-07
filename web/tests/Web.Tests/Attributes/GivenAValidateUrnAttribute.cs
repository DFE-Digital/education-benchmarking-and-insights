using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Web.App.Attributes;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenAValidateUrnAttribute
{
    [Theory]
    [InlineData("urn", "123456")]
    [InlineData("urn", "")]
    [InlineData("urn", null)]
    [InlineData("other", "abc")]
    public void ReturnsOkWhenValidUrnProvidedOrMissing(string argument, object? urn)
    {
        // arrange
        var attribute = new ValidateUrnAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { argument, urn }
        });

        // act
        attribute.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<OkResult>(context.Result);
    }

    [Theory]
    [InlineData("urn", "1234567")]
    [InlineData("urn", "abcdef")]
    public void ReturnsBadRequestWhenInvalidUrnProvided(string argument, object? urn)
    {
        // arrange
        var attribute = new ValidateUrnAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { argument, urn }
        });

        // act
        attribute.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        var result = Assert.IsType<ViewResult>(context.Result);
        Assert.Equal((int)HttpStatusCode.BadRequest, result.StatusCode);
    }

    private static ActionExecutingContext BuildContext(Dictionary<string, object?> actionArguments)
    {
        return new ActionExecutingContext(
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
    }
}