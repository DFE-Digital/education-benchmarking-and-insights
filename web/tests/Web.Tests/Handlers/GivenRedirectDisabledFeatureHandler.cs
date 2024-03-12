using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Abstractions;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Routing;
using Moq;
using Web.App.Handlers;
using Xunit;

namespace Web.Tests.Handlers;

public class GivenRedirectDisabledFeatureHandler
{
    [Fact]
    public void WhenHandleDisabledFeaturesIsCalled()
    {
        var handler = new RedirectDisabledFeatureHandler();

        var actionContext = new ActionContext(
            Mock.Of<HttpContext>(),
            Mock.Of<RouteData>(),
            Mock.Of<ActionDescriptor>(),
            new ModelStateDictionary()
        );

        var actionExecutingContext = new ActionExecutingContext(
            actionContext,
            new List<IFilterMetadata>(),
            new Dictionary<string, object?>(),
            Mock.Of<Controller>()
        );

        handler.HandleDisabledFeatures(Array.Empty<string>(), actionExecutingContext);

        Assert.IsType<ForbidResult>(actionExecutingContext.Result);
    }
}