using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Responses;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Benchmark.CustomData;

public class CustomDataFunctions(ILogger<CustomDataFunctions> logger, ICustomDataService service)
{
    [Function(nameof(SchoolCustomDataAsync))]
    [OpenApiOperation(nameof(SchoolCustomDataAsync), "Custom Data")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(CustomDataSchool))]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> SchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "custom-data/school/{urn}/{identifier}")] HttpRequestData req,
        string urn,
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
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var data = await service.CustomDataSchoolAsync(urn, identifier);
                return data == null
                    ? req.CreateResponse(HttpStatusCode.NotFound)
                    : await req.CreateJsonResponseAsync(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined school comparator set");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }

    [Function(nameof(CreateSchoolCustomDataAsync))]
    [OpenApiOperation(nameof(CreateSchoolCustomDataAsync), "Custom Data")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(CustomDataRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<MultiResponse> CreateSchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "custom-data/school/{urn}/{identifier}")] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();
        var response = new MultiResponse();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   {
                       "Application", Constants.ApplicationName
                   },
                   {
                       "CorrelationID", correlationId
                   },
                   {
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<CustomDataRequest>();
                var data = body.CreateData(identifier, urn);

                await service.UpsertCustomDataAsync(data);
                await service.UpsertUserDataAsync(CustomDataUserData.School(identifier, body.UserId, urn));

                var year = await service.CurrentYearAsync();

                var message = new PipelineStartMessage
                {
                    RunId = data.Id,
                    RunType = "custom",
                    Type = "custom-data",
                    URN = data.URN,
                    Year = int.Parse(year),
                    Payload = body.CreatePayload()
                };

                response.Messages = [message.ToJson()];
                response.HttpResponse = req.CreateResponse(HttpStatusCode.Accepted);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to upsert school custom data");
                response.HttpResponse = req.CreateResponse(HttpStatusCode.InternalServerError);
            }

            return response;
        }
    }

    [Function(nameof(RemoveSchoolCustomDataAsync))]
    [OpenApiOperation(nameof(RemoveSchoolCustomDataAsync), "Custom Data")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiParameter("identifier", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithoutBody(HttpStatusCode.OK)]
    [OpenApiResponseWithoutBody(HttpStatusCode.NotFound)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> RemoveSchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = "custom-data/school/{urn}/{identifier}")] HttpRequestData req,
        string urn,
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
                       "URN", urn
                   },
                   {
                       "Identifier", identifier
                   }
               }))
        {
            try
            {
                var data = await service.CustomDataSchoolAsync(urn, identifier);
                if (data == null)
                {
                    return req.CreateResponse(HttpStatusCode.NotFound);
                }

                await service.DeleteSchoolAsync(data);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete school custom data");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}