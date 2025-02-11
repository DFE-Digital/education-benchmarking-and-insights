using System.Diagnostics.CodeAnalysis;
using System.IO.Compression;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;
using Web.App.Services;

namespace Web.App.ActionResults;

public partial class CsvResultsActionResultExecutor(ICsvService csvService, ILogger<CsvResultsActionResultExecutor> logger) : IActionResultExecutor<CsvResults>
{
    public async Task ExecuteAsync(ActionContext context, CsvResults result)
    {
        ArgumentNullException.ThrowIfNull(context);
        ArgumentNullException.ThrowIfNull(result);

        var response = context.HttpContext.Response;
        if (result.StatusCode != null)
        {
            response.StatusCode = result.StatusCode.Value;
        }

        response.ContentType = result.ContentType;

        if (!string.IsNullOrWhiteSpace(result.ZipFileName))
        {
            response.Headers.Append(HeaderNames.ContentDisposition, $"attachment; filename=\"{result.ZipFileName}\"");
        }

        Log.CsvResultsExecuting(logger, result.Items);

        try
        {
            if (result.Items.Any())
            {
                using var zipStream = new MemoryStream();
                using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                {
                    var i = 0;
                    foreach (var item in result.Items.Where(m => m.Items != null))
                    {
                        i++;
                        var entry = archive.CreateEntry(item.CsvFileName ?? $"file-{i}.csv");
                        await using var entryStream = entry.Open();
                        await using var writer = new StreamWriter(entryStream, Encoding.UTF8);
                        var csv = csvService.SaveToCsv(item.Items!);
                        await writer.WriteAsync(csv);
                    }
                }

                zipStream.Seek(0, SeekOrigin.Begin);
                await response.Body.WriteAsync(zipStream.ToArray());
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
        [LoggerMessage(1, LogLevel.Information, "Executing CsvResults, writing value of type '{Type}'.", EventName = "CsvResultsExecuting", SkipEnabledCheck = true)]
        private static partial void CsvResultsExecuting(ILogger logger, string? type);

        public static void CsvResultsExecuting(ILogger logger, object? value)
        {
            if (!logger.IsEnabled(LogLevel.Information))
            {
                return;
            }

            var type = value == null ? "null" : value.GetType().FullName;
            CsvResultsExecuting(logger, type);
        }
    }
}