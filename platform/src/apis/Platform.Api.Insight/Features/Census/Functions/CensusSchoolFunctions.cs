using System;
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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;

namespace Platform.Api.Insight.Census;

public class CensusSchoolFunctions(
    ILogger<CensusSchoolFunctions> logger,
    ICensusService service,
    IValidator<CensusParameters> censusParametersValidator,
    IValidator<QuerySchoolCensusParameters> querySchoolCensusParametersValidator)
{
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
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
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

                var result = await service.GetCustomAsync(urn, identifier, queryParams.Dimension);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
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

            var (years, rows) = await service.GetSchoolHistoryAsync(urn, queryParams.Dimension, token);
            return years == null
                ? req.CreateNotFoundResponse()
                : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
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

            var (years, rows) = await service.GetComparatorAveHistoryAsync(urn, queryParams.Dimension, token);
            return years == null
                ? req.CreateNotFoundResponse()
                : await req.CreateJsonResponseAsync(rows.MapToApiResponse(years.StartYear, years.EndYear));
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

                var result = await service.QueryAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, queryParams.Dimension);
                return await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get census");
                return req.CreateErrorResponse();
            }
        }
    }
}