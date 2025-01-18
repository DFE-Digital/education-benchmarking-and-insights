using System.Net;
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

public class GetBalanceTrustsFunction(IBalanceService service)
{
    //TODO: Consider adding validation for parameters
    //TODO: Consider replacing with comparator-set endpoint balance/trust/{companyNumber}/comparator-set/{id}
    [Function(nameof(GetBalanceTrustsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetBalanceTrustsFunction), Constants.Features.Balance)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionFinance))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(BalanceTrustResponse[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "balance/trusts")] HttpRequestData req)
    {
        var queryParams = req.GetParameters<BalanceQueryTrustsParameters>();

        var result = await service.QueryTrustsAsync(queryParams.Trusts, queryParams.Dimension);
        return await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }
}