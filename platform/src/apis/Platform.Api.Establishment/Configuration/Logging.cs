﻿using System.Diagnostics.CodeAnalysis;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Platform.Api.Establishment.Configuration;

[ExcludeFromCodeCoverage]
internal static class Logging
{
    internal static void Configure(ILoggingBuilder builder)
    {
        builder.Services.Configure<LoggerFilterOptions>(_ => { });
    }
}