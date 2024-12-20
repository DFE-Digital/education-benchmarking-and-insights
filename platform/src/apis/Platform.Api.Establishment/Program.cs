using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Platform.Api.Establishment.Configuration;

var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure)
    .ConfigureOpenApi();

hostBuilder.Build().Run();

namespace Platform.Api.Establishment
{
    [ExcludeFromCodeCoverage]
    // ReSharper disable once UnusedType.Global
    public partial class Program;
}