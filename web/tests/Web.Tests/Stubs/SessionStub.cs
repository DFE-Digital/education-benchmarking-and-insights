using System.Collections.Concurrent;
using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Http;

namespace Web.Tests.Stubs;

public class SessionStub(ConcurrentDictionary<string, byte[]>? items = null) : ISession
{
    private readonly ConcurrentDictionary<string, byte[]> _items = items ?? new ConcurrentDictionary<string, byte[]>();

    public Task LoadAsync(CancellationToken cancellationToken = new()) => throw new NotImplementedException();

    public Task CommitAsync(CancellationToken cancellationToken = new()) => throw new NotImplementedException();

    public bool TryGetValue(string key, [MaybeNullWhen(false)] out byte[] value) => _items.TryGetValue(key, out value);

    public void Set(string key, byte[] value)
    {
        _items[key] = value;
    }

    public void Remove(string key)
    {
        _items.Remove(key, out _);
    }

    public void Clear()
    {
        _items.Clear();
    }

    public bool IsAvailable => true;

    public string Id => nameof(SessionStub);

    public IEnumerable<string> Keys => _items.Keys;
}