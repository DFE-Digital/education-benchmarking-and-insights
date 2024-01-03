using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Api.Benchmark.Models.Characteristics;
using EducationBenchmarking.Platform.Api.Benchmark.Requests;
using EducationBenchmarking.Platform.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Comparator Set")]
public class ComparatorSetFunctions
{
    private readonly ILogger<ComparatorSetFunctions> _logger;
    private readonly IComparatorSetDb _db;
    private readonly IValidator<ComparatorSetRequest> _validator;

    public ComparatorSetFunctions(IComparatorSetDb db, ILogger<ComparatorSetFunctions> logger,
        IValidator<ComparatorSetRequest> validator)
    {
        _db = db;
        _logger = logger;
        _validator = validator;
    }
    
    
    [FunctionName(nameof(GetCharacteristics))]
    [ProducesResponseType(typeof(Characteristic[]), (int)HttpStatusCode.OK)]
    public IActionResult GetCharacteristics(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "comparator-set/characteristics")]
        HttpRequest req)
    {
        return new JsonContentResult(Characteristics.All);
    }

    [FunctionName(nameof(PostComparatorSet))]
    [ProducesResponseType(typeof(ComparatorSet), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> PostComparatorSet(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "comparator-set")]
        [RequestBodyType(typeof(ComparatorSetRequest), "The comparator set object")]
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
                var body = req.ReadAsJson<ComparatorSetRequest>();

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
                _logger.LogError(e, "Failed to create comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}