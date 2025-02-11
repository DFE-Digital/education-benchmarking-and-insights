using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace Web.App.ActionResults;

public class CsvResults(IEnumerable<CsvResult> items, string? zipFileName = null) : ActionResult, IStatusCodeActionResult
{
    /// <summary>
    ///     Gets the <see cref="System.Net.Http.Headers.MediaTypeHeaderValue" /> representing the Content-Type header
    ///     of the response.
    /// </summary>
    public string? ContentType { get; } = new MediaTypeHeaderValue("application/zip").ToString();

    /// <summary>
    ///     Gets the items to be formatted.
    /// </summary>
    public IEnumerable<CsvResult> Items { get; } = items;

    /// <summary>
    ///     Gets the target file name for the output file.
    /// </summary>
    public string? ZipFileName { get; } = zipFileName;

    /// <summary>
    ///     Gets the HTTP status code.
    /// </summary>
    public int? StatusCode => (int)HttpStatusCode.OK;

    public override Task ExecuteResultAsync(ActionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var services = context.HttpContext.RequestServices;
        var executor = services.GetRequiredService<IActionResultExecutor<CsvResults>>();
        return executor.ExecuteAsync(context, this);
    }
}