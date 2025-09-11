using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Web.App;
using Web.App.Identity.Models;
using Web.App.Telemetry;
using Xunit;
using TelemetryClientExtensions = Web.App.Extensions.TelemetryClientExtensions;

namespace Web.Tests.Extensions;

public class GivenATelemetryClient
{
    private readonly Mock<ITelemetryClientWrapper> _telemetry = new();

    [Fact]
    public void TracksEventWhenTrackUserSignInInitiatedEventIsCalled()
    {
        // arrange
        const string path = $"/{nameof(path)}";
        const string redirectUri = $"/{nameof(redirectUri)}";
        const string controller = nameof(controller);
        const string action = nameof(action);
        var httpContext = new DefaultHttpContext
        {
            Request =
            {
                Path = path,
                QueryString = new QueryString($"?redirectUri={redirectUri}"),
                RouteValues = new RouteValueDictionary
                {
                    { "controller", controller },
                    { "action", action }
                }
            }
        };

        var context = new RedirectContext(
            httpContext,
            new AuthenticationScheme(string.Empty, null, typeof(FakeAuthHandler)),
            Mock.Of<OpenIdConnectOptions>(),
            Mock.Of<AuthenticationProperties>());

        var actualEventName = string.Empty;
        IDictionary<string, string> actualProperties = new Dictionary<string, string>();
        _telemetry
            .Setup(t => t.TrackEvent(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
            .Callback<string, IDictionary<string, string>>((eventName, properties) =>
            {
                actualEventName = eventName;
                actualProperties = properties;
            })
            .Verifiable();

        // act
        TelemetryClientExtensions.TrackUserSignInInitiatedEvent(_telemetry.Object, context);

        // assert
        _telemetry.VerifyAll();
        Assert.Equal("user-sign-in-initiated", actualEventName);
        Assert.Equal(4, actualProperties.Keys.Count);
        Assert.Equal(path, actualProperties["Path"]);
        Assert.Equal(redirectUri, actualProperties["RedirectUri"]);
        Assert.Equal(controller, actualProperties["Route.Controller"]);
        Assert.Equal(action, actualProperties["Route.Action"]);
    }

    [Theory]
    [InlineData(OrganisationCategories.SingleAcademyTrust, 1234567, "trust", "CompanyNumber", "01234567")]
    [InlineData(OrganisationCategories.SingleAcademyTrust, null, "trust", "CompanyNumber", "")]
    [InlineData(OrganisationCategories.MultiAcademyTrust, 1234567, "trust", "CompanyNumber", "01234567")]
    [InlineData(OrganisationCategories.LocalAuthority, 12, "local-authority", "Code", "012")]
    [InlineData(OrganisationCategories.LocalAuthority, null, "local-authority", "Code", "")]
    [InlineData("001", 12345, "school", "Urn", "012345")]
    [InlineData("001", null, "school", "Urn", "")]
    [InlineData(null, null, "", "", "")]
    public void TracksEventWhenTrackUserSignedInEventIsCalled(
        string? organisationCategory,
        int? organisationIdentifier,
        string expectedOrganisationType,
        string expectedOrganisationIdentifierKey,
        string expectedOrganisationIdentifierValue)
    {
        // arrange
        var organisation = new Organisation
        {
            Category = new OrganisationItem
            {
                Id = organisationCategory
            }
        };

        switch (organisationCategory)
        {
            case OrganisationCategories.SingleAcademyTrust:
            case OrganisationCategories.MultiAcademyTrust:
                organisation.CompanyRegistrationNumber = organisationIdentifier;
                break;
            case OrganisationCategories.LocalAuthority:
                organisation.EstablishmentNumber = organisationIdentifier;
                break;
            case "001":
                organisation.URN = organisationIdentifier;
                break;
        }

        const string userId = nameof(userId);
        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, userId));
        var principal = new ClaimsPrincipal(identity);

        var context = new TokenValidatedContext(
            new DefaultHttpContext(),
            new AuthenticationScheme(string.Empty, null, typeof(FakeAuthHandler)),
            Mock.Of<OpenIdConnectOptions>(),
            principal,
            Mock.Of<AuthenticationProperties>());

        var actualEventName = string.Empty;
        IDictionary<string, string> actualProperties = new Dictionary<string, string>();
        _telemetry
            .Setup(t => t.TrackEvent(It.IsAny<string>(), It.IsAny<IDictionary<string, string>>()))
            .Callback<string, IDictionary<string, string>>((eventName, properties) =>
            {
                actualEventName = eventName;
                actualProperties = properties;
            })
            .Verifiable();

        // act
        TelemetryClientExtensions.TrackUserSignedInEvent(_telemetry.Object, context, organisation);

        // assert
        _telemetry.VerifyAll();
        Assert.Equal("user-sign-in-success", actualEventName);
        Assert.Equal(organisationCategory == null ? 2 : 3, actualProperties.Keys.Count);
        Assert.Equal(userId, actualProperties["User"]);
        Assert.Equal(expectedOrganisationType, actualProperties["Organisation Type"]);

        if (organisationCategory != null)
        {
            Assert.True(actualProperties.ContainsKey(expectedOrganisationIdentifierKey));
            Assert.Equal(expectedOrganisationIdentifierValue, actualProperties[expectedOrganisationIdentifierKey]);
        }
    }

    public class FakeAuthHandler(IOptionsMonitor<AuthenticationSchemeOptions> options, ILoggerFactory logger, UrlEncoder encoder) : AuthenticationHandler<AuthenticationSchemeOptions>(options, logger, encoder)
    {
        protected override Task<AuthenticateResult> HandleAuthenticateAsync()
        {
            var identity = new ClaimsIdentity();
            var principal = new ClaimsPrincipal(identity);
            var ticket = new AuthenticationTicket(principal, Scheme.Name);
            return Task.FromResult(AuthenticateResult.Success(ticket));
        }
    }
}