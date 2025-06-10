using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.Content.Features.Years.Models;
using Platform.Api.Content.Features.Years.Services;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Content.Features.Years;

public class GetCurrentReturnYearsFunction(IYearsService service)
{
    [Function(nameof(GetCurrentReturnYearsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCurrentReturnYearsFunction), Constants.Features.Years)]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(FinanceYears))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.CurrentReturn)] HttpRequestData req,
        CancellationToken cancellationToken = default)
    {
        var result = await service.GetCurrentReturnYears(cancellationToken);
        return await req.CreateJsonResponseAsync(result, cancellationToken);
    }
}