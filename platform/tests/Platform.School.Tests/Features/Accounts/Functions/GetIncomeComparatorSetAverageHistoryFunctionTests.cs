using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetIncomeComparatorSetAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetIncomeComparatorSetAverageHistoryFunction, IGetIncomeComparatorSetAverageHistoryHandler, IdContext>
{
    protected override GetIncomeComparatorSetAverageHistoryFunction CreateFunction(IEnumerable<IGetIncomeComparatorSetAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}