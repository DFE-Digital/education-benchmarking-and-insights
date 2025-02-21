using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class GetLocalAuthoritiesNationalRankFunction(ILocalAuthorityRankingService service)
{
    [Function(nameof(GetLocalAuthoritiesNationalRankFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthoritiesNationalRankFunction), Constants.Features.LocalAuthorities)]
    [OpenApiParameter("sort", In = ParameterLocation.Query, Description = "Sort order for ranking", Type = typeof(string), Required = false, Example = typeof(ExampleSort))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(LocalAuthorityRanking))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthoritiesNationalRank)] HttpRequestData req,
        string sort = "asc")
    {
        var response = await service.GetRanking(sort);
        return await req.CreateJsonResponseAsync(response);
    }
}