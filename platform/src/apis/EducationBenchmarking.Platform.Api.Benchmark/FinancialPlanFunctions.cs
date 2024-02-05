using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Domain;
using EducationBenchmarking.Platform.Domain.Requests;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Financial Plan")]
public class FinancialPlanFunctions
{
    private readonly ILogger<FinancialPlanFunctions> _logger;
    private readonly IFinancialPlanDb _db;

    public FinancialPlanFunctions(ILogger<FinancialPlanFunctions> logger, IFinancialPlanDb db)
    {
        _logger = logger;
        _db = db;
    }


    [FunctionName(nameof(GetFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlan), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "financial-plan/{urn}/{year}")]
        HttpRequest req,
        string urn,
        int year)
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
                var plan = await _db.GetFinancialPlan(urn, year);
                return plan != null 
                    ? new JsonContentResult(plan)
                    : new NotFoundResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(PutFinancialPlanAsync))]
    [ProducesResponseType(typeof(FinancialPlan),(int)HttpStatusCode.Created)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.Conflict)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> PutFinancialPlanAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "put", Route = "financial-plan/{urn}/{year}")]
        [RequestBodyType(typeof(FinancialPlanRequest), "The financial plan object")]
        HttpRequest req,
        string urn,
        int year)
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
                var body = req.ReadAsJson<FinancialPlanRequest>();

                //TODO : Add request validator
                var result = await _db.UpsertFinancialPlan(urn, year, body);

                return result.Status switch
                {
                    DbResult.ResultStatus.Created => new CreatedResult($"financial-plan/{urn}/{year}", result.Content),
                    DbResult.ResultStatus.Updated => new NoContentResult(),
                    _ => throw new ArgumentOutOfRangeException(nameof(result.Status))
                };
            }
            catch (DataConflictException ex)
            {
                _logger.LogWarning(ex, "Upsert financial plan conflict");
                return new ConflictObjectResult(ex.Details);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to upsert financial plan");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}