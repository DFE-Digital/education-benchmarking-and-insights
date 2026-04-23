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

public class QuerySchoolsFunction(IEnumerable<IQuerySchoolsHandler> handlers) : VersionedFunctionBase<IQuerySchoolsHandler, BasicContext>(handlers)
{
    [Function(nameof(QuerySchoolsFunction))]
    [OpenApiOperation(nameof(QuerySchoolsFunction), Constants.Features.Details, Summary = "Retrieves characteristics for multiple schools", Description = "Returns a collection of schools with their descriptive characteristics (pupil numbers, phases, types) based on a provided list of Unique Reference Numbers (URNs).")]
    [OpenApiUrnsParameter(Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolCharacteristicResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ProblemDetails))]

    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.SchoolsCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}