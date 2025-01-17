using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Balance;

public class BalanceTrustFunctions(IBalanceService service)
{
    [Function(nameof(TrustBalanceAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(TrustBalanceAsync), Constants.Features.Balance)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceTrustResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> TrustBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/trust/{companyNumber}")] HttpRequestData req,
        string companyNumber)
    {
        var result = await service.GetTrustAsync(companyNumber);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }

    //TODO: Consider adding validation for parameters
    [Function(nameof(TrustBalanceHistoryAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(TrustBalanceHistoryAsync), Constants.Features.Balance)]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceHistoryResponse[]))]
    public async Task<HttpResponseData> TrustBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/trust/{companyNumber}/history")] HttpRequestData req,
        string companyNumber)
    {
        var queryParams = req.GetParameters<BalanceParameters>();

        var (years, rows) = await service.GetTrustHistoryAsync(companyNumber, queryParams.Dimension);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
    }

    //TODO: Consider adding validation for parameters
    //TODO: Consider replacing with comparator-set endpoint balance/trust/{companyNumber}/comparator-set/{id}
    [Function(nameof(QueryTrustsBalanceAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryTrustsBalanceAsync), Constants.Features.Balance)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceTrustResponse[]))]
    public async Task<HttpResponseData> QueryTrustsBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/trusts")] HttpRequestData req)
    {
        var queryParams = req.GetParameters<BalanceQueryTrustsParameters>();

        var result = await service.QueryTrustsAsync(queryParams.Trusts, queryParams.Dimension);
        return await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }
}