using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;

namespace Platform.Functions;

public abstract class VersionedFunctionBase<THandler>(IVersionedHandlerDispatcher<THandler> dispatcher)
    where THandler : class, IVersionedHandler
{
    protected async Task<HttpResponseData> WithHandlerAsync(
        HttpRequestData request,
        Func<THandler, Task<HttpResponseData>> handlerInvoker,
        CancellationToken token)
    {
        var version = request.ReadVersion();
        var handler = dispatcher.GetHandler(version);

        return handler is null
            ? await request.CreateUnsupportedVersionResponseAsync(token)
            : await handlerInvoker(handler);
    }
}