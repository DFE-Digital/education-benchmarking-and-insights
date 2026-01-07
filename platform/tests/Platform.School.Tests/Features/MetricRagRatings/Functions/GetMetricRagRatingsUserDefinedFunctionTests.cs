using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.School.Features.MetricRagRatings.Handlers;
using Platform.Api.School.Features.MetricRagRatings.Functions;
using Platform.Functions;
using Platform.Test;

namespace Platform.School.Tests.Features.MetricRagRatings.Functions;

public class GetMetricRagRatingsUserDefinedFunctionTests : FunctionRunAsyncReflectionTestsBase<GetMetricRagRatingsUserDefinedFunction, IGetUserDefinedHandler, IdContext>
{
    protected override GetMetricRagRatingsUserDefinedFunction CreateFunction(IEnumerable<IGetUserDefinedHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request,"id", CancellationToken.None];
}