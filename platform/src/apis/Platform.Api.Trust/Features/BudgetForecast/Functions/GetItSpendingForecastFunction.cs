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

public class GetItSpendingForecastFunction(IVersionedHandlerDispatcher<IGetItSpendingForecastHandler> dispatcher) : VersionedFunctionBase<IGetItSpendingForecastHandler>(dispatcher)
{
    [Function(nameof(GetItSpendingForecastFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetItSpendingForecastFunction), Constants.Features.BudgetForecast)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ItSpendingForecastResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.ItSpendingForecast)] HttpRequestData req,
        string companyNumber,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, companyNumber, token),
            token);
    }
}