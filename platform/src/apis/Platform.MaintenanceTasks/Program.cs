using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Hosting;

using Platform.MaintenanceTasks.Configuration;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure);

hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program;