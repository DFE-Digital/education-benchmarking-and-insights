using System.Diagnostics.CodeAnalysis;
using Microsoft.Azure.Functions.Worker.Http;

namespace Platform.Functions;

public interface IVersionedHandler<in TContext>
    where TContext : HandlerContext
{
    string Version { get; }
    Task<HttpResponseData> HandleAsync(TContext context);
}


[ExcludeFromCodeCoverage]
public abstract record HandlerContext(HttpRequestData Request, CancellationToken Token);

[ExcludeFromCodeCoverage]
public sealed record BasicContext(HttpRequestData Request, CancellationToken Token) : HandlerContext(Request, Token);

[ExcludeFromCodeCoverage]
public sealed record IdContext(HttpRequestData Request, CancellationToken Token, string Id) : HandlerContext(Request, Token);

[ExcludeFromCodeCoverage]
public sealed record IdPairContext(HttpRequestData Request, CancellationToken Token, string Id1, string Id2) : HandlerContext(Request, Token);