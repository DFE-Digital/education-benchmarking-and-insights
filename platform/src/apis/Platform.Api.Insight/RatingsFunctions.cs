using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Api.Insight.Db;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;

namespace Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Ratings")]
public class RatingsFunctions
{
    private readonly ILogger<RatingsFunctions> _logger;
    private readonly IRatingsDb _db;

    public RatingsFunctions(IRatingsDb db, ILogger<RatingsFunctions> logger)
    {
        _db = db;
        _logger = logger;
    }

    [FunctionName(nameof(QueryRatingsAsync))]
    [ProducesResponseType(typeof(RatingResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QueryRatingsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "ratings")] HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   {"Application", Constants.ApplicationName},
                   {"CorrelationID", correlationId}
               }))
        {
            try
            {
                var urns = req.Query["urns"].ToString().Split(",");

                var result = await _db.Get(urns);

                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed ratings query");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}