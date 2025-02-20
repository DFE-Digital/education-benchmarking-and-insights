using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using Web.App.Services;

namespace Web.App.ActionResults;

public partial class CsvResultActionResultExecutor(ICsvService csvService, ILogger<CsvResultActionResultExecutor> logger) : IActionResultExecutor<CsvResult>
{
    public async Task ExecuteAsync(ActionContext context, CsvResult result)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(result);

        var response = context.HttpContext.Response;
        if (result.StatusCode != null)
        {
            response.StatusCode = result.StatusCode.Value;
        }

        response.ContentType = result.ContentType;

        if (!string.IsNullOrWhiteSpace(result.CsvFileName))
        {
            response.Headers.Append(HeaderNames.ContentDisposition, $"attachment; filename=\"{result.CsvFileName}\"");
        }

        Log.CsvResultExecuting(logger, result.Items);

        try
        {
            if (result.Items != null && result.Items.Any())
            {
                var csv = csvService.SaveToCsv(result.Items, result.Exclude);
                await response.WriteAsync(csv);
                await response.CompleteAsync();
            }
        }
        catch (OperationCanceledException) when (context.HttpContext.RequestAborted.IsCancellationRequested)
        {
        }
    }

    [ExcludeFromCodeCoverage]
    private static partial class Log
    {
        [LoggerMessage(1, LogLevel.Information, "Executing CsvResult, writing value of type '{Type}'.", EventName = "CsvResultExecuting", SkipEnabledCheck = true)]
        private static partial void CsvResultExecuting(ILogger logger, string? type);

        public static void CsvResultExecuting(ILogger logger, object? value)
        {
            if (!logger.IsEnabled(LogLevel.Information))
            {
                return;
            }

            var type = value == null ? "null" : value.GetType().FullName;
            CsvResultExecuting(logger, type);
        }
    }
}