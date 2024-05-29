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
using Platform.Api.Establishment.Search;
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolDb _db;
    private readonly ISearchService<SchoolResponseModel> _schoolSearch;
    private readonly IValidator<PostSuggestRequestModel> _validator;
    private readonly ISchoolComparatorsService _comparatorSearch;

    public SchoolsFunctions(
        ILogger<SchoolsFunctions> logger,
        ISchoolDb db,
        ISearchService<SchoolResponseModel> schoolSearch,
        ISchoolComparatorsService comparatorSearch,
        IValidator<PostSuggestRequestModel> validator)
    {
        _logger = logger;
        _db = db;
        _schoolSearch = schoolSearch;
        _comparatorSearch = comparatorSearch;
        _validator = validator;
    }

    [FunctionName(nameof(SingleSchoolAsync))]
    [ProducesResponseType(typeof(SchoolResponseModel), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SingleSchoolAsync(
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
    [ProducesResponseType(typeof(IEnumerable<SchoolResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    [QueryStringParameter("companyNumber", "Company number", DataType = typeof(int), Required = false)]
    [QueryStringParameter("laCode", "Local authority code", DataType = typeof(int), Required = false)]
    [QueryStringParameter("phase", "Phase", DataType = typeof(string), Required = false)]
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
                var companyNumber = req.Query["companyNumber"].ToString();
                var laCode = req.Query["laCode"].ToString();
                var phase = req.Query["phase"].ToString();
                var schools = await _db.Query(companyNumber, laCode, phase);
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
    [ProducesResponseType(typeof(SearchResponseModel<SchoolResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SearchSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/search")]
        [RequestBodyType(typeof(PostSearchRequestModel), "The search object")]
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
                var body = req.ReadAsJson<PostSearchRequestModel>();
                var schools = await _schoolSearch.SearchAsync(body);
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
    [ProducesResponseType(typeof(SuggestResponseModel<SchoolResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/suggest")]
        [RequestBodyType(typeof(PostSuggestRequestModel), "The suggest object")]
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
                var body = req.ReadAsJson<PostSuggestRequestModel>();

                var validationResult = await _validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return new ValidationErrorsResult(validationResult.Errors);
                }

                var schools = await _schoolSearch.SuggestAsync(body);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get school suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SchoolComparatorsAsync))]
    [ProducesResponseType(typeof(SchoolComparatorResponseModel[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/comparators")]
        [RequestBodyType(typeof(PostSchoolComparatorsRequestModel), "The comparator characteristics object")]
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
                var body = req.ReadAsJson<PostSchoolComparatorsRequestModel>();
                //TODO : Add request validation
                var comparators = await _comparatorSearch.ComparatorsAsync(body);
                return new JsonContentResult(comparators);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to create school comparators");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}