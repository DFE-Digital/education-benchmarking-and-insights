using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Functions;

public interface IFunctionContextDataProvider
{
    ValueTask<HttpRequestData?> GetHttpRequestDataAsync(FunctionContext context);
    void SetInvocationResult(FunctionContext context, HttpResponseData result);
}

[ExcludeFromCodeCoverage]
public class FunctionContextDataProvider : IFunctionContextDataProvider
{
    public ValueTask<HttpRequestData?> GetHttpRequestDataAsync(FunctionContext context) => context.GetHttpRequestDataAsync();

    public void SetInvocationResult(FunctionContext context, HttpResponseData result)
    {
        context.GetInvocationResult().Value = result;
    }
}