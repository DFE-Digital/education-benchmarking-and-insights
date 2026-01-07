using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.Census.Functions;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.Census.Functions;

public class GetNationalAverageHistoryFunctionTests : FunctionRunAsyncReflectionTestsBase<GetNationalAverageHistoryFunction, IGetNationalAverageHistoryHandler, BasicContext>
{
    protected override GetNationalAverageHistoryFunction CreateFunction(IEnumerable<IGetNationalAverageHistoryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}