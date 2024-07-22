using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Insight.Schools;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolsService service)
{
    [Function(nameof(SchoolsCharacteristicsAsync))]
    [OpenApiOperation(nameof(SchoolsCharacteristicsAsync), "Schools")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolCharacteristic))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolsCharacteristicsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}/characteristics")] HttpRequestData req,
        string urn)
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
                var result = await service.CharacteristicAsync(urn);
                return result == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get school characteristics");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(QuerySchoolsCharacteristicsAsync))]
    [OpenApiOperation(nameof(QuerySchoolsCharacteristicsAsync), "Schools")]
    [OpenApiParameter("urns", In = ParameterLocation.Query, Description = "List of school URNs", Type = typeof(string[]), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(SchoolCharacteristic[]))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QuerySchoolsCharacteristicsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/characteristics")] HttpRequestData req)
    {
        var correlationId = req.GetCorrelationId();
        var queryParams = req.GetParameters<SchoolsParameters>();

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
                var schools = await service.QueryCharacteristicAsync(queryParams.Schools);
                return await req.CreateJsonResponseAsync(schools);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get schools characteristics");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}