using System.Diagnostics.CodeAnalysis;
using System.Net;
using System.Runtime.CompilerServices;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.Functions.Worker.Middleware;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
[assembly: InternalsVisibleTo("Platform.Functions.Tests")]
// ReSharper disable ClassNeverInstantiated.Global
namespace Platform.Functions.Middleware;

[ExcludeFromCodeCoverage]
public sealed class ExceptionHandlingMiddleware(ILogger<ExceptionHandlingMiddleware> logger) : IFunctionsWorkerMiddleware
{
    public Task Invoke(FunctionContext context, FunctionExecutionDelegate next) => Invoke(new FunctionContextWrapper(context), next);

    internal async Task Invoke(FunctionContextWrapper context, FunctionExecutionDelegate next)
    {
        try
        {
            await next(context.Context);
        }
        catch (Exception ex) when
            (ex is OperationCanceledException
             || ex is TaskCanceledException
             || ex is SqlException && ex.Message.Contains("Operation cancelled by user"))
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

    private static async Task WriteErrorResponse(FunctionContextWrapper context, string? content = null, int statusCode = (int)HttpStatusCode.InternalServerError)
    {
        var request = await context.GetHttpRequestDataAsync();
        var response = request!.CreateErrorResponse(statusCode);

        if (!string.IsNullOrWhiteSpace(content))
        {
            await response.WriteStringAsync(content);
        }

        context.SetInvocationResult(response);
    }
}

// ReSharper disable once ClassWithVirtualMembersNeverInherited.Global
public class FunctionContextWrapper(FunctionContext context)
{
    public FunctionContext Context => context;
    public virtual ValueTask<HttpRequestData?> GetHttpRequestDataAsync() => context.GetHttpRequestDataAsync();
    public virtual void SetInvocationResult(HttpResponseData result) => context.GetInvocationResult().Value = result;
}