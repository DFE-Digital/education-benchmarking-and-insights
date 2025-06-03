using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;

namespace Platform.Functions;

public abstract class VersionedFunctionBase<THandler, TParam>(IVersionedHandlerDispatcher<THandler> dispatcher) where THandler : class, IVersionedHandler
{
    protected async Task<HttpResponseData> HandleAsync(
        HttpRequestData request,
        TParam param,
        Func<THandler, HttpRequestData, TParam, CancellationToken, Task<HttpResponseData>> handlerFunc,
        CancellationToken cancellationToken = default)
    {
        var version = request.ReadVersion();
        var handler = dispatcher.GetHandler(version);

        if (handler is null)
        {
            return await request.CreateUnsupportedVersionResponseAsync(cancellationToken);
        }

        return await handlerFunc(handler, request, param, cancellationToken);
    }
}