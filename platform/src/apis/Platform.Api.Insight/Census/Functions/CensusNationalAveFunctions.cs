using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Cache;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.Census;

public class CensusNationalAveFunctions(
    ILogger<CensusNationalAveFunctions> logger,
    ICensusService service,
    IValidator<CensusNationalAvgParameters> validator,
    IDistributedCache cache,
    ICacheKeyFactory cacheKeyFactory)
{
    [Function(nameof(CensusHistoryAvgNationalAsync))]
    [OpenApiOperation(nameof(CensusHistoryAvgNationalAsync), "Census")]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Overall phase for response values", Type = typeof(string), Required = true, Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("financeType", In = ParameterLocation.Query, Description = "Finance type for response values", Type = typeof(string), Required = true, Example = typeof(ExampleFinanceTypes))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusHistoryAvgNationalAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/history/national-average")] HttpRequestData req,
        CancellationToken token)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusNationalAvgParameters>();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   }
               }))
        {
            var validationResult = await validator.ValidateAsync(queryParams, token);
            if (!validationResult.IsValid)
            {
                return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
            }

            var (years, rows) = await cache.GetSetAsync(
                cacheKeyFactory.CreateCensusHistoryCacheKey(queryParams.OverallPhase, queryParams.FinanceType, queryParams.Dimension),
                () => service.GetNationalAvgHistoryAsync(queryParams.OverallPhase, queryParams.FinanceType, queryParams.Dimension, token));
            return years == null
                ? req.CreateNotFoundResponse()
                : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
        }
    }
}