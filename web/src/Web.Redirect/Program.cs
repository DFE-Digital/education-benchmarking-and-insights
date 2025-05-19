// ReSharper disable ConvertToLambdaExpression
// ReSharper disable RouteTemplates.RouteParameterIsNotPassedToMethod
var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseRouting();

#pragma warning disable ASP0018
app.MapGet("/{*pageRoute:nonfile}", () =>
{
    // web.config rewrite rules should intercept request before this point,
    // but included as a 'catch all' route here as a fail-safe.
    return Results.Redirect("https://financial-benchmarking-and-insights-tool.education.gov.uk/");
});
#pragma warning restore ASP0018

app.Run();