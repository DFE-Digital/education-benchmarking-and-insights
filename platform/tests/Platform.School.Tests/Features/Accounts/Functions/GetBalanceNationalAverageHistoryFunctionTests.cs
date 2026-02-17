using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetBalanceNationalAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetBalanceNationalAverageHistoryFunction, IGetBalanceNationalAverageHistoryHandler, BasicContext>
{
    protected override GetBalanceNationalAverageHistoryFunction CreateFunction(IEnumerable<IGetBalanceNationalAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}