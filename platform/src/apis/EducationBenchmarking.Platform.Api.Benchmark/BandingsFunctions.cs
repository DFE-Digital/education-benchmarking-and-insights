using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Benchmark.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Benchmark;

[ApiExplorerSettings(GroupName = "Bandings")]
public class BandingsFunctions
{
    private readonly ILogger<BandingsFunctions> _logger;
    private readonly IBandingDb _db;

    public BandingsFunctions(ILogger<BandingsFunctions> logger, IBandingDb db)
    {
        _logger = logger;
        _db = db;
    }
    
    [FunctionName(nameof(GetFreeSchoolMealBandings))]
    [ProducesResponseType(typeof(Banding[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("phase", "Overall school phase", DataType = typeof(string), Required = false)]
    [QueryStringParameter("hasSixthForm", "Where or not a school has a sixth form", DataType = typeof(bool), Required = false)]
    [QueryStringParameter("fsm", "Target free school meals percentage", DataType = typeof(decimal), Required = false)]
    [QueryStringParameter("term", "Term", DataType = typeof(string), Required = false)]
    public async Task<IActionResult> GetFreeSchoolMealBandings(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "free-school-meal/bandings")]
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
                var bandings = await _db.GetFreeSchoolMealBandings();
                return new JsonContentResult(bandings);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get free school meal bandings");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
    
    [FunctionName(nameof(GetSchoolSizeBandings))]
    [ProducesResponseType(typeof(Banding[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("phase", "Overall school phase", DataType = typeof(string), Required = false)]
    [QueryStringParameter("hasSixthForm", "Where or not a school has a sixth form", DataType = typeof(bool), Required = false)]
    [QueryStringParameter("noOfPupils", "Target number of pupils", DataType = typeof(int), Required = false)]
    [QueryStringParameter("term", "Term", DataType = typeof(string), Required = false)]
    public async Task<IActionResult> GetSchoolSizeBandings(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school-size/bandings")]
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
                decimal? noOfPupils = decimal.TryParse(req.Query["noOfPupils"].ToString(), out var noOfPupilsVal) ? noOfPupilsVal : null;
                bool? hasSixthForm = bool.TryParse(req.Query["hasSixthForm"].ToString(), out var hasSixthFormVal) ? hasSixthFormVal : null;
                
                var phase = req.Query["phase"].ToString();
                var term = req.Query["term"].ToString();
                
                var bandings = await _db.GetSchoolSizeBandings(phase, term, noOfPupils, hasSixthForm);
                return new JsonContentResult(bandings);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get size bandings");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}