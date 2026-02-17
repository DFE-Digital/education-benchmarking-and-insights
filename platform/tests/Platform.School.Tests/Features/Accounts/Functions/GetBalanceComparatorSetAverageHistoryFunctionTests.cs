using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetBalanceComparatorSetAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetBalanceComparatorSetAverageHistoryFunction, IGetBalanceComparatorSetAverageHistoryHandler, IdContext>
{
    protected override GetBalanceComparatorSetAverageHistoryFunction CreateFunction(IEnumerable<IGetBalanceComparatorSetAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}