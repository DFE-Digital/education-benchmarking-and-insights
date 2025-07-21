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

public class GivenATrustAuthorizationAttribute
{
    private readonly Mock<IConfiguration> _configuration = new();
    private readonly IServiceCollection _services = new ServiceCollection();

    public GivenATrustAuthorizationAttribute()
    {
        var listener = new DiagnosticListener("Microsoft.AspNetCore");

        _services
            .AddScoped<IConfiguration>(_ => _configuration.Object)
            .AddSingleton(listener)
            .AddSingleton<DiagnosticSource>(listener)
            .AddScoped<IActionResultExecutor<ViewResult>>(_ => Mock.Of<IActionResultExecutor<ViewResult>>());
    }

    [Theory]
    [InlineData("12345678", new string[] { }, true, false)]
    [InlineData("12345678", new[] { "12345678" }, true, false)]
    [InlineData("123456", new[] { "12345678" }, true, false)]
    [InlineData("12345678", new[] { "87654321" }, false, true)]
    [InlineData("12345687", new string[] { }, false, true)]
    [InlineData("12345678", new string[] { }, null, true)]
    [InlineData(null, new string[] { }, false, true)]
    public async Task ShouldReturnForbiddenIfInvalidClaims(string? companyNumber, string[] trustClaims, bool? disableOrganisationClaimCheck, bool forbidden)
    {
        var routeData = new RouteData(new RouteValueDictionary
        {
            {
                "companyNumber", companyNumber
            }
        });

        var section = Mock.Of<IConfigurationSection>();
        section.Value = disableOrganisationClaimCheck?.ToString();
        _configuration
            .Setup(c => c.GetSection(EnvironmentVariables.DisableOrganisationClaimCheck))
            .Returns(section);

        var context = await OnAuthorization(routeData, trustClaims.Select(c => new Claim(ClaimNames.Trusts, c)).ToArray());

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

        var attribute = new TrustAuthorizationAttribute();
        attribute.OnAuthorization(filterContext);

        await (filterContext.Result?.ExecuteResultAsync(actionContext) ?? Task.CompletedTask);
        return filterContext;
    }
}