using System;
using System.Collections.Generic;
using System.Linq;
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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.Census;

public class CensusFunctions(
    ILogger<CensusFunctions> logger,
    ICensusService service,
    IValidator<CensusParameters> censusParametersValidator,
    IValidator<CensusNationalAvgParameters> censusNationalAvgValidator,
    IValidator<QuerySchoolCensusParameters> querySchoolCensusParametersValidator)
{
    [Function(nameof(CensusAllCategories))]
    [OpenApiOperation(nameof(CensusAllCategories), "Census")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> CensusAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/categories")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(CensusCategories.All);
        }
    }

    [Function(nameof(CensusAllDimensions))]
    [OpenApiOperation(nameof(CensusAllDimensions), "Census")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> CensusAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/dimensions")] HttpRequestData req)
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

            return await req.CreateJsonResponseAsync(CensusDimensions.All);
        }
    }

    [Function(nameof(CensusAsync))]
    [OpenApiOperation(nameof(CensusAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                var validationResult = await censusParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.GetAsync(urn);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(CensusResponseFactory.Create(result, queryParams.Category, queryParams.Dimension));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(CustomCensusAsync))]
    [OpenApiOperation(nameof(CustomCensusAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = false, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CustomCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/custom/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
                var validationResult = await censusParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.GetCustomAsync(urn, identifier);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(CensusResponseFactory.Create(result, queryParams.Category, queryParams.Dimension));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get custom census");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(CensusHistoryAsync))]
    [OpenApiOperation(nameof(CensusHistoryAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/history")] HttpRequestData req,
        string urn,
        CancellationToken cancellationToken)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
            var validationResult = await censusParametersValidator.ValidateAsync(queryParams, cancellationToken);
            if (!validationResult.IsValid)
            {
                return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
            }

            var result = await service.GetHistoryAsync(urn, cancellationToken);
            return await req.CreateJsonResponseAsync(result.Select(x => CensusResponseFactory.Create(x, queryParams.Dimension)));
        }
    }

    [Function(nameof(CensusHistoryAvgComparatorSetAsync))]
    [OpenApiOperation(nameof(CensusHistoryAvgComparatorSetAsync), "Census")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CensusHistoryAvgComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census/{urn}/history/comparator-set-average")] HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<CensusParameters>();

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
            var validationResult = await censusParametersValidator.ValidateAsync(queryParams, token);
            if (!validationResult.IsValid)
            {
                return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
            }

            var result = await service.GetHistoryAvgComparatorSetAsync(urn, queryParams.Dimension, token);
            return await req.CreateJsonResponseAsync(result);
        }
    }

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
            var validationResult = await censusNationalAvgValidator.ValidateAsync(queryParams, token);
            if (!validationResult.IsValid)
            {
                return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
            }

            var result = await service.GetHistoryAvgNationalAsync(queryParams.Dimension, queryParams.OverallPhase, queryParams.FinanceType, token);
            return await req.CreateJsonResponseAsync(result);
        }
    }

    [Function(nameof(QueryCensusAsync))]
    [OpenApiOperation(nameof(QueryCensusAsync), "Census")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Census category", Type = typeof(string), Required = true, Example = typeof(ExampleCensusCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleCensusDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CensusResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryCensusAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "census")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<QuerySchoolCensusParameters>();

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
                var validationResult = await querySchoolCensusParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase);
                return await req.CreateJsonResponseAsync(result.Select(x => CensusResponseFactory.Create(x, queryParams.Category, queryParams.Dimension)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census");
                return req.CreateErrorResponse();
            }
        }
    }
}