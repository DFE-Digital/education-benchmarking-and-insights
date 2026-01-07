using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Functions;
using Platform.Api.LocalAuthority.Features.StatisticalNeighbours.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.LocalAuthority.Tests.Features.StatisticalNeighbours.Functions;

public sealed class QueryHighNeedsFunctionTests : FunctionRunAsyncReflectionTestsBase<GetStatisticalNeighboursFunction, IGetStatisticalNeighboursHandler, IdContext>
{
    protected override GetStatisticalNeighboursFunction CreateFunction(IEnumerable<IGetStatisticalNeighboursHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "code", CancellationToken.None];
}