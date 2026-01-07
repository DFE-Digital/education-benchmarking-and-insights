using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Search.Functions;
using Platform.Api.LocalAuthority.Features.Search.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.Search.Functions;

public sealed class PostSuggestFunctionTests : FunctionRunAsyncReflectionTestsBase<PostSuggestFunction, IPostSuggestHandler, BasicContext>
{
    protected override PostSuggestFunction CreateFunction(IEnumerable<IPostSuggestHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}