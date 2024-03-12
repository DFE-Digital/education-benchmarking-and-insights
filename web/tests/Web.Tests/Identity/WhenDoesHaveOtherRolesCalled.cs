using System.Security.Claims;
using Web.Identity;
using Web.Identity.Extensions;
using Xunit;

namespace Web.Tests.Identity;


public class WhenDoesHaveOtherRolesCalledWithNoRoles
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(new List<Claim>())
        });

    private bool DoIt()
    {
        return _claimsPrincipal.DoesHaveOtherRoles();
    }

    [Fact]
    public void ShouldReturnFalse()
    {
        var response = DoIt();

        Assert.False(response);
    }
}


public class WhenDoesHaveOtherRolesCalledWithOneRole
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimNames.RoleCount, "1")
                })
        });

    private bool DoIt()
    {
        return _claimsPrincipal.DoesHaveOtherRoles();
    }

    [Fact]
    public void ShouldReturnFalse()
    {
        var response = DoIt();

        Assert.False(response);
    }
}


public class WhenDoesHaveOtherRolesCalledWithMoreThanOneRole
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimNames.RoleCount, "2")
                })
        });

    private bool DoIt()
    {
        return _claimsPrincipal.DoesHaveOtherRoles();
    }

    [Fact]
    public void ShouldReturnTrue()
    {
        var response = DoIt();

        Assert.True(response);
    }
}