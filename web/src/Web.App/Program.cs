using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Reflection;
using CorrelationId.DependencyInjection;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.FeatureManagement;
using Serilog;
using SmartBreadcrumbs.Extensions;
using Web.App;
using Web.App.Extensions;
using Web.App.Handlers;
using Web.App.Infrastructure.Apis;
using Web.App.Middleware;
using Web.App.Services;
using Web.App.Validators;

CultureInfo.DefaultThreadCurrentCulture = new CultureInfo("en-GB");

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog((context, configuration) => configuration.ReadFrom.Configuration(context.Configuration));

builder.Services.AddControllersWithViews().AddNewtonsoftJson(options =>
{
    options.SerializerSettings.SetJsonOptions();
});
builder.Services.AddDefaultCorrelationId();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(), options =>
{
    options.TagClasses = "govuk-breadcrumbs govuk-breadcrumbs--collapse-on-mobile";
    options.OlClasses = "govuk-breadcrumbs__list";
    options.LiTemplate = "<li class=\"govuk-breadcrumbs__list-item\"><a class=\"govuk-breadcrumbs__link\" href=\"{1}\">{0}</a></li>";
    options.ActiveLiTemplate = "<li class=\"govuk-breadcrumbs__list-item\"><a class=\"govuk-breadcrumbs__link\" href=\"{1}\">{0}</a></li>";
});

builder.Services.AddHealthChecks();
builder.Services.AddHttpContextAccessor();
builder.Services.AddScoped<IFinanceService, FinanceService>();
builder.Services.AddScoped<IFinancialPlanService, FinancialPlanService>();
builder.Services.AddScoped<IComparatorSetService, ComparatorSetService>();
builder.Services.AddScoped<ISuggestService, SuggestService>();
builder.Services.AddScoped<IFinancialPlanStageValidator, FinancialPlanStageValidator>();
builder.Services.AddFeatureManagement()
    .UseDisabledFeaturesHandler(new RedirectDisabledFeatureHandler());

builder.AddSessionService();

if (!builder.Environment.IsIntegration())
{
    builder.Services.AddDfeSignIn(options =>
    {
        builder.Configuration.GetSection("DFESignInSettings").Bind(options.Settings);
        options.Events.OnRejectPrincipal = response => Log.Logger.Warning("Token refresh failed with message: {ErrorDescription} ", response.ErrorDescription);
        options.Events.OnSpuriousAuthenticationRequest = _ => Log.Logger.Warning("Spurious log in attempt received for DFE sign in");
        options.Events.OnRemoteFailure = ctx => Log.Logger.Warning("Remote failure for DFE-sign in - {Failure}", ctx.Failure?.Message);
        options.Events.OnValidatedPrincipal = _ => Log.Logger.Debug("Valid principal received");
    });

    builder.Services.Configure<ForwardedHeadersOptions>(options =>
    {
        options.ForwardedHeaders = ForwardedHeaders.XForwardedHost | ForwardedHeaders.XForwardedProto;
        options.KnownProxies.Clear();
        options.KnownNetworks.Clear();
        options.AllowedHosts = new List<string>
        {
            "*.azurefd.net"
        };
    });

    builder.Services.AddOptions<ApiSettings>(Constants.InsightApi)
        .BindConfiguration(Constants.SectionInsightApi)
        .ValidateDataAnnotations();

    builder.Services.AddOptions<ApiSettings>(Constants.EstablishmentApi)
        .BindConfiguration(Constants.SectionEstablishmentApi)
        .ValidateDataAnnotations();

    builder.Services.AddOptions<ApiSettings>(Constants.BenchmarkApi)
        .BindConfiguration(Constants.SectionBenchmarkApi)
        .ValidateDataAnnotations();

    builder.Services.AddHttpClient<IInsightApi, InsightApi>()
        .ConfigureHttpClientForApi(Constants.InsightApi);

    builder.Services.AddHttpClient<ICensusApi, CensusApi>()
        .ConfigureHttpClientForApi(Constants.InsightApi);

    builder.Services.AddHttpClient<IEstablishmentApi, EstablishmentApi>()
        .ConfigureHttpClientForApi(Constants.EstablishmentApi);

    builder.Services.AddHttpClient<IBenchmarkApi, BenchmarkApi>()
        .ConfigureHttpClientForApi(Constants.BenchmarkApi);
}

var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    app.UseHsts(); // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
}

app.UseForwardedHeaders();
app.UseMiddleware<CustomResponseHeadersMiddleware>();
app.UseStatusCodePagesWithReExecute("/error/{0}");
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.UseSession();

app.MapHealthChecks("/health");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();


[ExcludeFromCodeCoverage]
public partial class Program; // required for integration tests
