using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Balance.Parameters;
using Platform.Api.Insight.Features.Balance.Responses;
using Platform.Api.Insight.Features.Balance.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Balance;

public class GetBalanceTrustHistoryFunction(IBalanceService service)
{
    //TODO: Consider adding validation for parameters
    [Function(nameof(GetBalanceTrustHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceTrustHistoryFunction), Constants.Features.Balance)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceHistoryResponse))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.TrustHistory)] HttpRequestData req,
        string companyNumber,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<BalanceParameters>();

        var (years, rows) = await service.GetTrustHistoryAsync(companyNumber, queryParams.Dimension, cancellationToken);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows), cancellationToken);
    }
}