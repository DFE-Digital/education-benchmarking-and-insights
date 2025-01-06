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

public class BalanceTrustFunctions(ILogger<BalanceTrustFunctions> logger, IBalanceService service)
{
    //TODO: Add parameters to logger scope
    [Function(nameof(TrustBalanceAsync))]
    [OpenApiOperation(nameof(TrustBalanceAsync), "Balance")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BalanceTrustResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
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
                var result = await service.GetTrustAsync(companyNumber);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get balance");
                return req.CreateErrorResponse();
            }
        }
    }

    //TODO: Add parameters to logger scope
    //TODO: Add validation for dimension
    [Function(nameof(TrustBalanceHistoryAsync))]
    [OpenApiOperation(nameof(TrustBalanceHistoryAsync), "Balance")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BalanceHistoryResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustBalanceHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}/history")] HttpRequestData req,
        string companyNumber)
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
                var (years, rows) = await service.GetTrustHistoryAsync(companyNumber, queryParams.Dimension);
                return years == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust balance history");
                return req.CreateErrorResponse();
            }
        }
    }

    //TODO: Add parameters to logger scope
    //TODO: Add validation for companyNumbers and dimension
    //TODO: Consider replacing with comparator-set endpoint balance/trust/{companyNumber}/comparator-set/{id}
    [Function(nameof(QueryTrustsBalanceAsync))]
    [OpenApiOperation(nameof(QueryTrustsBalanceAsync), "Balance")]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleBalanceDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(BalanceTrustResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryTrustsBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trusts")] HttpRequestData req)
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
                var result = await service.QueryTrustsAsync(queryParams.Trusts, queryParams.Dimension);
                return await req.CreateJsonResponseAsync(result.MapToApiResponse());
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query trusts balance");
                return req.CreateErrorResponse();
            }
        }
    }
}