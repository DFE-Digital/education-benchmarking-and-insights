using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;

using Platform.MaintenanceTasks.Configuration;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure)
    .ConfigureAppConfiguration((context, builder) =>
    {
        var env = context.HostingEnvironment.EnvironmentName.ToLower();
        builder.AddUserSecrets($"platform-{env}");
    });


hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program;