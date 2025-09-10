using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using Web.App.Identity;
using Web.App.Telemetry;
using Xunit;
using TelemetryClientExtensions = Web.App.Extensions.TelemetryClientExtensions;

namespace Web.Tests.Extensions;

public class GivenATelemetryClient
{
    private readonly Mock<ITelemetryClientWrapper> _telemetry = new();

    [Fact]
    public void TracksEventWhenTrackUserSignedInEventIsCalled()
    {
        // arrange
        var identity = new ClaimsIdentity();
        identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, "user-id"));
        identity.AddClaim(new Claim(ClaimTypes.Email, nameof(ClaimTypes.Email)));
        identity.AddClaim(new Claim(ClaimTypes.GivenName, nameof(ClaimTypes.GivenName)));
        identity.AddClaim(new Claim(ClaimTypes.Surname, nameof(ClaimTypes.Surname)));
        identity.AddClaim(new Claim(ClaimNames.Organisation, "{}"));
        identity.AddClaim(new Claim("sid", "sensitive"));
        identity.AddClaim(new Claim("nonce", "sensitive"));
        identity.AddClaim(new Claim("at_hash", "sensitive"));
        identity.AddClaim(new Claim("aud", "FBIT"));
        identity.AddClaim(new Claim("exp", "0000000000"));
        identity.AddClaim(new Claim("iat", "0000000000"));
        identity.AddClaim(new Claim("iss", "https://pp-oidc.signin.education.gov.uk:443"));

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
        TelemetryClientExtensions.TrackUserSignedInEvent(_telemetry.Object, context);

        // assert
        _telemetry.VerifyAll();
        Assert.Equal("user-sign-in-success", actualEventName);
        Assert.Equal(2, actualProperties.Keys.Count);
        Assert.Equal("user-id", actualProperties["User"]);
        Assert.Equal("""
                       http://schemas.xmlsoap.org/ws/2005/05/identity/claims/nameidentifier: user-id
                       http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress: *****
                       http://schemas.xmlsoap.org/ws/2005/05/identity/claims/givenname: *********
                       http://schemas.xmlsoap.org/ws/2005/05/identity/claims/surname: *******
                       organisation: {}
                       sid: *********
                       nonce: *********
                       at_hash: *********
                       aud: FBIT
                       exp: 0000000000
                       iat: 0000000000
                       iss: https://pp-oidc.signin.education.gov.uk:443
                       """, actualProperties["Claims"]);
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