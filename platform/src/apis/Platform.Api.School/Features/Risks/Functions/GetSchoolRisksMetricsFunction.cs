using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Platform.Api.School.Features.Risks.Handlers;
using Platform.Api.School.Features.Risks.Models;
using Platform.Functions;
using Platform.OpenApi;
using Platform.OpenApi.Attributes;

namespace Platform.Api.School.Features.Risks.Functions;

public class GetSchoolRisksMetricsFunction(IEnumerable<IGetSchoolRisksMetricsHandler> handlers) : VersionedFunctionBase<IGetSchoolRisksMetricsHandler, IdContext>(handlers)
{
    [Function(nameof(GetSchoolRisksMetricsFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolRisksMetricsFunction), Constants.Features.Risks, Summary = "Get schools risk metric data", Description = "Returns a risk indicator metrics data for a specific school")]
    [OpenApiUrnParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolRisksMetricsResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.Metrics)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}
