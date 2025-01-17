using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Balance;

public class BalanceSchoolFunctions(IBalanceService service)
{
    [Function(nameof(SchoolBalanceAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SchoolBalanceAsync), Constants.Features.Balance)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceSchoolResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> SchoolBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/school/{urn}")] HttpRequestData req,
        string urn)
    {
        var result = await service.GetSchoolAsync(urn);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }

    //TODO: Consider adding validation for parameters
    [Function(nameof(SchoolBalanceHistoryAsync))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(SchoolBalanceHistoryAsync), Constants.Features.Balance)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceHistoryResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> SchoolBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/school/{urn}/history")] HttpRequestData req,
        string urn)
    {
        var queryParams = req.GetParameters<BalanceParameters>();

        var (years, rows) = await service.GetSchoolHistoryAsync(urn, queryParams.Dimension);
        return years == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }
}