using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Handlers;
using Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;

namespace Platform.Api.LocalAuthority.Features.EducationHealthCarePlans.Functions;

public class QueryEducationHealthCarePlansFunction(IVersionedHandlerDispatcher<IQueryEducationHealthCarePlansHandler> dispatcher) : VersionedFunctionBase<IQueryEducationHealthCarePlansHandler>(dispatcher)
{
    [Function(nameof(QueryEducationHealthCarePlansFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(QueryEducationHealthCarePlansFunction), Constants.Features.EducationHealthCarePlans)]
    [OpenApiParameter(Platform.Functions.Constants.ApiVersion, Type = typeof(string), Required = false, In = ParameterLocation.Header)]
    [OpenApiParameter("code", In = ParameterLocation.Query, Description = "List of local authority codes", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for resultant values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(EducationHealthCarePlansResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]

    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.EducationHealthCarePlansCollection)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}