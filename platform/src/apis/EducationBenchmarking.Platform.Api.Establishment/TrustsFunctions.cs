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

[ApiExplorerSettings(GroupName = "Trusts")]
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ISearchService<Trust> _search;
    private readonly IValidator<PostSuggestRequest> _validator;
    private readonly ITrustDb _db;

    public TrustsFunctions(ILogger<TrustsFunctions> logger, ISearchService<Trust> search, IValidator<PostSuggestRequest> validator, ITrustDb db)
    {
        _logger = logger;
        _search = search;
        _validator = validator;
        _db = db;
    }


    [FunctionName(nameof(SingleTrustAsync))]
    [ProducesResponseType(typeof(Trust), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SingleTrustAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{identifier}")] HttpRequest req,
        string identifier)
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
                var school = await _db.Get(identifier);

                return school == null
                    ? new NotFoundResult()
                    : new JsonContentResult(school);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(TrustSchoolsAsync))]
    [ProducesResponseType(typeof(IEnumerable<School>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> TrustSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{identifier}/schools")] HttpRequest req,
        string identifier)
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
                var schools = await _db.Schools(identifier);

                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust schools");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(QueryTrustsAsync))]
    public IActionResult QueryTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trusts")] HttpRequest req)
    {
        return new OkResult();
    }

    [FunctionName(nameof(SearchTrustsAsync))]
    public IActionResult SearchTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/search")]
        [RequestBodyType(typeof(PostSearchRequest), "The search object")]  HttpRequest req)
    {
        return new OkResult();
    }

    [FunctionName(nameof(SuggestTrustsAsync))]
    [ProducesResponseType(typeof(SuggestOutput<Trust>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/suggest")]
        [RequestBodyType(typeof(PostSuggestRequest), "The suggest object")] HttpRequest req)
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

                var trusts = await _search.SuggestAsync(body);
                return new JsonContentResult(trusts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get trust suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}