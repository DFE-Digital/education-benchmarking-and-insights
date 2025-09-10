using System.Diagnostics;
using System.Net;
using System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Controllers;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Web.App;
using Web.App.Attributes;
using Web.App.Identity;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenASchoolAuthorizationAttribute
{
    private readonly Mock<IConfiguration> _configuration = new();
    private readonly IServiceCollection _services = new ServiceCollection();

    public GivenASchoolAuthorizationAttribute()
    {
        var listener = new DiagnosticListener("Microsoft.AspNetCore");

        _services
            .AddScoped<IConfiguration>(_ => _configuration.Object)
            .AddSingleton(listener)
            .AddSingleton<DiagnosticSource>(listener)
            .AddScoped<IActionResultExecutor<ViewResult>>(_ => Mock.Of<IActionResultExecutor<ViewResult>>());
    }

    [Theory]
    [InlineData("123456", new string[]
        { }, true, false)]
    [InlineData("123456", new[]
    {
        "123456"
    }, true, false)]
    [InlineData("123456", new[]
    {
        "654321"
    }, false, true)]
    [InlineData("123456", new string[]
        { }, false, true)]
    [InlineData("123456", new string[]
        { }, null, true)]
    [InlineData(null, new string[]
        { }, false, true)]
    public async Task ShouldReturnForbiddenIfInvalidClaims(string? urn, string[] schoolClaims, bool? disableOrganisationClaimCheck, bool forbidden)
    {
        var routeData = new RouteData(new RouteValueDictionary
        {
            { "urn", urn }
        });

        var section = Mock.Of<IConfigurationSection>();
        section.Value = disableOrganisationClaimCheck?.ToString();
        _configuration
            .Setup(c => c.GetSection(EnvironmentVariables.DisableOrganisationClaimCheck))
            .Returns(section);

        var context = await OnAuthorization(routeData, schoolClaims.Select(c => new Claim(ClaimNames.Schools, c)).ToArray());

        if (forbidden)
        {
            var result = Assert.IsType<ViewResult>(context.Result);
            Assert.Equal("../Error/Forbidden", result.ViewName);
            Assert.Equal((int)HttpStatusCode.Forbidden, result.StatusCode);
            return;
        }

        Assert.Null(context.Result);
    }

    private async Task<AuthorizationFilterContext> OnAuthorization(RouteData routeData, Claim[] claims)
    {
        var provider = _services.BuildServiceProvider();

        var httpContext = new DefaultHttpContext
        {
            RequestServices = provider,
            User = new ClaimsPrincipal(new ClaimsIdentity(claims, "mock"))
        };

        var actionContext = new ActionContext
        {
            HttpContext = httpContext,
            RouteData = routeData,
            ActionDescriptor = new ControllerActionDescriptor()
        };

        var filterContext = new AuthorizationFilterContext(
            actionContext,
            new List<IFilterMetadata>()
        );

        var attribute = new SchoolAuthorizationAttribute();
        attribute.OnAuthorization(filterContext);

        await (filterContext.Result?.ExecuteResultAsync(actionContext) ?? Task.CompletedTask);
        return filterContext;
    }
}