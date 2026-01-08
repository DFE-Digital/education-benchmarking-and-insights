using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.Accounts.Functions;
using Platform.Api.Trust.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.Accounts.Functions;

public class QueryBalanceFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryBalanceFunction, IQueryBalanceHandler, BasicContext>
{
    protected override QueryBalanceFunction CreateFunction(IEnumerable<IQueryBalanceHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}