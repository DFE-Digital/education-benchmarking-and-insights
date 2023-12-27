using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Insight.Db;
using EducationBenchmarking.Platform.Api.Insight.Models;
using EducationBenchmarking.Platform.Shared;
using EducationBenchmarking.Platform.Shared.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Insight;

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
    [ProducesResponseType(typeof(PagedSchoolExpenditure), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = false)]
    [QueryStringParameter("page", "Page number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("pageSize", "Size of page ", DataType = typeof(int), Required = false)]
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
                var (page, pageSize) = QueryParameters.GetPagingValues(req.Query);
                var urns = req.Query["urns"].ToString().Split(",");
                
                var result = await _db.GetExpenditure(urns, page, pageSize);
                
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
    [ProducesResponseType(typeof(PagedSchoolWorkforce), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = false)]
    [QueryStringParameter("page", "Page number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("pageSize", "Size of page ", DataType = typeof(int), Required = false)]
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
                var (page, pageSize) = QueryParameters.GetPagingValues(req.Query);
                var urns = req.Query["urns"].ToString().Split(",");
                
                var result = await _db.GetWorkforce(urns, page, pageSize);
                
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