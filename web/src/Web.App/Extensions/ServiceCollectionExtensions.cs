using System.Security.Claims;
using FluentValidation;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Web.App.ActionResults;
using Web.App.Identity;
using Web.App.Identity.Models;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Infrastructure.Apis.Content;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Apis.NonFinancial;
using Web.App.Infrastructure.Storage;
using Web.App.Services;
using Web.App.Validators;

namespace Web.App.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddDfeSignIn(this IServiceCollection services,
        Action<DfeSignInOptions>? optionCfg = null)
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

    public static IServiceCollection AddBenchmarkApi(this IServiceCollection services)
    {
        const string section = "Apis:Benchmark";

        services.AddHttpClient<IFinancialPlanApi, FinancialPlanApi>().Configure<FinancialPlanApi>(section);
        services.AddHttpClient<ICustomDataApi, CustomDataApi>().Configure<CustomDataApi>(section);
        services.AddHttpClient<IComparatorSetApi, ComparatorSetApi>().Configure<ComparatorSetApi>(section);
        services.AddHttpClient<IUserDataApi, UserDataApi>().Configure<UserDataApi>(section);
        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);

        return services;
    }

    public static IServiceCollection AddEstablishmentApi(this IServiceCollection services)
    {
        const string section = "Apis:Establishment";

        services.AddHttpClient<IEstablishmentApi, EstablishmentApi>().Configure<EstablishmentApi>(section);
        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);
        services.AddHttpClient<IComparatorApi, ComparatorApi>().Configure<ComparatorApi>(section);

        return services;
    }

    public static IServiceCollection AddInsightApi(this IServiceCollection services)
    {
        const string section = "Apis:Insight";

        services.AddHttpClient<ICensusApi, CensusApi>().Configure<CensusApi>(section);
        services.AddHttpClient<IIncomeApi, IncomeApi>().Configure<IncomeApi>(section);
        services.AddHttpClient<IBalanceApi, BalanceApi>().Configure<BalanceApi>(section);
        services.AddHttpClient<IExpenditureApi, ExpenditureApi>().Configure<ExpenditureApi>(section);
        services.AddHttpClient<IMetricRagRatingApi, MetricRagRatingApi>().Configure<MetricRagRatingApi>(section);
        services.AddHttpClient<ISchoolInsightApi, SchoolInsightApi>().Configure<SchoolInsightApi>(section);
        services.AddHttpClient<ITrustInsightApi, TrustInsightApi>().Configure<TrustInsightApi>(section);
        services.AddHttpClient<IBudgetForecastApi, BudgetForecastApi>().Configure<BudgetForecastApi>(section);
        services.AddHttpClient<IItSpendApi, ItSpendApi>().Configure<ItSpendApi>(section);
        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);

        return services;
    }

    public static IServiceCollection AddLocalAuthorityFinancesApi(this IServiceCollection services)
    {
        const string section = "Apis:LocalAuthorityFinances";

        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);
        services.AddHttpClient<ILocalAuthoritiesApi, LocalAuthoritiesApi>().Configure<LocalAuthoritiesApi>(section);

        return services;
    }

    public static IServiceCollection AddNonFinancialApi(this IServiceCollection services)
    {
        const string section = "Apis:NonFinancial";

        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);
        services.AddHttpClient<IEducationHealthCarePlansApi, EducationHealthCarePlansApi>().Configure<EducationHealthCarePlansApi>(section);

        return services;
    }

    public static IServiceCollection AddChartRenderingApi(this IServiceCollection services)
    {
        const string section = "Apis:ChartRendering";

        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);
        services.AddHttpClient<IChartRenderingApi, ChartRenderingApi>().Configure<ChartRenderingApi>(section);

        return services;
    }

    public static IServiceCollection AddContentApi(this IServiceCollection services)
    {
        const string section = "Apis:Content";

        services.AddHttpClient<IHealthApi, HealthApi>(section).Configure<HealthApi>(section);
        services.AddHttpClient<IBannerApi, BannerApi>().Configure<BannerApi>(section);
        services.AddHttpClient<ICommercialResourcesApi, CommercialResourcesApi>().Configure<CommercialResourcesApi>(section);
        services.AddHttpClient<IFilesApi, FilesApi>().Configure<FilesApi>(section);
        services.AddHttpClient<INewsApi, NewsApi>().Configure<BannerApi>(section);
        services.AddHttpClient<IYearsApi, YearsApi>().Configure<YearsApi>(section);

        return services;
    }

    public static IServiceCollection AddStorage(this IServiceCollection services)
    {
        services.AddOptions<DataSourceStorageOptions>()
            .BindConfiguration("Storage")
            .ValidateDataAnnotations();

        services.AddSingleton<IDataSourceStorage, DataSourceStorage>();

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
                    OnRedirectToAccessDenied = context =>
                    {
                        context.Response.StatusCode = 403;
                        return Task.CompletedTask;
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
                                ClientId = clientId ?? throw new ArgumentNullException(nameof(clientId)),
                                ClientSecret = clientSecret,
                                RefreshToken = refreshTokenClaim != null ? refreshTokenClaim.Value : ""
                            });

                            if (!response.IsError)
                            {
                                identity.RemoveClaim(accessTokenClaim);
                                identity.RemoveClaim(refreshTokenClaim);
                                identity.AddClaims(new[]
                                {
                                    new Claim(ClaimNames.AccessToken, response.AccessToken ?? throw new ArgumentNullException(nameof(response.AccessToken))),
                                    new Claim(ClaimNames.RefreshToken, response.RefreshToken ?? throw new ArgumentNullException(nameof(response.RefreshToken)))
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
                    OnTokenValidated = async context =>
                    {
                        try
                        {
                            var organisation = context.Principal?.Organisation();
                            var service = context.HttpContext.RequestServices.GetRequiredService<IClaimsIdentifierService>();
                            var (schools, trusts) = await service.IdentifyValidClaims(organisation);

                            context.Principal?.ApplyClaims(context.TokenEndpointResponse?.AccessToken, schools, trusts);
                            opts.Events.OnValidatedPrincipal(context);
                        }
                        catch (Exception ex)
                        {
                            opts.Events.OnNotValidatedPrincipal(context, ex);
                            context.Fail(ex);
                        }
                    }
                };
            });
    }

    public static IServiceCollection AddActionResults(this IServiceCollection services)
    {
        return services
            .AddSingleton<IActionResultExecutor<CsvResult>, CsvResultActionResultExecutor>()
            .AddSingleton<IActionResultExecutor<CsvResults>, CsvResultsActionResultExecutor>()
            .AddSingleton<ICsvService, CsvService>();
    }

    public static IServiceCollection AddValidation(this IServiceCollection services)
    {
        return services
            .AddScoped<IFinancialPlanStageValidator, FinancialPlanStageValidator>()
            .AddScoped<IValidator<OrganisationIdentifier>, OrganisationIdentifierValidator>();
    }
}