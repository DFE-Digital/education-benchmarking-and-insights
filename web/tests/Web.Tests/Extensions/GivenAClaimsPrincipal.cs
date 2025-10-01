using System.Security.Claims;
using Microsoft.Extensions.Configuration;
using Moq;
using Web.App;
using Web.App.Extensions;
using Web.App.Identity;
using Xunit;

namespace Web.Tests.Extensions;

public class GivenAClaimsPrincipal
{
    private readonly Mock<IConfiguration> _configuration = new();

    [Theory]
    [InlineData("12345678", new string[] { }, true, true)]
    [InlineData("12345678", new[] { "12345678" }, true, true)]
    [InlineData("123456", new[] { "12345678" }, true, true)]
    [InlineData("123456", new[] { "12345678" }, false, true)]
    [InlineData("12345678", new[] { "12345678" }, false, true)]
    [InlineData("12345678", new[] { "87654321" }, false, false)]
    [InlineData("12345687", new string[] { }, false, false)]
    [InlineData("12345678", new string[] { }, null, false)]
    [InlineData(null, new string[] { }, false, false)]
    public void ReturnsExpectedTrustAuthorisation(string? companyNumber, string[] trustClaims, bool? disableOrganisationClaimCheck, bool authorised)
    {
        var section = Mock.Of<IConfigurationSection>();
        section.Value = disableOrganisationClaimCheck?.ToString();
        _configuration
            .Setup(c => c.GetSection(EnvironmentVariables.DisableOrganisationClaimCheck))
            .Returns(section);
        var user = new ClaimsPrincipal(new ClaimsIdentity(trustClaims.Select(c => new Claim(ClaimNames.Trusts, c)), "mock"));

        var result = user.HasTrustAuthorisation(companyNumber, _configuration.Object);

        Assert.Equal(authorised, result);
    }
}