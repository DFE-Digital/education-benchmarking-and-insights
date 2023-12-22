using System.Reflection;
using CorrelationId.DependencyInjection;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Services;
using SmartBreadcrumbs.Extensions;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetupLogging("Education Benchmarking and Insights");
builder.Services.AddControllersWithViews().AddNewtonsoftJson(options => { options.SerializerSettings.SetJsonOptions(); });
builder.Services.AddDefaultCorrelationId();
builder.Services.AddApplicationInsightsTelemetry();
builder.Services.AddBreadcrumbs(Assembly.GetExecutingAssembly(),options =>
{
    options.TagName = "nav";
    options.TagClasses = "govuk-breadcrumbs govuk-breadcrumbs--collapse-on-mobile";
    options.OlClasses = "govuk-breadcrumbs__list";
    //options.LiTemplate = "<li><a href=\"{1}\">{0}</a></li>";
    options.LiTemplate = "<li class=\"govuk-breadcrumbs__list-item\"><a class=\"govuk-breadcrumbs__link\" href=\"{1}\">{0}</a></li>";
    options.ActiveLiTemplate = "<li class=\"govuk-breadcrumbs__list-item\"><a class=\"govuk-breadcrumbs__link\" href=\"{1}\">{0}</a></li>";
});

if (!builder.Environment.IsEnvironment("IntegrationTest"))
{
    builder.Services.AddOptions<ApiSettings>(ApiSettings.InsightApi)
        .BindConfiguration($"Apis:{ApiSettings.InsightApi}")
        .ValidateDataAnnotations();

    builder.Services.AddOptions<ApiSettings>(ApiSettings.EstablishmentApi)
        .BindConfiguration($"Apis:{ApiSettings.EstablishmentApi}")
        .ValidateDataAnnotations();

    builder.Services.AddHttpClient<IInsightApi, InsightApi>()
        .ConfigureHttpClientForApi(ApiSettings.InsightApi);

    builder.Services.AddHttpClient<IEstablishmentApi, EstablishmentApi>()
        .ConfigureHttpClientForApi(ApiSettings.EstablishmentApi);
}

builder.Services.AddSingleton<IFinanceService, FinanceService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseStatusCodePagesWithRedirects("/error/{0}");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { } // required for intergration tests

