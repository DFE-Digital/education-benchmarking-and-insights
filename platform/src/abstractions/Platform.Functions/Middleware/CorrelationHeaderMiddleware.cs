using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Middleware;
using Platform.Functions.Extensions;

// ReSharper disable ClassNeverInstantiated.Global
namespace Platform.Functions.Middleware;

[ExcludeFromCodeCoverage]
public sealed class CorrelationHeaderMiddleware : IFunctionsWorkerMiddleware
{
    public async Task Invoke(FunctionContext context, FunctionExecutionDelegate next)
    {
        var requestData = await context.GetHttpRequestDataAsync();
        var correlationId = requestData?.GetCorrelationId() ?? Guid.Empty;

        await next(context);

        context.GetHttpResponseData()?.Headers.Add(Constants.CorrelationIdHeader, correlationId.ToString());
    }
}