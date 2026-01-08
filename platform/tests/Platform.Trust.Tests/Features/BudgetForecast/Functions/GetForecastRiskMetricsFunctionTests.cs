using Microsoft.Azure.Functions.Worker.Http;
using Platform.Api.Trust.Features.BudgetForecast.Functions;
using Platform.Api.Trust.Features.BudgetForecast.Handlers;
using Platform.Functions;
using Platform.Test;

namespace Platform.Trust.Tests.Features.BudgetForecast.Functions;

public class GetForecastRiskMetricsFunctionTests : FunctionRunAsyncReflectionTestsBase<GetForecastRiskMetricsFunction, IGetForecastRiskMetricsHandler, IdContext>
{
    protected override GetForecastRiskMetricsFunction CreateFunction(IEnumerable<IGetForecastRiskMetricsHandler> handlers) => new(handlers);

    protected override object[] GetRunAsyncArguments(HttpRequestData request) => [request, "companyNumber", CancellationToken.None];
}