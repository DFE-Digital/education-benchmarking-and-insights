using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using Platform.Api.Establishment.Db;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Trusts")]
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ISearchService<TrustResponseModel> _search;
    private readonly IValidator<PostSuggestRequestModel> _validator;
    private readonly ITrustDb _db;

    public TrustsFunctions(ILogger<TrustsFunctions> logger, ISearchService<TrustResponseModel> search, IValidator<PostSuggestRequestModel> validator, ITrustDb db)
    {
        _logger = logger;
        _search = search;
        _validator = validator;
        _db = db;
    }


    [FunctionName(nameof(SingleTrustAsync))]
    [ProducesResponseType(typeof(TrustResponseModel), (int)HttpStatusCode.OK)]
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
    [ProducesResponseType(typeof(IEnumerable<SchoolResponseModel>), (int)HttpStatusCode.OK)]
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
        [RequestBodyType(typeof(PostSearchRequestModel), "The search object")]  HttpRequest req)
    {
        return new OkResult();
    }

    [FunctionName(nameof(SuggestTrustsAsync))]
    [ProducesResponseType(typeof(SuggestResponseModel<TrustResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/suggest")]
        [RequestBodyType(typeof(PostSuggestRequestModel), "The suggest object")] HttpRequest req)
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
                var body = req.ReadAsJson<PostSuggestRequestModel>();

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