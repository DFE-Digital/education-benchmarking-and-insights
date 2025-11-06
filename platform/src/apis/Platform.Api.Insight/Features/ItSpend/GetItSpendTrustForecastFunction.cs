using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Extensions;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.ItSpend.Parameters;
using Platform.Api.Insight.Features.ItSpend.Responses;
using Platform.Api.Insight.Features.ItSpend.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.ItSpend;

public class GetItSpendTrustForecastFunction(IItSpendService service)
{
    [Function(nameof(GetItSpendTrustForecastFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetItSpendTrustForecastFunction), Constants.Features.ItSpend)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(ItSpendTrustForecastResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustItSpendForecast)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetTrustForecastAsync(companyNumber, cancellationToken);

        return result.IsEmpty()
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result, cancellationToken);
    }
}