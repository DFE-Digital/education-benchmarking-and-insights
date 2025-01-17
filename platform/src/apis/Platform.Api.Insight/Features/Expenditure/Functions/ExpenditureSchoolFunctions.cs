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
using Platform.Functions.OpenApi.Examples;

namespace Platform.Api.Insight.Expenditure;

public class ExpenditureSchoolFunctions(
    ILogger<ExpenditureSchoolFunctions> logger,
    IExpenditureService service,
    IValidator<ExpenditureParameters> expenditureParametersValidator,
    IValidator<QuerySchoolExpenditureParameters> querySchoolExpenditureParametersValidator)
{
    [Function(nameof(SchoolExpenditureAsync))]
    [OpenApiOperation(nameof(SchoolExpenditureAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ExpenditureSchoolResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}")] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

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
                var validationResult = await expenditureParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.GetSchoolAsync(urn, queryParams.Dimension);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school expenditure");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(CustomSchoolExpenditureAsync))]
    [OpenApiOperation(nameof(CustomSchoolExpenditureAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ExpenditureSchoolResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> CustomSchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/custom/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

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
                var validationResult = await expenditureParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.GetCustomSchoolAsync(urn, identifier, queryParams.Dimension);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get custom school expenditure");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SchoolExpenditureHistoryAsync))]
    [OpenApiOperation(nameof(SchoolExpenditureHistoryAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleExpenditureDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/history")] HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

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
            var validationResult = await expenditureParametersValidator.ValidateAsync(queryParams, token);
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

    [Function(nameof(SchoolExpenditureHistoryAvgComparatorSetAsync))]
    [OpenApiOperation(nameof(SchoolExpenditureHistoryAvgComparatorSetAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleExpenditureDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ExpenditureHistoryResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolExpenditureHistoryAvgComparatorSetAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/history/comparator-set-average")] HttpRequestData req,
        string urn,
        CancellationToken token)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<ExpenditureParameters>();

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
            var validationResult = await expenditureParametersValidator.ValidateAsync(queryParams, token);
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

    [Function(nameof(QuerySchoolsExpenditureAsync))]
    [OpenApiOperation(nameof(QuerySchoolsExpenditureAsync), "Expenditure")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(ExpenditureSchoolResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/schools")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<QuerySchoolExpenditureParameters>();

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
                var validationResult = await querySchoolExpenditureParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QuerySchoolsAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase, queryParams.Dimension);
                return await req.CreateJsonResponseAsync(result.MapToApiResponse(queryParams.Category));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query schools expenditure");
                return req.CreateErrorResponse();
            }
        }
    }
}