using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Trust.Features.BudgetForecast.Handlers;
using Platform.Api.Trust.Features.BudgetForecast.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Trust.Features.BudgetForecast.Functions;

public class GetForecastRiskMetricsFunction(IEnumerable<IGetForecastRiskMetricsHandler> handlers) : VersionedFunctionBase<IGetForecastRiskMetricsHandler, IdContext>(handlers)
{
    [Function(nameof(GetForecastRiskMetricsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetForecastRiskMetricsFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ForecastRiskMetricsResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.ForecastRiskMetrics)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, companyNumber);
        return await RunAsync(context);
    }
}