using System.Security.Claims;
using Web.Identity;
using Web.Identity.Extensions;
using Xunit;

namespace Web.Tests.Identity;

public class WhenDoesHaveOneActiveRoleCalledWithOneActiveRoleInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimNames.ActiveRole, "claim1"),
                })
        });

    private bool DoIt()
    {
        return _claimsPrincipal.DoesHaveOneActiveRole();
    }

    [Fact]
    public void ShouldReturnTrue()
    {
        var response = DoIt();

        Assert.True(response);
    }
}

public class WhenDoesHaveOneActiveRoleCalledWithNoActiveRoleInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimTypes.Role, "claim1")
                })
        });

    private bool DoIt()
    {
        return _claimsPrincipal.DoesHaveOneActiveRole();
    }

    [Fact]
    public void ShouldReturnFalse()
    {
        var response = DoIt();

        Assert.False(response);
    }
}