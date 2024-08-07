using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;
using Platform.Orchestrator.Configuration;
var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure);

hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
// ReSharper disable once UnusedType.Global
public partial class Program;