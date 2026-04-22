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
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.School.Features.Details.Functions;

public class GetSchoolFunction(IEnumerable<IGetSchoolHandler> handlers) : VersionedFunctionBase<IGetSchoolHandler, IdContext>(handlers)
{
    [Function(nameof(GetSchoolFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolFunction), Constants.Features.Details, Summary = "Retrieves a single school's core details", Description = "Returns the fundamental details for a specific school, including its name, address, contact information, and trust affiliation, identified by its 6-digit Unique Reference Number (URN).")]
    [OpenApiUrnParameter]
    [OpenApiApiVersionParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolSingle)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}