using Microsoft.Extensions.Hosting;
using Platform.UserDataCleanUp.Configuration;
var hostBuilder = new HostBuilder()
    .ConfigureFunctionsWorkerDefaults(Worker.Configure, Worker.Options)
    .ConfigureServices(Services.Configure)
    .ConfigureLogging(Logging.Configure);

hostBuilder.Build().Run();