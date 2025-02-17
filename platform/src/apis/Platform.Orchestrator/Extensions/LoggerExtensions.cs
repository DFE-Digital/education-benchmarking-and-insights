﻿using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.Logging;

namespace Platform.Orchestrator.Extensions;

[ExcludeFromCodeCoverage]
public static class LoggerExtensions
{
    public static IDisposable? BeginApplicationScope(this ILogger? logger, string? jobId = null)
    {
        var args = new Dictionary<string, object>
        {
            { "Application", Constants.ApplicationName }
        };

        if (!string.IsNullOrWhiteSpace(jobId))
        {
            args.Add("JobId", jobId);
        }

        return logger?.BeginScope(args);
    }
}