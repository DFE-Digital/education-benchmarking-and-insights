using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.LocalAuthorities.Models;
using Platform.Api.Establishment.Features.LocalAuthorities.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.LocalAuthorities;

public class GetLocalAuthoritiesFunction(ILocalAuthoritiesService service)
{
    [Function(nameof(GetLocalAuthoritiesFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetLocalAuthoritiesFunction), Constants.Features.LocalAuthorities)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(IEnumerable<LocalAuthority>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.LocalAuthorities)] HttpRequestData req)
    {
        var response = await service.GetAllAsync();
        if (!response.Any())
        {
            return req.CreateNotFoundResponse();
        }

        return await req.CreateJsonResponseAsync(response);
    }
}