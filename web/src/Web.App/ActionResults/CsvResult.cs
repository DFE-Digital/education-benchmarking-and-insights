using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.Net.Http.Headers;

namespace Web.App.ActionResults;

public class CsvResult(IEnumerable<object>? items, string? csvFileName = null, string[]? exclude = null) : ActionResult, IStatusCodeActionResult
{
    /// <summary>
    ///     Gets the <see cref="System.Net.Http.Headers.MediaTypeHeaderValue" /> representing the Content-Type header
    ///     of the response.
    /// </summary>
    public string? ContentType { get; } = new MediaTypeHeaderValue("text/csv")
    {
        Encoding = Encoding.UTF8
    }.ToString();

    /// <summary>
    ///     Gets the items to be formatted.
    /// </summary>
    public IEnumerable<object>? Items { get; } = items;

    /// <summary>
    ///     Gets the columns to be excluded from the output file.
    /// </summary>
    public string[] Exclude { get; } = exclude ?? [];

    /// <summary>
    ///     Gets the target file name for the output file.
    /// </summary>
    public string? CsvFileName { get; } = csvFileName;

    /// <summary>
    ///     Gets the HTTP status code.
    /// </summary>
    public int? StatusCode => (int)HttpStatusCode.OK;

    public override Task ExecuteResultAsync(ActionContext context)
    {
        ArgumentNullException.ThrowIfNull(context);

        var services = context.HttpContext.RequestServices;
        var executor = services.GetRequiredService<IActionResultExecutor<CsvResult>>();
        return executor.ExecuteAsync(context, this);
    }
}