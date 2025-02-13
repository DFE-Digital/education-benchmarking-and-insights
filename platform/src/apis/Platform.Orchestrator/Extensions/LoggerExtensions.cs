using System;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Platform.Orchestrator.Extensions;

public static class LoggerExtensions
{
    public static IDisposable? BeginApplicationScope(this ILogger? logger) => logger?.BeginScope(new Dictionary<string, object>
    {
        { "Application", Constants.ApplicationName }
    });
}