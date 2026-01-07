using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Functions;

public interface IVersionedHandler<in TContext>
    where TContext : HandlerContext
{
    string Version { get; }
    Task<HttpResponseData> HandleAsync(TContext context);
}


public abstract record HandlerContext(HttpRequestData Request, CancellationToken Token);

public sealed record BasicContext(HttpRequestData Request, CancellationToken Token) : HandlerContext(Request, Token);

public sealed record IdContext(HttpRequestData Request, CancellationToken Token, string Id) : HandlerContext(Request, Token);

public sealed record IdPairContext(HttpRequestData Request, CancellationToken Token, string Id1, string Id2) : HandlerContext(Request, Token);