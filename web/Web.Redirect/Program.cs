var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();
app.UseRouting();

 #pragma warning disable ASP0018
// ReSharper disable once RouteTemplates.RouteParameterIsNotPassedToMethod
app.MapGet("/{*pageRoute:nonfile}", () => Results.Redirect("https://financial-benchmarking-and-insights-tool.education.gov.uk/"));
 #pragma warning restore ASP0018

app.Run();