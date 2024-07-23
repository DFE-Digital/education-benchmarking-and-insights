using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Azure.WebJobs.Extensions.OpenApi.Core.Attributes;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;
using Platform.Functions.OpenApi;
using Platform.Infrastructure.Sql;
namespace Platform.Api.Insight;

public class CommonFunctions(IDatabaseFactory dbFactory, ILogger<CommonFunctions> logger)
{
    [Function(nameof(SingleCurrentReturnYearsAsync))]
    [OpenApiOperation(nameof(SingleCurrentReturnYearsAsync), "Common")]
    [OpenApiSecurityHeader]
    [OpenApiResponseWithBody(HttpStatusCode.OK, "application/json", typeof(object))]
    public async Task<HttpResponseData> SingleCurrentReturnYearsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "current-return-years")] HttpRequestData req)
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
                using var conn = await dbFactory.GetConnection();
                const string sql = "SELECT Value FROM Parameters WHERE Name = @Name";

                var aar = await conn.QueryFirstOrDefaultAsync<string>(sql, new
                {
                    Name = "LatestAARYear"
                });
                var cfr = await conn.QueryFirstOrDefaultAsync<string>(sql, new
                {
                    Name = "LatestCFRYear"
                });

                var result = new
                {
                    aar,
                    cfr
                };

                return await req.CreateJsonResponseAsync(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to get current return years");
                return req.CreateErrorResponse();
            }
        }
    }
}