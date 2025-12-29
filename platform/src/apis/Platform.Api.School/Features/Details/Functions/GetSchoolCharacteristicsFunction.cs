using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.School.Features.Details.Handlers;
using Platform.Api.School.Features.Details.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.School.Features.Details.Functions;

public class GetSchoolCharacteristicsFunction(IVersionedHandlerDispatcher<IGetSchoolCharacteristicsHandler> dispatcher) : VersionedFunctionBase<IGetSchoolCharacteristicsHandler>(dispatcher)
{
    [Function(nameof(GetSchoolCharacteristicsFunction))]
    [OpenApiOperation(nameof(GetSchoolCharacteristicsFunction), Constants.Features.Details)]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolCharacteristicResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolCharacteristics)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, urn, token),
            token);
    }
}