using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Insight.Features.Income.Responses;
using Platform.Api.Insight.Features.Income.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Income;

public class GetIncomeSchoolFunction(IIncomeService service)
{
    [Function(nameof(GetIncomeSchoolFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetIncomeSchoolFunction), Constants.Features.Income)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(IncomeSchoolResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "income/school/{urn}")]
        HttpRequestData req,
        string urn)
    {
        var result = await service.GetSchoolAsync(urn);
        return result == null
            ? req.CreateNotFoundResponse()
            : await req.CreateJsonResponseAsync(result.MapToApiResponse());
    }
}