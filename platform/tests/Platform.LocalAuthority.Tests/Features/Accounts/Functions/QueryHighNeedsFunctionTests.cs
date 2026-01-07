using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.Accounts.Functions;
using Platform.Api.LocalAuthority.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.Accounts.Functions;

public sealed class QueryHighNeedsFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryHighNeedsFunction, IQueryHighNeedsHandler, BasicContext>
{
    protected override QueryHighNeedsFunction CreateFunction(IEnumerable<IQueryHighNeedsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}