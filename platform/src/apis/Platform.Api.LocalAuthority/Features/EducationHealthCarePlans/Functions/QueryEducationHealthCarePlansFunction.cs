using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Functions;

public class QueryEducationHealthCarePlansFunction(IEnumerable<IQueryEducationHealthCarePlansHandler> handlers) : VersionedFunctionBase<IQueryEducationHealthCarePlansHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryEducationHealthCarePlansFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryEducationHealthCarePlansFunction), Constants.Features.EducationHealthCarePlans, Summary = "Get education, health and care plans data", Description = "Returns the education, health and care plans data for the specified local authorities.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodesParameter("code", isRequired: true)]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(EducationHealthCarePlansResponse[]), Description = "The education, health and care plans data for the requested local authorities")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.EducationHealthCarePlansCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}
