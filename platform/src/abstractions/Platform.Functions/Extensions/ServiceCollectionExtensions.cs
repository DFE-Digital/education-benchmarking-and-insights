using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Extensions.Logging;
//DO NOT REMOVE - require when !DEBUG
using Serilog.Events;
using Microsoft.ApplicationInsights;

namespace Platform.Functions.Extensions;

[ExcludeFromCodeCoverage]
public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSerilogLoggerProvider(this IServiceCollection serviceCollection, string applicationName)
    {
        serviceCollection.AddSingleton<ILoggerProvider>((sp) =>
        {
            Log.Logger = new LoggerConfiguration()
#if !DEBUG
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
                .WriteTo.ApplicationInsights(sp.GetRequiredService<TelemetryClient>(), TelemetryConverter.Traces)
#else
                .WriteTo.Console()
#endif
                .Enrich.FromLogContext()
                .Enrich.WithProperty("Application", applicationName)
                .Enrich.WithCorrelationIdHeader()
                .Enrich.FromLogContext()
                .CreateLogger();
            return new SerilogLoggerProvider(Log.Logger, true);
        });

        return serviceCollection;
    }
}