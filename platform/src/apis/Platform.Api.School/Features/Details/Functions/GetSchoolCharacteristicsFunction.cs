using System.Collections.Generic;
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
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Details.Functions;

public class GetSchoolCharacteristicsFunction(IEnumerable<IGetSchoolCharacteristicsHandler> handlers) : VersionedFunctionBase<IGetSchoolCharacteristicsHandler, IdContext>(handlers)
{
    [Function(nameof(GetSchoolCharacteristicsFunction))]
    [OpenApiOperation(nameof(GetSchoolCharacteristicsFunction), Constants.Features.Details, Summary = "Retrieves a school's descriptive characteristics", Description = "Returns the benchmarking characteristics for a specific school, such as total pupils, free school meal percentages, and building information, identified by its 6-digit Unique Reference Number (URN).")]
    [OpenApiUrnParameter]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolCharacteristicResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolCharacteristics)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}