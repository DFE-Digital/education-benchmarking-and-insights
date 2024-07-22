using System;
using System.Collections.Generic;
using System.Linq;
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

public class BalanceFunctions(ILogger<BalanceFunctions> logger, IBalanceService service)
{
    [Function(nameof(BalanceAllDimensions))]
    [OpenApiOperation(nameof(BalanceAllDimensions), "Balance")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> BalanceAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/dimensions")] HttpRequestData req)
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

            return await req.CreateJsonResponseAsync(BalanceDimensions.All);
        }
    }

    [Function(nameof(SchoolBalanceAsync))]
    [OpenApiOperation(nameof(SchoolBalanceAsync), "Balance")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolBalanceResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/school/{urn}")] HttpRequestData req,
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
                var result = await service.GetSchoolAsync(urn);
                return result == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(BalanceResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school balance");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(TrustBalanceAsync))]
    [OpenApiOperation(nameof(TrustBalanceAsync), "Balance")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustBalanceResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/trust/{companyNumber}")] HttpRequestData req,
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
                var result = await service.GetTrustAsync(companyNumber);
                return result == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(BalanceResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get balance");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(SchoolBalanceHistoryAsync))]
    [OpenApiOperation(nameof(SchoolBalanceHistoryAsync), "Balance")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolBalanceHistoryResponse))]
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
                //TODO: Add validation for dimension
                var result = await service.GetSchoolHistoryAsync(urn);
                return await req.CreateJsonResponseAsync(result.Select(x => BalanceResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school balance history");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(TrustBalanceHistoryAsync))]
    [OpenApiOperation(nameof(TrustBalanceHistoryAsync), "Balance")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustBalanceHistoryResponse[]))]
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
                //TODO: Add validation for dimension
                var result = await service.GetTrustHistoryAsync(companyNumber);
                return await req.CreateJsonResponseAsync(result.Select(x => BalanceResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust balance history");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QuerySchoolsBalanceAsync))]
    [OpenApiOperation(nameof(QuerySchoolsBalanceAsync), "Balance")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolBalanceResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsBalanceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "balance/schools")] HttpRequestData req)
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
                //TODO: Add validation for urns and dimension
                var result = await service.QuerySchoolsAsync(queryParams.Schools);
                return await req.CreateJsonResponseAsync(result.Select(x => BalanceResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query schools balance");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QueryTrustsBalanceAsync))]
    [OpenApiOperation(nameof(QueryTrustsBalanceAsync), "Balance")]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleBalanceDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustBalanceResponse[]))]
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
                //TODO: Add validation for companyNumbers and dimension
                var result = await service.QueryTrustsAsync(queryParams.Trusts);
                return await req.CreateJsonResponseAsync(result.Select(x => BalanceResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query trusts balance");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}