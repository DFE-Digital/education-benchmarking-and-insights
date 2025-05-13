using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using System.Runtime.CompilerServices;
using CorrelationId.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.FeatureManagement;
using Serilog;
using SmartBreadcrumbs.Extensions;
using Web.App.Cache;
using Web.App.Extensions;
using Web.App.Handlers;
using Web.App.HealthChecks;
using Web.App.Middleware;
using Web.App.Services;
using Web.App.Telemetry;

[assembly: InternalsVisibleTo("Web.Tests")]

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson(options => options.SerializerSettings.SetJsonOptions())
    .AddMvcOptions(options => options.SetModelBindingOptions());

builder.Services
    .AddDefaultCorrelationId()
    .AddApplicationInsightsTelemetry()
    .AddHttpContextAccessor()
    .AddSingleton<ITelemetryInitializer, AuthenticatedUserTelemetryInitializer>()
    .AddScoped<IFinanceService, FinanceService>()
    .AddScoped<IFinancialPlanService, FinancialPlanService>()
    .AddScoped<ISchoolComparatorSetService, SchoolComparatorSetService>()
    .AddScoped<ITrustComparatorSetService, TrustComparatorSetService>()
    .AddScoped<ISuggestService, SuggestService>()
    .AddScoped<ICustomDataService, CustomDataService>()
    .AddScoped<IUserDataService, UserDataService>()
    .AddScoped<IClaimsIdentifierService, ClaimsIdentifierService>()
    .AddScoped<ILocalAuthorityComparatorSetService, LocalAuthorityComparatorSetService>()
    .AddScoped<ISearchService, SearchService>()
    .AddValidation()
    .AddActionResults();

builder.Services.AddHealthChecks()
    .AddCheck<ApiHealthCheck>("API Health Check");

builder.Services.AddFeatureManagement()
    .UseDisabledFeaturesHandler(new RedirectDisabledFeatureHandler());

builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
{
    options.TagClasses = "govuk-breadcrumbs govuk-breadcrumbs--collapse-on-mobile";
    options.OlClasses = "govuk-breadcrumbs__list";
    options.LiTemplate =
        "<li class=\"govuk-breadcrumbs__list-item\"><a class=\"govuk-breadcrumbs__link\" href=\"{1}\">{0}</a></li>";
    options.ActiveLiTemplate = " ";
});

builder.Services.AddMemoryCache();

builder.Services.Configure<CacheOptions>(builder.Configuration.GetSection("CacheOptions"));

builder.AddSessionService();

if (!builder.Environment.IsIntegration())
{
    builder.Services.AddDfeSignIn(options =>
    {
        builder.Configuration.GetSection("DFESignInSettings").Bind(options.Settings);
        options.Events.OnRejectPrincipal = response =>
            Log.Logger.Warning("Token refresh failed: {ErrorDescription} ", response.ErrorDescription);
        options.Events.OnSpuriousAuthenticationRequest =
            _ => Log.Logger.Warning("Spurious log in attempt received for DFE sign in");
        options.Events.OnRemoteFailure = ctx =>
            Log.Logger.Warning("Remote failure for DFE-sign in: {Failure}", ctx.Failure?.Message);
        options.Events.OnValidatedPrincipal = ctx =>
            Log.Logger.Debug(
                "Valid principal received: {Identity} ({Organisation})",
                ctx.Principal?.UserGuid(),
                ctx.Principal?.Organisation().Id);
        options.Events.OnNotValidatedPrincipal = (ctx, ex) =>
            Log.Logger.Warning(
                "Token validated, but additional validation failed for {Identity} ({Organisation}): {ErrorMessage}",
                ctx.Principal?.UserGuid(),
                ctx.Principal?.Organisation().Id,
                ex.Message);
    });

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto;
        options.KnownProxies.Clear();
        options.KnownNetworks.Clear();
        options.AllowedHosts = new List<string>
        {
            "*.azurefd.net",
            "financial-benchmarking-and-insights-tool.education.gov.uk",
            "schools-financial-benchmarking.service.gov.uk"
        };
    });

    builder.Services
        .AddBenchmarkApi()
        .AddEstablishmentApi()
        .AddInsightApi()
        .AddLocalAuthorityFinancesApi()
        .AddNonFinancialApi()
        .AddStorage();

    builder.AddSwaggerService();
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app
        .UseSwagger()
        .UseSwaggerUI();
}
else
{
    app
        .UseExceptionHandler("/error")
        .UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app
    .UseStaticFiles(new StaticFileOptions
    {
        OnPrepareResponse = ctx =>
        {
            ctx.Context.Response.Headers.Append("Cache-Control", "public,max-age=600");
            ctx.Context.Response.Headers.Append("Expires", DateTime.UtcNow.AddMinutes(60).ToString("R"));
        }
    })
    .UseForwardedHeaders()
    .UseMiddleware<CustomResponseHeadersMiddleware>()
    .UseStatusCodePagesWithReExecute("/error/{0}")
    .UseHttpsRedirection()
    .UseRouting()
    .UseAuthorization()
    .UseSession();

app.MapHealthChecks(
    "/health",
    new HealthCheckOptions
    {
        ResponseWriter = async (context, report) =>
        {
            context.Response.ContentType = "application/json";
            var result = new
            {
                status = report.Status.ToString(),
                details = report.Entries.Select(e => new { key = e.Key, value = e.Value.Status.ToString() })
            }.ToJson();
            await context.Response.WriteAsync(result);
        }
    });

app.MapControllerRoute(
    "default",
    "{controller=Home}/{action=Index}/{id?}");

app.Run();


[ExcludeFromCodeCoverage]
public partial class Program; // required for integration tests