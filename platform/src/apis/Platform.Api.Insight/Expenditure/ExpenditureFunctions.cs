﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
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
namespace Platform.Api.Insight.Expenditure;

public class ExpenditureFunctions(
    ILogger<ExpenditureFunctions> logger,
    IExpenditureService service,
    IValidator<ExpenditureParameters> expenditureParametersValidator,
    IValidator<QuerySchoolExpenditureParameters> querySchoolExpenditureParametersValidator,
    IValidator<QueryTrustExpenditureParameters> queryTrustExpenditureParametersValidator)
{
    [Function(nameof(ExpenditureAllCategories))]
    [OpenApiOperation(nameof(ExpenditureAllCategories), "Expenditure")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> ExpenditureAllCategories(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/categories")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(ExpenditureCategories.All);
        }
    }

    [Function(nameof(ExpenditureAllDimensions))]
    [OpenApiOperation(nameof(ExpenditureAllDimensions), "Expenditure")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(string[]))]
    public async Task<HttpResponseData> ExpenditureAllDimensions(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/dimensions")] HttpRequestData req)
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
            return await req.CreateJsonResponseAsync(ExpenditureDimensions.All);
        }
    }

    [Function(nameof(SchoolExpenditureAsync))]
    [OpenApiOperation(nameof(SchoolExpenditureAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolExpenditureResponse))]
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

                var result = await service.GetSchoolAsync(urn);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(ExpenditureResponseFactory.Create(result, queryParams));
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
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolExpenditureResponse))]
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

                var result = await service.GetCustomSchoolAsync(urn, identifier);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(ExpenditureResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get custom school expenditure");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(TrustExpenditureAsync))]
    [OpenApiOperation(nameof(TrustExpenditureAsync), "Expenditure")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustExpenditureResponse))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trust/{companyNumber}")] HttpRequestData req,
        string companyNumber)
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

                var result = await service.GetTrustAsync(companyNumber);
                return result == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(ExpenditureResponseFactory.Create(result, queryParams));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get expenditure");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SchoolExpenditureHistoryAsync))]
    [OpenApiOperation(nameof(SchoolExpenditureHistoryAsync), "Expenditure")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleExpenditureDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolExpenditureHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/school/{urn}/history")] HttpRequestData req,
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

                var result = await service.GetSchoolHistoryAsync(urn);
                return await req.CreateJsonResponseAsync(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school expenditure history");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(TrustExpenditureHistoryAsync))]
    [OpenApiOperation(nameof(TrustExpenditureHistoryAsync), "Expenditure")]
    [OpenApiParameter("companyNumber", Type = typeof(string), Required = true)]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Required = true, Example = typeof(ExampleExpenditureDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustExpenditureHistoryResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> TrustExpenditureHistoryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trust/{companyNumber}/history")] HttpRequestData req,
        string companyNumber)
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

                var result = await service.GetTrustHistoryAsync(companyNumber);
                return await req.CreateJsonResponseAsync(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get trust expenditure history");
                return req.CreateErrorResponse();
            }
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
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolExpenditureResponse[]))]
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

                var result = await service.QuerySchoolsAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase);
                return await req.CreateJsonResponseAsync(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query schools expenditure");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(QueryTrustsExpenditureAsync))]
    [OpenApiOperation(nameof(QueryTrustsExpenditureAsync), "Expenditure")]
    [OpenApiParameter("companyNumbers", In = ParameterLocation.Query, Description = "List of trust company numbers", Type = typeof(string[]), Required = true)]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Expenditure category", Type = typeof(string), Example = typeof(ExampleExpenditureCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Dimension for response values", Type = typeof(string), Example = typeof(ExampleExpenditureDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(TrustExpenditureResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryTrustsExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "expenditure/trusts")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<QueryTrustExpenditureParameters>();

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
                var validationResult = await queryTrustExpenditureParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QueryTrustsAsync(queryParams.CompanyNumbers);
                return await req.CreateJsonResponseAsync(result.Select(x => ExpenditureResponseFactory.Create(x, queryParams)));
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query trusts expenditure");
                return req.CreateErrorResponse();
            }
        }
    }
}