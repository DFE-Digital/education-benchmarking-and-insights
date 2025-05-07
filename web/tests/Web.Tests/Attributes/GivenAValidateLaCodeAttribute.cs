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

public class GivenAValidateLaCodeAttribute
{
    [Theory]
    [InlineData("code", "123")]
    [InlineData("code", "")]
    [InlineData("code", null)]
    [InlineData("other", "abc")]
    public void ReturnsOkWhenValidLaCodeProvidedOrMissing(string argument, object? code)
    {
        // arrange
        var attribute = new ValidateLaCodeAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { argument, code }
        });

        // act
        attribute.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<OkResult>(context.Result);
    }

    [Theory]
    [InlineData("code", "1234")]
    [InlineData("code", "abc")]
    public void ReturnsBadRequestWhenInvalidLaCodeProvided(string argument, object? code)
    {
        // arrange
        var attribute = new ValidateLaCodeAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { argument, code }
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