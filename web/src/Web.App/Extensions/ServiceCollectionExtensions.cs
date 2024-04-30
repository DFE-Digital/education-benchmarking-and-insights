using System.Security.Claims;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Web.App.Identity;
using Web.App.Identity.Models;

namespace Web.App.Extensions;

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
                options.Cookie.Name = "dsi-education-benchmarking";
                options.ExpireTimeSpan = TimeSpan.FromHours(1);
                options.SlidingExpiration = true;
                options.Cookie.SameSite = SameSiteMode.Lax;

                options.Events = new CookieAuthenticationEvents
                {
                    OnRedirectToAccessDenied = async context =>
                    {
                        context.Response.StatusCode = 403;
                    },
                    OnValidatePrincipal = async x =>
                    {
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
                    NameClaimType = "name",
                };

                options.ProtocolValidator = new OpenIdConnectProtocolValidator
                {
                    RequireSub = true,
                    RequireStateValidation = false,
                    NonceLifetime = TimeSpan.FromMinutes(60),
                };

                options.Events = new OpenIdConnectEvents
                {
                    OnMessageReceived = async context =>
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
                    },

                    OnRemoteFailure = async ctx =>
                    {
                        opts.Events.OnRemoteFailure(ctx);

                        ctx.Response.Redirect("/");
                        ctx.HandleResponse();
                    },

                    OnRedirectToIdentityProvider = _ => Task.CompletedTask,

                    OnTokenValidated = async x =>
                    {
                        try
                        {
                            var schools = Array.Empty<string>();
                            var organisation = x.Principal?.Organisation();
                            if (organisation?.UrnValue != null)
                            {
                                schools = [organisation.UrnValue.ToString()];
                            }

                            //TODO: Handle trust - lookup schools for trust information;
                            //var api = x.HttpContext.RequestServices.GetRequiredService<IEstablishmentApi>();

                            x.Principal?.ApplyClaims(x.TokenEndpointResponse!.AccessToken, schools);
                            opts.Events.OnValidatedPrincipal(x);

                        }
                        catch (Exception ex)
                        {
                            x.Fail(ex);
                        }
                    }
                };
            });
    }
}