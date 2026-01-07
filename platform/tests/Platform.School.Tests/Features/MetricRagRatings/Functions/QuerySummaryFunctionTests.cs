using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Functions;
using Platform.Api.School.Features.MetricRagRatings.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.MetricRagRatings.Functions;

public class QuerySummaryFunctionTests : FunctionRunAsyncReflectionTestsBase<QuerySummaryFunction, IQuerySummaryHandler, BasicContext>
{
    protected override QuerySummaryFunction CreateFunction(IEnumerable<IQuerySummaryHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, CancellationToken.None];
}