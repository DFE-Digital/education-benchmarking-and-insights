using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Establishment.Features.Trusts;

public class GetTrustFunction(ITrustsService service)
{
    [Function(nameof(GetTrustFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetTrustFunction), Constants.Features.Trusts)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(Trust))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Trust)]
        HttpRequestData req,
        string identifier)
    {
        var response = await service.GetAsync(identifier);
        if (response == null)
        {
            return req.CreateNotFoundResponse();
        }

        return await req.CreateJsonResponseAsync(response);
    }
}