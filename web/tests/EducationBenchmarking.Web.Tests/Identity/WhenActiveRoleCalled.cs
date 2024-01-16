using System.Security.Claims;
using EducationBenchmarking.Web.Identity;
using EducationBenchmarking.Web.Identity.Extensions;
using EducationBenchmarking.Web.Identity.Models;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Identity;

public class WhenActiveRoleCalledWithOneActiveRoleInClaims
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

    private Role DoIt()
    {
        return _claimsPrincipal.ActiveRole();
    }

    [Fact]
    public void ShouldReturnRole()
    {
        var response = DoIt();

        Assert.NotNull(response);
    }
}

public class WhenActiveRoleCalledWithTwoActiveRoleInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimTypes.Role, "claim1"),
                    new(ClaimTypes.Role, "claim2")
                })
        });

    private Role DoIt()
    {
        return _claimsPrincipal.ActiveRole();
    }

    [Fact]
    public void ShouldReturnNull()
    {
        var response = DoIt();

        Assert.Null(response);
    }
}

public class WhenActiveRoleCalledWithNoActiveRoleInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new("allRoles", "claim1")
                })
        });

    private Role DoIt()
    {
        return _claimsPrincipal.ActiveRole();
    }

    [Fact]
    public void ShouldReturnNull()
    {
        var response = DoIt();

        Assert.Null(response);
    }
}