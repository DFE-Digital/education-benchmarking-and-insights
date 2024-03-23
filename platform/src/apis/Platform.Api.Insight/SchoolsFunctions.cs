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

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolsDb _db;

    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolsDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(QuerySchoolExpenditureAsync))]
    [ProducesResponseType(typeof(SchoolExpenditureResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QuerySchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/expenditure")] HttpRequest req)
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

                var result = await _db.Expenditure(urns);

                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed school expenditure query");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolWorkforceAsync))]
    [ProducesResponseType(typeof(SchoolWorkforceResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> QuerySchoolWorkforceAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/workforce")] HttpRequest req)
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

                var result = await _db.Workforce(urns);

                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed school workforce query");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}