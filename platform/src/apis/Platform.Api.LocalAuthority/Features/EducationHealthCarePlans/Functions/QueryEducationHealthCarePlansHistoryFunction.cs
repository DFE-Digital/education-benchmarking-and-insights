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

public class QueryEducationHealthCarePlansHistoryFunction(IEnumerable<IQueryEducationHealthCarePlansHistoryHandler> handlers) : VersionedFunctionBase<IQueryEducationHealthCarePlansHistoryHandler, BasicContext>(handlers)
{
    [Function(nameof(QueryEducationHealthCarePlansHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryEducationHealthCarePlansHistoryFunction), Constants.Features.EducationHealthCarePlans, Summary = "Get education, health and care plans history", Description = "Returns the historical education, health and care plans data for the specified local authorities.")]
    [OpenApiApiVersionParameter]
    [OpenApiLaCodesParameter("code", isRequired: true)]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(EducationHealthCarePlansYearHistory), Description = "The historical education, health and care plans data for the requested local authorities")]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "The request was invalid")]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound, Description = "The requested local authorities could not be found")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.EducationHealthCarePlansHistoryCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}