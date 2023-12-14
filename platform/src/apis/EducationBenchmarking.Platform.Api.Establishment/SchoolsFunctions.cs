using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolDb _db;

    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISchoolDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName(nameof(GetSchoolAsync))]
    [ProducesResponseType((int) HttpStatusCode.OK, Type = typeof(School))]
    [ProducesResponseType((int) HttpStatusCode.NotFound)]
    [ProducesResponseType((int) HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> GetSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{urn}")]
        HttpRequest req,
        string urn)
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
                var school = await _db.GetSchool(urn);

                return school == null
                    ? new NotFoundResult()
                    : new JsonContentResult(school);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create school comparator set");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsAsync))]
    public async Task<IActionResult> QuerySchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools")]
        HttpRequest req)
    {
        return new OkResult();
    }

    [FunctionName(nameof(SearchSchoolsAsync))]
    public async Task<IActionResult> SearchSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/search")]
        [RequestBodyType(typeof(PostSearchRequest), "The search object")]
        HttpRequest req)
    {
        return new OkResult();
    }

    [FunctionName(nameof(SuggestSchoolsAsync))]
    public async Task<IActionResult> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/suggest")]
        [RequestBodyType(typeof(PostSuggestRequest), "The suggest object")]
        HttpRequest req)
    {
        return new OkResult();
    }
}