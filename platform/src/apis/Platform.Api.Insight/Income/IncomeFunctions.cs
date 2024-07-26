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
namespace Platform.Api.Insight.Income;

public class IncomeFunctions(ILogger<IncomeFunctions> logger, IIncomeService service)
{
    [Function(nameof(IncomeAllCategories))]
    [OpenApiOperation(nameof(IncomeAllCategories), "Income")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> IncomeAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/categories")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(IncomeCategories.All);
        }
    }

    [Function(nameof(IncomeAllDimensions))]
    [OpenApiOperation(nameof(IncomeAllDimensions), "Income")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> IncomeAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/dimensions")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(IncomeDimensions.All);
        }
    }

    [Function(nameof(SchoolIncomeAsync))]
    [OpenApiOperation(nameof(SchoolIncomeAsync), "Income")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Income category", Type = typeof(string), Example = typeof(ExampleIncomeCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolIncomeResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(IncomeResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school income");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(TrustIncomeAsync))]
    [OpenApiOperation(nameof(TrustIncomeAsync), "Income")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Income category", Type = typeof(string), Example = typeof(ExampleIncomeCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustIncomeResponse))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                    : await req.CreateJsonResponseAsync(IncomeResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get income");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SchoolIncomeHistoryAsync))]
    [OpenApiOperation(nameof(SchoolIncomeHistoryAsync), "Income")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolIncomeHistoryResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/school/{urn}/history")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                return await req.CreateJsonResponseAsync(result.Select(x => IncomeResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school income history");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(TrustIncomeHistoryAsync))]
    [OpenApiOperation(nameof(TrustIncomeHistoryAsync), "Income")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustIncomeHistoryResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustIncomeHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trust/{companyNumber}/history")] HttpRequestData req,
        string companyNumber)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                return await req.CreateJsonResponseAsync(result.Select(x => IncomeResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust income history");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(QuerySchoolsIncomeAsync))]
    [OpenApiOperation(nameof(QuerySchoolsIncomeAsync), "Income")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Income category", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolIncomeResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/schools")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                //TODO: Add validation for urns, category and dimension
                var result = await service.QuerySchoolsAsync(queryParams.Schools);
                return await req.CreateJsonResponseAsync(result.Select(x => IncomeResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query schools income");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(QueryTrustsIncomeAsync))]
    [OpenApiOperation(nameof(QueryTrustsIncomeAsync), "Income")]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Income category", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustIncomeResponse[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryTrustsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trusts")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<IncomeParameters>();

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
                //TODO: Add validation for companyNumbers, category and dimension
                var result = await service.QueryTrustsAsync(queryParams.Trusts);
                return await req.CreateJsonResponseAsync(result.Select(x => IncomeResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query trusts income");
                return req.CreateErrorResponse();
            }
        }
    }
}