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

namespace Platform.Api.School.Features.Census.Functions;

public class GetNationalAverageHistoryFunction(IVersionedHandlerDispatcher<IGetNationalAverageHistoryHandler> dispatcher) : VersionedFunctionBase<IGetNationalAverageHistoryHandler>(dispatcher)
{
    [Function(nameof(GetNationalAverageHistoryFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetNationalAverageHistoryFunction), Constants.Features.Census)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.Dimension))]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Overall phase for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.Phase))]
    [OpenApiParameter("financeType", In = ParameterLocation.Query, Description = "Finance type for response values", Type = typeof(string), Required = true, Example = typeof(OpenApiExamples.FinanceTypes))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJsonProblem, typeof(ValidationProblemDetails), Description = "Validation errors or bad request.")]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = Routes.NationalAverageHistory)] HttpRequestData req,
        CancellationToken token = default)
    {
        return await WithHandlerAsync(
            req,
            handler => handler.HandleAsync(req, token),
            token);
    }
}