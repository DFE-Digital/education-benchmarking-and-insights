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

public class GetForecastRiskFunction(IEnumerable<IGetForecastRiskMHandler> handlers) : VersionedFunctionBase<IGetForecastRiskMHandler, IdContext>(handlers)
{
    [Function(nameof(GetForecastRiskFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetForecastRiskFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("runType", In = ParameterLocation.Query, Description = "Forecast run type", Type = typeof(string), Example = typeof(OpenApiExamples.BudgetForecastRunType))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Forecast run category", Type = typeof(string), Required = false, Example = typeof(OpenApiExamples.BudgetForecastRunCategory))]
    [OpenApiParameter("runId", In = ParameterLocation.Query, Description = "Forecast run identifier or year", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.BudgetForecastRunId))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ForecastRiskResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.ForecastRiskSingle)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, companyNumber);
        return await RunAsync(context);
    }
}