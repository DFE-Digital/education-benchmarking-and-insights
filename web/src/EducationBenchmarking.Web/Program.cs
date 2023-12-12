using CorrelationId.DependencyInjection;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Apis;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.SetupLogging("Education Benchmarking and Insights");
builder.Services.AddControllersWithViews();
builder.Services.AddDefaultCorrelationId();
builder.Services.AddApplicationInsightsTelemetry();

builder.Services.AddOptions<ApiSettings>(ApiSettings.SchoolApi)
    .BindConfiguration($"Apis:{ApiSettings.SchoolApi}")
    .ValidateDataAnnotations();

builder.Services.AddHttpClient<ISchoolApi, SchoolApi>()
    .ConfigureHttpClientForApi(ApiSettings.SchoolApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public partial class Program { } // required for intergration tests

