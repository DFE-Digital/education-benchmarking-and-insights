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

public class GetSchoolRisksHistoryFunction(IEnumerable<IGetSchoolRisksHistoryHandler> handlers) : VersionedFunctionBase<IGetSchoolRisksHistoryHandler, IdContext>(handlers)
{
    [Function(nameof(GetSchoolRisksHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetSchoolRisksHistoryFunction), Constants.Features.Risks, Summary = "Get schools risk history data", Description = "Returns a historical time-series of risk indicators data for a specific school")]
    [OpenApiUrnParameter]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SchoolRisksHistoryResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.History)] HttpRequestData req,
        string urn,
        CancellationToken token = default)
    {
        var context = new IdContext(req, token, urn);
        return await RunAsync(context);
    }
}
