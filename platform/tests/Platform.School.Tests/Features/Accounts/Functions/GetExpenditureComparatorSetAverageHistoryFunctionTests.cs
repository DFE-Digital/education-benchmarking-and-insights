using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Accounts.Functions;
using Platform.Api.School.Features.Accounts.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Accounts.Functions;

public class GetExpenditureComparatorSetAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetExpenditureComparatorSetAverageHistoryFunction, IGetExpenditureComparatorSetAverageHistoryHandler, IdContext>
{
    protected override GetExpenditureComparatorSetAverageHistoryFunction CreateFunction(IEnumerable<IGetExpenditureComparatorSetAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}