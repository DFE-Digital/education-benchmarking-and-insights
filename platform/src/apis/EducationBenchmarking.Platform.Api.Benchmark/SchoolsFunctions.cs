using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Shared;
using EducationBenchmarking.Platform.Shared.Characteristics;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolDb _db;
    private readonly IValidator<SchoolComparatorSetRequest> _validator;

    public SchoolsFunctions(ISchoolDb db, ILogger<SchoolsFunctions> logger, IValidator<SchoolComparatorSetRequest> validator)
    {
        _db = db;
        _logger = logger;
        _validator = validator;
    }

    [FunctionName(nameof(GetSchoolCharacteristics))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public IActionResult GetSchoolCharacteristics(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools/characteristics")]  HttpRequest req)
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
                return new JsonContentResult(Questions.Schools.All);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
    [FunctionName(nameof(CreateSchoolComparatorSet))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateSchoolComparatorSet(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/comparator-set")]
        [RequestBodyType(typeof(SchoolComparatorSetRequest), "The school comparator set object")]
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
                var body = req.ReadAsJson<SchoolComparatorSetRequest>();
                
                var validationResult = await _validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return new ValidationErrorsResult(validationResult.Errors);
                }

                var set = await _db.CreateSet(body);
                
                return new JsonContentResult(set);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}