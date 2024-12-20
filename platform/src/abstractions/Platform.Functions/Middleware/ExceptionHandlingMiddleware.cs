using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
// ReSharper disable ClassNeverInstantiated.Global
namespace Platform.Functions.Middleware;

[ExcludeFromCodeCoverage]
public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context);
        }
        catch (TaskCanceledException ex)
        {
            logger.LogInformation(ex, "The request was cancelled upon request by the client");

            // ideally the unofficial status code 499 (Client Closed Request) would be set but
            // `HttpResponseData.StatusCode` is a `HttpStatusCode` rather than `StatusCode` or `int` 
            await WriteErrorResponse(context, "Client Closed Request");
        }
        catch (Exception e)
        {
            logger.LogError(e, "Failed to complete request");
            await WriteErrorResponse(context);
        }
    }

    private static async Task WriteErrorResponse(FunctionContext context, string? content = null)
    {
        var request = await context.GetHttpRequestDataAsync();
        var response = request!.CreateErrorResponse();

        if (!string.IsNullOrWhiteSpace(content))
        {
            await response.WriteStringAsync(content);
        }

        context.GetInvocationResult().Value = response;
    }
}