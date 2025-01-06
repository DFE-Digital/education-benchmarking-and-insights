using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Api.Insight.OpenApi.Examples;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Balance;

public class BalanceSchoolFunctions(ILogger<BalanceSchoolFunctions> logger, IBalanceService service)
{
    [Function(nameof(SchoolBalanceAsync))]
    [OpenApiOperation(nameof(SchoolBalanceAsync), "Balance")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BalanceSchoolResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
               }))
        {
            try
            {
                var result = await service.GetSchoolAsync(urn);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school balance");
                return req.CreateErrorResponse();
            }
        }
    }

    //TODO: Add validation for dimension
    //TODO: Add parameters to logger scope
    [Function(nameof(SchoolBalanceHistoryAsync))]
    [OpenApiOperation(nameof(SchoolBalanceHistoryAsync), "Balance")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BalanceHistoryResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}/history")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<BalanceParameters>();

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
            try
            {

                var (years, rows) = await service.GetSchoolHistoryAsync(urn, queryParams.Dimension);
                return years == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school balance history");
                return req.CreateErrorResponse();
            }
        }
    }
}