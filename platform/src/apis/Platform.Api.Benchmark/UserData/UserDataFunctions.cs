using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
namespace Platform.Api.Benchmark.UserData;

public class UserDataFunctions(IUserDataService service, ILogger<UserDataFunctions> logger)
{
    [Function(nameof(QueryAsync))]
    [OpenApiOperation(nameof(QueryAsync), "User Data")]
    [OpenApiParameter("userId", In = ParameterLocation.Query, Description = "User Id", Type = typeof(string), Required = true)]
    [OpenApiParameter("type", In = ParameterLocation.Query, Description = "Type", Type = typeof(string), Required = false)]
    [OpenApiParameter("organisationType", In = ParameterLocation.Query, Description = "Organisation Type", Type = typeof(string), Required = false)]
    [OpenApiParameter("organisationId", In = ParameterLocation.Query, Description = "Organisation Id", Type = typeof(string), Required = false)]
    [OpenApiParameter("status", In = ParameterLocation.Query, Description = "Status", Type = typeof(string), Required = false)]
    [OpenApiParameter("id", In = ParameterLocation.Query, Description = "Identifier", Type = typeof(string), Required = false)]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(IEnumerable<UserData>))]
    [OpenApiResponseWithoutBody(HttpStatusCode.InternalServerError)]
    public async Task<HttpResponseData> QueryAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "user-data")] HttpRequestData req)
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
                var userId = req.Query["userId"] ?? string.Empty;
                var type = req.Query["type"];
                var status = req.Query["status"];
                var id = req.Query["id"];
                var organisationType = req.Query["organisationType"];
                var organisationId = req.Query["organisationId"];
                var data = await service.QueryAsync(userId, type, status, id, organisationId, organisationType);

                return await req.CreateJsonResponseAsync(data);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get user data");
                return req.CreateResponse(HttpStatusCode.InternalServerError);
            }
        }
    }
}