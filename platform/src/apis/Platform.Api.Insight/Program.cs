using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Platform.Api.Insight.Configuration;
[assembly: InternalsVisibleTo("Platform.Tests")]

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure)
    .ConfigureOpenApi();

hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public partial class Program;