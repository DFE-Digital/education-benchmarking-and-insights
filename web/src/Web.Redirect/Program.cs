// ReSharper disable RouteTemplates.RouteParameterIsNotPassedToMethod
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();
app.UseRouting();

#pragma warning disable ASP0018
app.MapGet("/{*pageRoute:nonfile}", () =>
{
    // web.config rewrite rules should intercept request before this catch-all endpoint
    throw new ApplicationException("Unable to redirect to new service");
});
#pragma warning restore ASP0018

app.Run();