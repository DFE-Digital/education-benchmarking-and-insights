using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Shared;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Trusts")]
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ITrustDb _db;
    private readonly IValidator<TrustComparatorSetRequest> _validator;
    
    public TrustsFunctions(ILogger<TrustsFunctions> logger, ITrustDb trustDb,
        IValidator<TrustComparatorSetRequest> validator)
    {
        _logger = logger;
        _db = trustDb;
        _validator = validator;
    }

    [FunctionName(nameof(GetTrustCharacteristics))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(Characteristic[]))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public IActionResult GetTrustCharacteristics(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trusts/characteristics")]  HttpRequest req)
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
                return new JsonContentResult(Characteristics.Trusts.All);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust characteristics");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
    [FunctionName(nameof(CreateTrustComparatorSet))]
    [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(ComparatorSet<Trust>))]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> CreateTrustComparatorSet(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/comparator-set")]
        [RequestBodyType(typeof(TrustComparatorSetRequest), "The trust comparator set object")]
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
                var body = req.ReadAsJson<TrustComparatorSetRequest>();
                
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
                _logger.LogError(e, "Failed to create trust comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}