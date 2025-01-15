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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Search.Requests;
using Platform.Search.Responses;

namespace Platform.Api.Establishment.Features.Schools;

public class SchoolsFunctions(ILogger<SchoolsFunctions> logger,
    ISchoolsService service,
    IValidator<SuggestRequest> validator)
{
    [Function(nameof(SingleSchoolAsync))]
    [OpenApiOperation(nameof(SingleSchoolAsync), Constants.Features.Schools)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(School))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SingleSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Get, Route = "school/{identifier}")] HttpRequestData req,
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

    //TODO: Create specific suggest request
    //TODO: Add query parameter to new request
    [Function(nameof(SuggestSchoolsAsync))]
    [OpenApiOperation(nameof(SuggestSchoolsAsync), "Schools")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs to exclude", Type = typeof(string[]), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody(ContentType.ApplicationJson, typeof(SuggestRequest), Description = "The suggest object")]
    [OpenApiResponseWithBody(HttpStatusCode.OK, ContentType.ApplicationJson, typeof(SuggestResponse<School>))]
    [OpenApiResponseWithBody(HttpStatusCode.BadRequest, ContentType.ApplicationJson, typeof(ValidationError[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, MethodType.Post, Route = "schools/suggest")] HttpRequestData req)
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