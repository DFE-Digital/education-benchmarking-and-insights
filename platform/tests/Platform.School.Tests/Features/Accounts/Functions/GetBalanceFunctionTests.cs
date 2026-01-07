using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetBalanceFunctionTests : FunctionRunAsyncReflectionTestsBase<GetBalanceFunction, IGetBalanceHandler, IdContext>
{
    protected override GetBalanceFunction CreateFunction(IEnumerable<IGetBalanceHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}