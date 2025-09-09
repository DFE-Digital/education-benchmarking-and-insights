using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Platform.Api.Establishment.Configuration;

// ReSharper disable ClassNeverInstantiated.Global

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure)
    .ConfigureOpenApi();

hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
public partial class Program;