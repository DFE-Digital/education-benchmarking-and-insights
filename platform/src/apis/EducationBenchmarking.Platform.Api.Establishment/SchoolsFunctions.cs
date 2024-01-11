using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Establishment.Db;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentValidation;
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
    private readonly ISearchService<School> _search;
    private readonly IValidator<PostSuggestRequest> _validator;

    public SchoolsFunctions(
        ILogger<SchoolsFunctions> logger, 
        ISchoolDb db, 
        ISearchService<School> search,
        IValidator<PostSuggestRequest> validator)
    {
        _logger = logger;
        _db = db;
        _search = search;
        _validator = validator;
    }

    [FunctionName(nameof(GetSchoolAsync))]
    [ProducesResponseType(typeof(School), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
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
                var school = await _db.Get(urn);

                return school == null
                    ? new NotFoundResult()
                    : new JsonContentResult(school);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QuerySchoolsAsync))]
    [ProducesResponseType(typeof(PagedResults<School>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("page", "Page number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("pageSize", "Size of page ", DataType = typeof(int), Required = false)]
    public async Task<IActionResult> QuerySchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "schools")]
        HttpRequest req)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object?>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "Query", req.QueryString.HasValue ? req.QueryString.Value : "" }
               }))
        {
            try
            {
                var schools = await _db.Query(req.Query);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query schools");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SearchSchoolsAsync))]
    [ProducesResponseType(typeof(SearchOutput<School>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SearchSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/search")]
        [RequestBodyType(typeof(PostSearchRequest), "The search object")]
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
                var body = req.ReadAsJson<PostSearchRequest>();
                var schools = await _search.SearchAsync(body);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to search schools");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SuggestSchoolsAsync))]
    [ProducesResponseType(typeof(SuggestOutput<School>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/suggest")]
        [RequestBodyType(typeof(PostSuggestRequest), "The suggest object")]
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
                var body = req.ReadAsJson<PostSuggestRequest>();
        
                var validationResult = await _validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return new ValidationErrorsResult(validationResult.Errors);
                }
        
                var schools = await _search.SuggestAsync(body, req.HttpContext.RequestAborted);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}