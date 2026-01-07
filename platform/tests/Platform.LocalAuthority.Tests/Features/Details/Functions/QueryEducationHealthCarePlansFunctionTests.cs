using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Details.Functions;
using Platform.Api.LocalAuthority.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.Details.Functions;

public sealed class QueryLocalAuthoritiesFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryLocalAuthoritiesFunction, IQueryLocalAuthoritiesHandler, BasicContext>
{
    protected override QueryLocalAuthoritiesFunction CreateFunction(IEnumerable<IQueryLocalAuthoritiesHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}