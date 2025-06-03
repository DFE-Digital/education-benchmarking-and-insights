namespace Platform.Functions;

public interface IVersionedHandlerDispatcher<out THandler>
    where THandler : class, IVersionedHandler
{
    THandler? GetHandler(string? version);
}

public class VersionedHandlerDispatcher<THandler> : IVersionedHandlerDispatcher<THandler>
    where THandler : class, IVersionedHandler
{
    private readonly Dictionary<string, THandler> _handlers;
    private readonly THandler? _latestHandler;

    public VersionedHandlerDispatcher(IEnumerable<THandler> handlers)
    {
        var versionedHandlers = handlers as THandler[] ?? handlers.ToArray();

        _handlers = versionedHandlers.ToDictionary(h => h.Version);
        _latestHandler = versionedHandlers
            .OrderByDescending(h => Version.Parse(h.Version))
            .FirstOrDefault();
    }

    public THandler? GetHandler(string? version)
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