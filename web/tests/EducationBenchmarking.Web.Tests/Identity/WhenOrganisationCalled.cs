using System.Security.Claims;
using EducationBenchmarking.Web.Identity.Extensions;
using EducationBenchmarking.Web.Identity.Models;
using Newtonsoft.Json;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Identity;

public class WhenOrganisationCalledWithOrganisationInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new("organisation", JsonConvert.SerializeObject(new Organisation { URN = 999999 }))
                })
        });

    private Organisation DoIt()
    {
        return _claimsPrincipal.Organisation();
    }

    [Fact]
    public void ShouldReturnOrganisation()
    {
        var response = DoIt();
            
        Assert.NotNull(response);
    }
} 


public class WhenOrganisationCalledWithoutOrganisationInClaims
{
    private readonly ClaimsPrincipal _claimsPrincipal = new(
        new List<ClaimsIdentity>
        {
            new(
                new List<Claim>
                {
                    new(ClaimTypes.Role, "arole")
                })
        });


    private Organisation DoIt()
    {
        return _claimsPrincipal.Organisation();
    }

    [Fact]
    public void ShouldHandleNull()
    {
        Assert.Throws<ArgumentNullException>(() => DoIt());
    }
}