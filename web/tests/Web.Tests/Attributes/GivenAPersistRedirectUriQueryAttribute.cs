using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Primitives;
using Web.App.Attributes;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenAPersistRedirectUriQueryAttribute
{
    private static ActionExecutingContext CreateActionExecutingContext(Dictionary<string, StringValues>? query = null)
    {
        var httpContext = new DefaultHttpContext();
        if (query != null)
        {
            var builder = new QueryBuilder(query.Select(kv => new KeyValuePair<string, string?>(kv.Key, kv.Value.ToString()))!);
            httpContext.Request.QueryString = new QueryString(builder.ToQueryString().ToString());
        }

        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();
        return new ActionExecutingContext(
            new ActionContext(httpContext, routeData, actionDescriptor),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            null!);
    }

    private static ResultExecutingContext CreateResultExecutingContext(HttpContext httpContext, IActionResult result)
    {
        var routeData = new RouteData();
        var actionDescriptor = new ActionDescriptor();

        return new ResultExecutingContext(
            new ActionContext(httpContext, routeData, actionDescriptor),
            new List<IFilterMetadata>(),
            result,
            null!);
    }

    [Fact]
    public void SetsRedirectUriInViewDataForViewResult()
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        const string redirectUri = "redirect-uri";
        var actionExecutingContext = CreateActionExecutingContext(new Dictionary<string, StringValues>
        {
            [PersistRedirectUriQueryAttribute.RedirectUriQuery] = redirectUri
        });
        var view = new ViewResult
        {
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        };

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(actionExecutingContext.HttpContext, view);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        Assert.True(view.ViewData.TryGetValue(PersistRedirectUriQueryAttribute.RedirectUriQuery, out var value));
        Assert.Equal(redirectUri, value);
    }

    [Fact]
    public void SetsRedirectUriInRouteValuesForRedirectToActionResult()
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        const string redirectUri = "redirect-uri";
        var actionExecutingContext = CreateActionExecutingContext(new Dictionary<string, StringValues>
        {
            [PersistRedirectUriQueryAttribute.RedirectUriQuery] = redirectUri
        });
        var redirect = new RedirectToActionResult("Action", "Controller", null);

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(actionExecutingContext.HttpContext, redirect);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        Assert.NotNull(redirect.RouteValues);
        Assert.True(redirect.RouteValues!.TryGetValue(PersistRedirectUriQueryAttribute.RedirectUriQuery, out var value));
        Assert.Equal(redirectUri, value);
    }

    [Fact]
    public void SetsRedirectUriInRouteValuesForRedirectToRouteResult()
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        const string redirectUri = "redirect-uri";
        var actionExecutingContext = CreateActionExecutingContext(new Dictionary<string, StringValues>
        {
            [PersistRedirectUriQueryAttribute.RedirectUriQuery] = redirectUri
        });
        var redirect = new RedirectToRouteResult("default", null);

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(actionExecutingContext.HttpContext, redirect);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        Assert.NotNull(redirect.RouteValues);
        Assert.True(redirect.RouteValues!.TryGetValue(PersistRedirectUriQueryAttribute.RedirectUriQuery, out var value));
        Assert.Equal(redirectUri, value);
    }

    [Theory]
    [InlineData("https://example.com/path", "redirect-uri", "https://example.com/path?redirectUri=redirect-uri")]
    [InlineData("/relative/path?exists=1", "redirect-uri", "/relative/path?exists=1&redirectUri=redirect-uri")]
    public void MergesRedirectUriInQueryStringForRedirectResult(string baseUrl, string redirectUri, string expected)
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                QueryString = new QueryString($"?{PersistRedirectUriQueryAttribute.RedirectUriQuery}={redirectUri}")
            }
        };
        var redirect = new RedirectResult(baseUrl, true, true);

        var actionExecutingContext = new ActionExecutingContext(
            new ActionContext(httpContext, new RouteData(), new ActionDescriptor()),
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            null!);

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(httpContext, redirect);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        var updated = Assert.IsType<RedirectResult>(resultExecutingContext.Result);
        Assert.Equal(expected, updated.Url);
        Assert.True(updated.Permanent);
        Assert.True(updated.PreserveMethod);
    }

    [Fact]
    public void DoesNotSetViewDataWhenRedirectUriMissing()
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        var actionExecutingContext = CreateActionExecutingContext();
        var view = new ViewResult
        {
            ViewData = new ViewDataDictionary(new EmptyModelMetadataProvider(), new ModelStateDictionary())
        };

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(actionExecutingContext.HttpContext, view);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        Assert.False(view.ViewData.ContainsKey(PersistRedirectUriQueryAttribute.RedirectUriQuery));
    }

    [Fact]
    public void DoesNotUpdateRouteWhenRedirectUriMissing()
    {
        // arrange
        var attribute = new PersistRedirectUriQueryAttribute();
        var actionExecutingContext = CreateActionExecutingContext();
        var redirect = new RedirectToActionResult("Action", "Controller", null);

        // act
        attribute.OnActionExecuting(actionExecutingContext);
        var resultExecutingContext = CreateResultExecutingContext(actionExecutingContext.HttpContext, redirect);
        attribute.OnResultExecuting(resultExecutingContext);

        // assert
        Assert.Null(redirect.RouteValues);
    }
}