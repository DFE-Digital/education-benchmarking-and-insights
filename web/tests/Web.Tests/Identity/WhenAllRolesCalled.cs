using System.Security.Claims;
using Web.Identity;
using Web.Identity.Extensions;
using Web.Identity.Models;
using Xunit;

namespace Web.Tests.Identity;

public class WhenAllRolesCalledWithAllRolesClaimsInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimTypes.Role, RoleNames.SomeUserRole)
                })
        });

    private IEnumerable<Role> DoIt()
    {
        return _claimsPrincipal.Roles();
    }

    [Fact]
    public void ShouldReturnTheAllClaims()
    {
        var response = DoIt();

        Assert.NotNull(response);
        Assert.Single(response.ToList());
    }
}

public class WhenOrganisationCalledWithoutAllRolesClaimsInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimNames.ActiveRole, "arole")
                })
        });

    private IEnumerable<Role> DoIt()
    {
        return _claimsPrincipal.Roles();
    }

    [Fact]
    public void ShouldReturnNoItems()
    {
        var response = DoIt();

        Assert.NotNull(response);
        Assert.Empty(response.ToList());
    }
}