using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Insight.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
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
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    [QueryStringParameter("page", "Page number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("pageSize", "Size of page", DataType = typeof(int), Required = false)]
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
                var (page, pageSize) = req.Query.GetPagingValues();
                var urns = req.Query["urns"].ToString().Split(",");
                
                var result = await _db.Expenditure(urns, page, pageSize);
                
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
    [QueryStringParameter("urns", "List of school URNs", DataType = typeof(string), Required = true)]
    [QueryStringParameter("page", "Page number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("pageSize", "Size of page", DataType = typeof(int), Required = false)]
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
                var (page, pageSize) = req.Query.GetPagingValues();
                var urns = req.Query["urns"].ToString().Split(",");
                
                var result = await _db.Workforce(urns, page, pageSize);
                
                return new JsonContentResult(result);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed school workforce query");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
    [FunctionName(nameof(GetSchoolRatings))]
    [ProducesResponseType(typeof(Rating[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("phase", "Overall school phase", DataType = typeof(string), Required = true)]
    [QueryStringParameter("term", "Term", DataType = typeof(string), Required = true)]
    [QueryStringParameter("size", "School size band", DataType = typeof(string), Required = true)]
    [QueryStringParameter("fsm", "Free school meals band", DataType = typeof(string), Required = true)]
    public async Task<IActionResult> GetSchoolRatings(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/ratings")]
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
                var phase = req.Query["phase"].ToString();
                var term = req.Query["term"].ToString();
                var size = req.Query["size"].ToString();
                var fsm = req.Query["fsm"].ToString();
                
                var bandings = await _db.SchoolRatings(phase, term, size, fsm);
                return new JsonContentResult(bandings);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school ratings");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}