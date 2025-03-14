using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using Microsoft.Azure.Functions.Worker.Extensions.OpenApi.Extensions;
using Microsoft.Extensions.Hosting;
using Platform.Api.LocalAuthorityFinances.Configuration;

[assembly: InternalsVisibleTo("Platform.LocalAuthorityFinances.Tests")]
var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure)
    .ConfigureOpenApi();

hostBuilder.Build().Run();

[ExcludeFromCodeCoverage]
// ReSharper disable once ClassNeverInstantiated.Global
public partial class Program;