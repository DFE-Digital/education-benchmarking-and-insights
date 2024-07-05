using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using Web.App.Identity;
using Web.App.Identity.Models;
namespace Web.Integration.Tests;

public class TestAuthOptions : AuthenticationSchemeOptions
{
    public int URN { get; set; }
    public int CompanyNumber { get; set; }
    public bool AllowAuth { get; set; } = true;
}

public class Auth : AuthenticationHandler<TestAuthOptions>
{
    public const string Email = "integration@test.com";
    public const string GivenName = "Test";
    public const string FamilyName = "User";
    public const string OrganisationName = "Test Organisation";

    public Auth(IOptionsMonitor<TestAuthOptions> options, ILoggerFactory logger, UrlEncoder encoder) : base(options, logger, encoder)
    {
    }
    public static ClaimsPrincipal GetUser(int urn, int companyNumber, string authType = "Test")
    {
        var claims = new List<Claim>
        {
            new("email", Email),
            new(ClaimNames.Schools, urn.ToString()),
            new(ClaimNames.GivenName, GivenName),
            new(ClaimNames.FamilyName, FamilyName),
            new(ClaimNames.Organisation, JsonConvert.SerializeObject(new Organisation
            {
                Id = Guid.Empty,
                Name = OrganisationName,
                URN = urn
            })),
            new(ClaimNames.Trusts, companyNumber.ToString()),
            new(ClaimTypes.NameIdentifier, Guid.Empty.ToString())
        };

        var identity = new ClaimsIdentity(claims, authType);
        return new ClaimsPrincipal(identity);
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        var principal = GetUser(Options.URN, Options.CompanyNumber);
        var ticket = new AuthenticationTicket(principal, "Test");

        var result = Options.AllowAuth
            ? AuthenticateResult.Success(ticket)
            : AuthenticateResult.Fail("Auth rejected");

        return Task.FromResult(result);
    }
}