using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Details.Functions;
using Platform.Api.Trust.Features.Details.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Details.Functions;

public class QueryTrustsFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryTrustsFunction, IQueryTrustsHandler, BasicContext>
{
    protected override QueryTrustsFunction CreateFunction(IEnumerable<IQueryTrustsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}