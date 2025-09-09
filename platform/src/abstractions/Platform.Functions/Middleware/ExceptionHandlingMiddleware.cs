using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;

namespace Platform.Functions.Middleware;

public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger, IFunctionContextDataProvider provider) : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var state = State(context);
        using (logger.BeginScope(state))
        {
            try
            {
                await next(context);
            }
            catch (Exception ex) when
                (ex is OperationCanceledException
                 || ex is TaskCanceledException
                 || (ex is SqlException && ex.Message.Contains("Operation cancelled by user")))
            {
                logger.LogInformation(ex, "The request was cancelled upon request by the client");
                await WriteErrorResponse(context, "Client Closed Request", 499);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to complete request");
                await WriteErrorResponse(context);
            }
        }
    }

    private async Task<Dictionary<string, object>> State(FunctionContext context)
    {
        var req = await provider.GetHttpRequestDataAsync(context);
        var correlationId = req?.GetCorrelationId() ?? Guid.Empty;

        return new Dictionary<string, object>
        {
            { "CorrelationID", correlationId }
        };
    }

    private async Task WriteErrorResponse(FunctionContext context, string? content = null, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        var request = await provider.GetHttpRequestDataAsync(context);
        var response = request!.CreateErrorResponse(statusCode);

        if (!string.IsNullOrWhiteSpace(content))
        {
            await response.WriteStringAsync(content);
        }

        provider.SetInvocationResult(context, response);
    }
}