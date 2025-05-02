using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.Balance.Responses;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Balance;

public class GetBalanceTrustFunction(IBalanceService service)
{
    [Function(nameof(GetBalanceTrustFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceTrustFunction), Constants.Features.Balance)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceTrustResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Trust)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetTrustAsync(companyNumber, cancellationToken);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse(), cancellationToken: cancellationToken);
    }
}