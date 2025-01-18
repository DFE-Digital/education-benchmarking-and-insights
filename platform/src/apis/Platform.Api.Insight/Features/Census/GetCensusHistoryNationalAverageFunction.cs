using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.Features.Census.Parameters;
using Platform.Api.Insight.Features.Census.Responses;
using Platform.Api.Insight.Features.Census.Services;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Features.Census;

public class GetCensusHistoryNationalAverageFunction(ICensusService service, IValidator<CensusNationalAvgParameters> validator)
{
    [Function(nameof(GetCensusHistoryNationalAverageFunction))]
    [OpenApiSecurityHeader]
    [OpenApiOperation(nameof(GetCensusHistoryNationalAverageFunction), Constants.Features.Census)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleDimensionCensus))]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Overall phase for response values", Type = typeof(string), Required = true, Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("financeType", In = ParameterLocation.Query, Description = "Finance type for response values", Type = typeof(string), Required = true, Example = typeof(ExampleFinanceTypes))]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(CensusHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    public async Task<HttpResponseData> RunAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "census/history/national-average")]
        HttpRequestData req,
        CancellationToken token)
    {
        var queryParams = req.GetParameters<CensusNationalAvgParameters>();

        var validationResult = await validator.ValidateAsync(queryParams, token);
        if (!validationResult.IsValid)
        {
            return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
        }

        var (years, rows) = await service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType,
            queryParams.Dimension, token);
        return years == null
            ? await req.CreateJsonResponseAsync(new CensusHistoryResponse())
            : await req.CreateJsonResponseAsync(years.MapToApiResponse(rows));
    }
}