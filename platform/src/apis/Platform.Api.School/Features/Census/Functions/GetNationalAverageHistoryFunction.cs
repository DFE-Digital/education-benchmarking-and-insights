using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.School.Features.Census.Handlers;
using Platform.Api.School.Features.Census.Models;
using Platform.Functions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Attributes;

namespace Platform.Api.School.Features.Census.Functions;

public class GetNationalAverageHistoryFunction(IEnumerable<IGetNationalAverageHistoryHandler> handlers) : VersionedFunctionBase<IGetNationalAverageHistoryHandler, BasicContext>(handlers)
{
    [Function(nameof(GetNationalAverageHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetNationalAverageHistoryFunction), Constants.Features.Census, Summary = "Get school census national average history", Description = "Returns historical national average census and workforce data for schools, filtered by finance type and phase.")]
    [OpenApiDimensionParameter(Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiPhaseParameter(Required = true, Example = typeof(OpenApiExamples.Phase))]
    [OpenApiFinanceTypeParameter(Required = true, Example = typeof(OpenApiExamples.FinanceTypes))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.NationalAverageHistory)] HttpRequestData req,
        CancellationToken token = default)
    {
        var context = new BasicContext(req, token);
        return await RunAsync(context);
    }
}