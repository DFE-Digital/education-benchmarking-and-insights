using Microsoft.Azure.Functions.Worker.Http;
using Platform.Functions.Extensions;

namespace Platform.Functions;


public abstract class VersionedFunctionBase<THandler, TContext>
    where THandler : class, IVersionedHandler<TContext>
    where TContext : HandlerContext
{
    private readonly Dictionary<string, THandler> _handlers;
    private readonly THandler? _latestHandler;

    protected VersionedFunctionBase(IEnumerable<THandler> handlers)
    {
        var versionedHandlers = handlers as THandler[] ?? handlers.ToArray();

        _handlers = versionedHandlers.ToDictionary(h => h.Version);
        _latestHandler = versionedHandlers
            .OrderByDescending(h => Version.Parse(h.Version))
            .FirstOrDefault();
    }

    protected async Task<HttpResponseData> RunAsync(TContext context)
    {
        var version = context.Request.ReadVersion();
        var handler = GetHandler(version);

        return handler is null
            ? await context.Request.CreateUnsupportedVersionResponseAsync(context.Token)
            : await handler.HandleAsync(context);
    }

    private THandler? GetHandler(string? version)
    {
        if (string.IsNullOrWhiteSpace(version))
        {
            return _latestHandler;
        }

        return _handlers.TryGetValue(version, out var handler)
            ? handler
            : null;
    }
}