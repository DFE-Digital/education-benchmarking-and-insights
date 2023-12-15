using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.School.Db;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.School;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolExpenditureDb _db;
    
    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolExpenditureDb db)
    {
        _logger = logger;
        _db = db;
    }
    
    [FunctionName(nameof(QuerySchoolExpenditureAsync))]
    [ProducesResponseType(typeof(PagedResults<SchoolExpenditure>), (int)HttpStatusCode.OK)]
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
                   {"CorrelationID", correlationId},
                   {"Query",  req.QueryString.HasValue ? req.QueryString.Value : "" }
               }))
        {
            try
            {
                var schools = await _db.Query(req.Query);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed school expenditure query");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}