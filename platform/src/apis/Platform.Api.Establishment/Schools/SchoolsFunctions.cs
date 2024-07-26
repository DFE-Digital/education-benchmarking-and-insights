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
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Infrastructure.Search;
namespace Platform.Api.Establishment.Schools;

public class SchoolsFunctions(ILogger<SchoolsFunctions> logger,
    ISchoolsService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleSchoolAsync))]
    [OpenApiOperation(nameof(SingleSchoolAsync), "Schools")]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(School))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{identifier}")] HttpRequestData req,
        string identifier)
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
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var school = await service.GetAsync(identifier);

                return school == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(school);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(QuerySchoolsAsync))]
    [OpenApiOperation(nameof(QuerySchoolsAsync), "Schools")]
    [OpenApiParameter("companyNumber", In = ParameterLocation.Query, Description = "Company number", Type = typeof(string), Required = false)]
    [OpenApiParameter("laCode", In = ParameterLocation.Query, Description = "Local authority code", Type = typeof(int), Required = false)]
    [OpenApiParameter("phase", In = ParameterLocation.Query, Description = "Phase", Type = typeof(string), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<School>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object?>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       // ReSharper disable once ConditionalAccessQualifierIsNonNullableAccordingToAPIContract
                       "Query", req.Url?.Query
                   }
               }))
        {
            try
            {
                var companyNumber = req.Query["companyNumber"];
                var laCode = req.Query["laCode"];
                var phase = req.Query["phase"];
                var schools = await service.QueryAsync(companyNumber, laCode, phase);
                return await req.CreateJsonResponseAsync(schools);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to query schools");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(SuggestSchoolsAsync))]
    [OpenApiOperation(nameof(SuggestSchoolsAsync), "Schools")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs to exclude", Type = typeof(string[]), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(SuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SuggestResponse<School>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/suggest")] HttpRequestData req)
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
                var body = await req.ReadAsJsonAsync<SuggestRequest>();

                var validationResult = await validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return await req.CreateValidationErrorsResponseAsync(validationResult.Errors);
                }

                var urns = req.Query["urns"]?.Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var schools = await service.SuggestAsync(body, urns);
                return await req.CreateJsonResponseAsync(schools);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school suggestions");
                return req.CreateErrorResponse();
            }
        }
    }
}