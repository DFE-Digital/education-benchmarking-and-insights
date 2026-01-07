using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Functions;
using Platform.Api.School.Features.MetricRagRatings.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.MetricRagRatings.Functions;

public class QueryDetailsFunctionTests : FunctionRunAsyncReflectionTestsBase<QueryDetailsFunction, IQueryDetailsHandler, BasicContext>
{
    protected override QueryDetailsFunction CreateFunction(IEnumerable<IQueryDetailsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}