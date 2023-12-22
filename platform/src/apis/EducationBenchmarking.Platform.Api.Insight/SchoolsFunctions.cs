using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Insight.Db;
using EducationBenchmarking.Platform.Api.Insight.Models;
using EducationBenchmarking.Platform.Api.Insight.Requests;
using EducationBenchmarking.Platform.Shared;
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
    private readonly ISchoolExpenditureDb _db;
    
    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolExpenditureDb db)
    {
        _logger = logger;
        _db = db;
    }
    
    [FunctionName(nameof(GetSchoolExpenditureAsync))]
    [ProducesResponseType(typeof(SchoolExpenditure), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetSchoolExpenditureAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/expenditure")] 
        [RequestBodyType(typeof(SchoolExpenditureRequest), "The school expenditure request object")]
        HttpRequest req)
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
                var body = req.ReadAsJson<SchoolExpenditureRequest>();
                
                //TODO : Add request validator
                // var validationResult = await _validator.ValidateAsync(body);
                // if (!validationResult.IsValid)
                // {
                //     return new ValidationErrorsResult(validationResult.Errors);
                // }
                
                var schools = await _db.GetExpenditure(body);
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