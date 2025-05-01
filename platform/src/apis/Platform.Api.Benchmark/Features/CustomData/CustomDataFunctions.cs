using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Api.Benchmark.Features.CustomData.Models;
using Platform.Api.Benchmark.Features.CustomData.Requests;
using Platform.Api.Benchmark.Features.CustomData.Services;
using Platform.Api.Benchmark.Responses;
using Platform.Domain;
using Platform.Domain.Messages;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Json;

namespace Platform.Api.Benchmark.Features.CustomData;

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
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = Routes.CustomDataItem)] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var data = await service.CustomDataSchoolAsync(urn, identifier);
                return data == null
                    ? req.CreateNotFoundResponse()
                    : await req.CreateJsonResponseAsync(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user defined school comparator set");
                return req.CreateErrorResponse();
            }
        }
    }

    [Function(nameof(CreateSchoolCustomDataAsync))]
    [OpenApiOperation(nameof(CreateSchoolCustomDataAsync), "Custom Data")]
    [OpenApiParameter("urn", Type = typeof(string), Required = true)]
    [OpenApiSecurityHeader]
    [OpenApiRequestBody("application/json", typeof(CustomDataRequest), Description = "The user defined set of schools object")]
    [OpenApiResponseWithoutBody(HttpStatusCode.Accepted)]
    [OpenApiResponseWithoutBody(HttpStatusCode.BadRequest)]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<MultiResponse> CreateSchoolCustomDataAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = Routes.CustomData)] HttpRequestData req,
        string urn)
    {
        var correlationId = req.GetCorrelationId();
        var response = new MultiResponse();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn }
               }))
        {
            try
            {
                var body = await req.ReadAsJsonAsync<CustomDataRequest>();
                var identifier = Guid.NewGuid().ToString();
                var data = body.CreateData(identifier, urn);

                await service.UpsertCustomDataAsync(data);
                await service.InsertNewAndDeactivateExistingUserDataAsync(CustomDataUserData.School(identifier, body.UserId, urn));

                var year = await service.CurrentYearAsync();

                var message = new PipelineStartCustom
                {
                    RunId = data.Id,
                    RunType = Pipeline.RunType.Custom,
                    Type = Pipeline.JobType.CustomData,
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
                response.HttpResponse = req.CreateErrorResponse();
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
        [HttpTrigger(AuthorizationLevel.Admin, "delete", Route = Routes.CustomDataItem)] HttpRequestData req,
        string urn,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "URN", urn },
                   { "Identifier", identifier }
               }))
        {
            try
            {
                var data = await service.CustomDataSchoolAsync(urn, identifier);
                if (data == null)
                {
                    return req.CreateNotFoundResponse();
                }

                await service.DeleteSchoolAsync(data);
                return req.CreateResponse(HttpStatusCode.OK);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to delete school custom data");
                return req.CreateErrorResponse();
            }
        }
    }
}