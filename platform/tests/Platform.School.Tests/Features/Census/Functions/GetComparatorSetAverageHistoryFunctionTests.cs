using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Functions;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Census.Functions;

public class GetComparatorSetAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetComparatorSetAverageHistoryFunction, IGetComparatorSetAverageHistoryHandler, IdContext>
{
    protected override GetComparatorSetAverageHistoryFunction CreateFunction(IEnumerable<IGetComparatorSetAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "urn", CancellationToken.None];
}