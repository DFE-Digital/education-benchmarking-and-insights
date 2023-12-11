using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CorrelationId.DependencyInjection;
using Microsoft.ApplicationInsights.Extensibility;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Core;
using Serilog.Events;
using Serilog.Extensions.Logging;
using ILogger = Microsoft.Extensions.Logging.ILogger;

namespace EducationBenchmarking.Web.Extensions;

public static class ServiceCollection
{
    public static void SetupLogging(this IServiceCollection services, string applicationName)
    {
        var logger = CreateLogger(applicationName);
        services.AddLogging(lb =>
        {
            var provider = new SerilogLoggerProvider(logger);
            lb.AddProvider(provider);
            services.AddSingleton(typeof(ILogger), provider.CreateLogger(applicationName));
        });
    }
    
    public static Logger CreateLogger(string applicationName)
    {
        var instrumentationKey = Environment.GetEnvironmentVariable("APPINSIGHTS_INSTRUMENTATIONKEY");

        return new LoggerConfiguration()
#if !DEBUG
                .MinimumLevel.Information()
                .MinimumLevel.Override("Microsoft", LogEventLevel.Warning)
#endif
            .Enrich.FromLogContext()
            .Enrich.WithProperty("Application", applicationName)
            .Enrich.WithCorrelationIdHeader(Constants.CorrelationIdHeader)
            .WriteTo.Console()
            .WriteTo.ApplicationInsights(new TelemetryConfiguration(instrumentationKey), TelemetryConverter.Traces)
            .CreateLogger();
    }
}