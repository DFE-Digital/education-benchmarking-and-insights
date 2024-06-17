using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using Dapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Common")]
public class CommonFunctions
{
    private readonly IDatabaseFactory _dbFactory;
    private readonly ILogger<CommonFunctions> _logger;

    public CommonFunctions(IDatabaseFactory dbFactory, ILogger<CommonFunctions> logger)
    {
        _dbFactory = dbFactory;
        _logger = logger;
    }

    [FunctionName(nameof(SingleCurrentReturnYearsAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public async Task<IActionResult> SingleCurrentReturnYearsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "current-return-years")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();
        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId }
               }))
        {
            try
            {
                using var conn = await _dbFactory.GetConnection();
                const string sql = "SELECT Value FROM Parameters WHERE Name = @Name";

                var aar = await conn.QueryFirstOrDefaultAsync<string>(sql, new { Name = "LatestAARYear" });
                var cfr = await conn.QueryFirstOrDefaultAsync<string>(sql, new { Name = "LatestCFRYear" });

                return new JsonContentResult(new { aar, cfr });
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get current return years");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}