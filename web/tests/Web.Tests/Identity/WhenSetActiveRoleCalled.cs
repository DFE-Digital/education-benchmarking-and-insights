using System.Security.Claims;
using Web.Identity;
using Web.Identity.Extensions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Moq;
using Xunit;

namespace Web.Tests.Identity;

public class WhenSetActiveRoleCalledWithOneActiveRoleInClaims : SetActiveRoleTestBase
{
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly HttpContext _httpContext;

    public WhenSetActiveRoleCalledWithOneActiveRoleInClaims()
    {
        var roles = new List<Claim>
        {
            new(ClaimNames.ActiveRole, RoleNames.SomeUserRole),
            new(ClaimNames.ActiveRole, "claim1"),
        };
        roles.AddRange(Operations.GetAllRoles().Select(r => new Claim(ClaimTypes.Role, r.Name)));
        _claimsPrincipal = new ClaimsPrincipal(
            new List<ClaimsIdentity>
            {
                new(roles)
            });

        _httpContext = BuildContext();
    }

    private async Task<bool> DoIt()
    {
        return await _claimsPrincipal.SetActiveRole(_httpContext, RoleNames.SomeUserRole);
    }

    [Fact]
    public async Task ShouldNotThrowAnyExceptions()
    {
        var response = await DoIt();

        Assert.True(true);
    }
    [Fact]
    public async Task ShouldReturnTrue()
    {
        var response = await DoIt();

        Assert.True(response);
    }

    [Fact]
    public async Task ShouldSetActiveRoleToThePassedRole()
    {
        var response = await DoIt();

        Assert.Equal(RoleNames.SomeUserRole, _claimsPrincipal.ActiveRole().Name);
    }

    [Fact]
    public async Task ShouldRemoveOtherActiveRoles()
    {
        var currentActivePrincipals = _claimsPrincipal.Claims
            .Where(c => c.Type == ClaimNames.ActiveRole).ToList();

        Assert.Equal(2, currentActivePrincipals.ToList().Count);

        var response = await DoIt();

        var updatedActivePrincipals = _claimsPrincipal.Claims
            .Where(c => c.Type == ClaimNames.ActiveRole).ToList();

        Assert.Single(updatedActivePrincipals);
        Assert.Equal("some_role_name", updatedActivePrincipals.First().Value);
    }

    [Fact]
    public async Task ShouldSetClaimPrincipalToHaveOnlyOneActiveRole()
    {
        var response = await DoIt();

        Assert.True(_claimsPrincipal.DoesHaveOneActiveRole());
    }

    [Fact]
    public async Task ShouldRefreshOperation()
    {
        var operationClaimsPrior = _claimsPrincipal.Claims.Where(c => c.Type == ClaimNames.Operation);

        Assert.Empty(operationClaimsPrior.ToList());

        await DoIt();

        var operationClaims = _claimsPrincipal.Claims.Where(c => c.Type == ClaimNames.Operation);

        Assert.Single(operationClaims.ToList());
    }
    [Fact]
    public async Task ShouldCallHttpContextSignInAsync()
    {
        var response = await DoIt();

        AuthenticationService.Verify(
            _ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(),
                It.IsAny<AuthenticationProperties>()),
            Times.Once);
    }
}

public class WhenSetActiveRoleCalledWithInvalidRole : SetActiveRoleTestBase
{
    private readonly ClaimsPrincipal _claimsPrincipal;
    private readonly HttpContext _httpContext;

    public WhenSetActiveRoleCalledWithInvalidRole()
    {
        var roles = new List<Claim>
        {
            new(ClaimNames.ActiveRole, RoleNames.SomeUserRole),
            new(ClaimNames.ActiveRole, "claim1"),
        };
        roles.AddRange(Operations.GetAllRoles().Select(r => new Claim(ClaimTypes.Role, r.Name)));
        _claimsPrincipal = new ClaimsPrincipal(
            new List<ClaimsIdentity>
            {
                new(roles)
            });

        _httpContext = BuildContext();
    }

    private async Task<bool> DoIt()
    {
        return await _claimsPrincipal.SetActiveRole(_httpContext, "invalid");
    }

    [Fact]
    public async Task ShouldBeFalse()
    {
        Assert.False(await DoIt());
    }
}
public class SetActiveRoleTestBase
{
    public HttpContext BuildContext()
    {
        var mockTempDataDictionaryFactory = new Mock<ITempDataDictionaryFactory>();
        var mockUrlHelperFactory = new Mock<IUrlHelperFactory>();

        AuthenticationService
            .Setup(_ => _.SignInAsync(It.IsAny<HttpContext>(), It.IsAny<string>(), It.IsAny<ClaimsPrincipal>(), It.IsAny<AuthenticationProperties>()))
            .Returns(Task.FromResult((object?)null));

        var serviceProviderMock = new Mock<IServiceProvider>();

        serviceProviderMock
            .Setup(_ => _.GetService(typeof(IAuthenticationService)))
            .Returns(AuthenticationService.Object);

        serviceProviderMock
            .Setup(_ => _.GetService(typeof(IUrlHelperFactory)))
            .Returns(mockUrlHelperFactory.Object);

        serviceProviderMock
            .Setup(_ => _.GetService(typeof(ITempDataDictionaryFactory)))
            .Returns(mockTempDataDictionaryFactory.Object);

        return new DefaultHttpContext
        {
            RequestServices = serviceProviderMock.Object
        };
    }
    public Mock<IAuthenticationService> AuthenticationService { get; } = new();
}