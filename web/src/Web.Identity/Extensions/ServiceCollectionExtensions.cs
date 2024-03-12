using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using Web.Identity.Models;
using IdentityModel.Client;
using JWT.Algorithms;
using JWT.Builder;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace Web.Identity.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDfeSignIn(this IServiceCollection services,
        Action<DfeSignInOptions> optionCfg = null)
    {
        var opts = new DfeSignInOptions();
        optionCfg?.Invoke(opts);

        services
            .AddAntiForgery(opts.IsDevelopment)
            .AddAuthentication(options =>
            {
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
            })
            .AddDfeSignInCookie(opts)
            .AddDfeSignInOpenIdConnect(opts);

        return services;
    }

    private static IServiceCollection AddAntiForgery(this IServiceCollection services, bool isDevelopment)
    {
        return services
            .AddAntiforgery(options =>
            {
                options.Cookie.SecurePolicy = isDevelopment
                    ? CookieSecurePolicy.SameAsRequest
                    : CookieSecurePolicy.Always;
            });
    }

    private static AuthenticationBuilder AddDfeSignInCookie(this AuthenticationBuilder builder,
        DfeSignInOptions opts)
    {
        return builder
            .AddCookie(options =>
            {
                options.Cookie.Name = Constants.AuthCookieName;
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
                    },
                    OnValidatePrincipal = async x =>
                    {
                        x.Principal.GetOrSelectRole(x.HttpContext);

                        if (TimeIssuedIsValid(x.Properties.IssuedUtc))
                        {
                            var identity = x.Principal!.Identity as ClaimsIdentity ??
                                           throw new Exception("Missing identity claim");

                            var accessTokenClaim = identity.FindFirst(ClaimNames.AccessToken);
                            var refreshTokenClaim = identity.FindFirst(ClaimNames.RefreshToken);
                            var clientId = opts.Settings.ClientID;
                            var clientSecret = opts.Settings.ClientSecret;
                            var tokenEndpoint = opts.Settings.TokenEndPoint;

                            var client = new HttpClient();

                            var response = await client.RequestRefreshTokenAsync(new RefreshTokenRequest
                            {
                                Address = tokenEndpoint,
                                ClientId = clientId,
                                ClientSecret = clientSecret,
                                RefreshToken = refreshTokenClaim != null ? refreshTokenClaim.Value : ""
                            });

                            if (!response.IsError)
                            {
                                identity.RemoveClaim(accessTokenClaim);
                                identity.RemoveClaim(refreshTokenClaim);
                                identity.AddClaims(new[]
                                {
                                    new Claim(ClaimNames.AccessToken, response.AccessToken),
                                    new Claim(ClaimNames.RefreshToken, response.RefreshToken)
                                });

                                x.ShouldRenew = true;
                            }
                            else
                            {
                                opts.Events.OnRejectPrincipal(response);

                                x.RejectPrincipal();
                            }
                        }
                    }
                };
            });
    }

    private static bool TimeIssuedIsValid(DateTimeOffset? issuedUtc)
    {
        if (!issuedUtc.HasValue)
        {
            return false;
        }

        var timeElapsed = DateTimeOffset.UtcNow.Subtract(issuedUtc.Value);
        return timeElapsed > TimeSpan.FromMinutes(59.5);
    }

    private static AuthenticationBuilder AddDfeSignInOpenIdConnect(this AuthenticationBuilder builder,
        DfeSignInOptions opts)
    {
        return builder
            .AddOpenIdConnect(options =>
            {
                options.SignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.MetadataAddress = opts.Settings.MetadataAddress;
                options.RequireHttpsMetadata = false;
                options.ClientId = opts.Settings.ClientID;
                options.NonceCookie.SameSite = SameSiteMode.None;
                options.CorrelationCookie.SameSite = SameSiteMode.None;
                options.ClientSecret = opts.Settings.ClientSecret;
                options.ResponseType = OpenIdConnectResponseType.Code;
                options.GetClaimsFromUserInfoEndpoint = true;
                options.SaveTokens = true;
                options.CallbackPath = new PathString(opts.Settings.CallbackPath);
                options.SignedOutCallbackPath = new PathString(opts.Settings.SignedOutCallbackPath);
                options.DisableTelemetry = true;

                options.Scope.Clear();
                options.Scope.Add("openid");
                options.Scope.Add("email");
                options.Scope.Add("profile");
                options.Scope.Add("organisation");
                options.Scope.Add("offline_access");

                //required to set user.identity.name
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    NameClaimType = "name"
                };

                options.SecurityTokenValidator = new JwtSecurityTokenHandler
                {
                    InboundClaimTypeMap = new Dictionary<string, string>(),
                    TokenLifetimeInMinutes = 60,
                    SetDefaultTimesOnTokenCreation = true,
                };

                options.ProtocolValidator = new OpenIdConnectProtocolValidator
                {
                    RequireSub = true,
                    RequireStateValidation = false,
                    NonceLifetime = TimeSpan.FromMinutes(60)
                };

                options.Events = new OpenIdConnectEvents
                {
                    OnMessageReceived = context =>
                    {
                        var isSpuriousAuthCbRequest =
                            context.Request.Path == options.CallbackPath &&
                            context.Request.Method == "GET" &&
                            !context.Request.Query.ContainsKey("code");

                        if (isSpuriousAuthCbRequest)
                        {
                            opts.Events.OnSpuriousAuthenticationRequest(context);

                            context.Response.Redirect("/");
                            context.HandleResponse();
                        }

                        return Task.CompletedTask;
                    },

                    OnRemoteFailure = ctx =>
                    {
                        opts.Events.OnRemoteFailure(ctx);

                        ctx.Response.Redirect("/");
                        ctx.HandleResponse();
                        return Task.CompletedTask;
                    },

                    OnRedirectToIdentityProvider = _ => Task.CompletedTask,

                    OnTokenValidated = async x =>
                    {
                        try
                        {
                            var issuer = opts.Settings.Issuer;
                            var audience = opts.Settings.Audience;
                            var apiSecret = opts.Settings.APISecret;
                            var apiUri = opts.Settings.APIUri;

                            ArgumentNullException.ThrowIfNull(issuer);
                            ArgumentNullException.ThrowIfNull(audience);
                            ArgumentNullException.ThrowIfNull(apiSecret);
                            ArgumentNullException.ThrowIfNull(apiUri);

                            var userRoles = await GetUserRoles(issuer, audience, apiSecret, apiUri, x.Principal);
                            x.Principal.ApplyClaims(x.TokenEndpointResponse!.AccessToken, userRoles);

                            opts.Events.OnValidatedPrincipal(x);
                        }
                        catch (Exception)
                        {
                            x.Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                            x.Response.Redirect("/");
                        }
                    }
                };
            });
    }

    private static async Task<Role[]> GetUserRoles(string issuer, string audience, string apiSecret, string apiUri,
        ClaimsPrincipal principal)
    {
        var token = new JwtBuilder()
            .WithAlgorithm(new HMACSHA256Algorithm())
            .Issuer(issuer)
            .Audience(audience)
            .WithSecret(apiSecret)
            .Encode();

        var organisation = principal.Organisation();

        var client = new HttpClient();

        client.SetBearerToken(token);

        var serviceid = issuer;

        var userId = principal.UserGuid();

        var requestUrl = $"{apiUri}/services/{serviceid}/organisations/{organisation.Id}/users/{userId}";
        var response = await client.GetAsync(requestUrl);

        if (!response.IsSuccessStatusCode)
        {
            throw new SystemException("Could not get Role Type for User");
        }

        var json = await response.Content.ReadAsStringAsync();

        var result = JsonConvert.DeserializeObject<DfeIdentity>(json);

        if (result == null || result.UserId != userId)
        {
            throw new SystemException("UserId mismatch after retrieving User");
        }

        return result.Roles.ToArray();
    }
}