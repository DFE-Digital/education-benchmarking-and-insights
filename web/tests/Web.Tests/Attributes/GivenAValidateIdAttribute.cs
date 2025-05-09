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

public class GivenAValidateIdAttribute
{
    [Theory]
    [InlineData("id", "123456", "type", "school")]
    [InlineData("id", "", "type", "school")]
    [InlineData("id", null, "type", "school")]
    [InlineData("id", "12345678", "type", "trust")]
    [InlineData("id", "", "type", "trust")]
    [InlineData("id", null, "type", "trust")]
    [InlineData("id", "123", "type", "local-authority")]
    [InlineData("id", "", "type", "local-authority")]
    [InlineData("id", null, "type", "local-authority")]
    [InlineData("other", "abc", "type", "school")]
    public void ReturnsOkWhenValidIdAndTypeProvidedOrMissing(string idArgument, object? id, string typeArgument, object? type)
    {
        // arrange
        var attribute = new ValidateIdAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { idArgument, id },
            { typeArgument, type }
        });

        // act
        attribute.OnActionExecuting(context);

        // assert
        Assert.NotNull(context.Result);
        Assert.IsType<OkResult>(context.Result);
    }

    [Theory]
    [InlineData("id", "1234567", "type", "school")]
    [InlineData("id", "123456789", "type", "trust")]
    [InlineData("id", "1234", "type", "local-authority")]
    [InlineData("id", "123456", "type", "other")]
    public void ReturnsBadRequestWhenInvalidIdOrTypeProvided(string idArgument, object? id, string typeArgument, object? type)
    {
        // arrange
        var attribute = new ValidateIdAttribute();
        var context = BuildContext(new Dictionary<string, object?>
        {
            { idArgument, id },
            { typeArgument, type }
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