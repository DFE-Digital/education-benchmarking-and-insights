using System;
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
namespace Platform.Api.Insight.Income;

public class IncomeFunctions(
    ILogger<IncomeFunctions> logger,
    IIncomeService service,
    IValidator<IncomeParameters> incomeParametersValidator,
    IValidator<QuerySchoolIncomeParameters> querySchoolIncomeParametersValidator,
    IValidator<QueryTrustIncomeParameters> queryTrustIncomeParametersValidator)
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
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
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
                var validationResult = await incomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

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
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
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
                var validationResult = await incomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }
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
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
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
                var validationResult = await incomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

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
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
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
                var validationResult = await incomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

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
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "School overall phase", Type = typeof(string), Example = typeof(ExampleOverallPhase))]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Eight digit trust company number", Type = typeof(string))]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority three digit code", Type = typeof(string))]
    [OpenApiParameter("category", In = ParameterLocation.Query, Description = "Income category", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeCategory))]
    [OpenApiParameter("dimension", In = ParameterLocation.Query, Description = "Value dimension", Type = typeof(string), Required = true, Example = typeof(ExampleIncomeDimension))]
    [OpenApiParameter("excludeCentralServices", In = ParameterLocation.Query, Description = "Exclude central services amounts", Type = typeof(bool), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolIncomeResponse[]))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/schools")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<QuerySchoolIncomeParameters>();

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
                var validationResult = await querySchoolIncomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QuerySchoolsAsync(queryParams.Urns, queryParams.CompanyNumber, queryParams.LaCode, queryParams.Phase);
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
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, "application/json", typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryTrustsIncomeAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "income/trusts")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<QueryTrustIncomeParameters>();

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
                var validationResult = await queryTrustIncomeParametersValidator.ValidateAsync(queryParams);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var result = await service.QueryTrustsAsync(queryParams.CompanyNumbers);
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