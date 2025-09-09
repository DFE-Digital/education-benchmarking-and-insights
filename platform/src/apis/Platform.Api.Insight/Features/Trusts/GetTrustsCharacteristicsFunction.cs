using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Trusts.Models;
using Platform.Api.Insight.Features.Trusts.Parameters;
using Platform.Api.Insight.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Features.Trusts;

public class GetTrustsCharacteristicsFunction(ITrustsService service)
{
    //TODO: Add parameters validator
    //TODO: Move to Establishment API
    [Function(nameof(GetTrustsCharacteristicsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustsCharacteristicsFunction), Constants.Features.Trust)]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(TrustCharacteristic[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Characteristics)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var queryParams = req.GetParameters<TrustsParameters>();

        var trusts = await service.QueryAsync(queryParams.Trusts, cancellationToken);
        return await req.CreateJsonResponseAsync(trusts, cancellationToken);
    }
}