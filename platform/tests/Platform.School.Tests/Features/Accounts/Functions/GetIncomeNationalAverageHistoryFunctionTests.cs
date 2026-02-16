using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetIncomeNationalAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetIncomeNationalAverageHistoryFunction, IGetIncomeNationalAverageHistoryHandler, BasicContext>
{
    protected override GetIncomeNationalAverageHistoryFunction CreateFunction(IEnumerable<IGetIncomeNationalAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}