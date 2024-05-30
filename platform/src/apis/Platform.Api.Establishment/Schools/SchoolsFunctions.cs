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
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment.Schools;

[ApiExplorerSettings(GroupName = "Schools")]
public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISchoolService _schoolService;
    private readonly IValidator<PostSuggestRequest> _validator;
    private readonly ISchoolComparatorsService _comparatorService;

    public SchoolsFunctions(
        ILogger<SchoolsFunctions> logger,
        ISchoolService schoolService,
        ISchoolComparatorsService comparatorService,
        IValidator<PostSuggestRequest> validator)
    {
        _logger = logger;
        _schoolService = schoolService;
        _comparatorService = comparatorService;
        _validator = validator;
    }

    [FunctionName(nameof(SingleSchoolAsync))]
    [ProducesResponseType(typeof(School), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SingleSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "school/{identifier}")]
        HttpRequest req,
        string identifier)
    {
        var correlationId = req.GetCorrelationId();

        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName },
                   { "CorrelationID", correlationId },
                   { "Identifier", identifier}
               }))
        {
            try
            {
                var school = await _schoolService.GetAsync(identifier);

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
    [ProducesResponseType(typeof(IEnumerable<School>), (int)HttpStatusCode.OK)]
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
                var schools = await _schoolService.QueryAsync(companyNumber, laCode, phase);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to query schools");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SuggestSchoolsAsync))]
    [ProducesResponseType(typeof(SuggestResponse<School>), (int)HttpStatusCode.OK)]
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

                var schools = await _schoolService.SuggestAsync(body);
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
    [ProducesResponseType(typeof(SchoolComparator[]), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SchoolComparatorsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "schools/comparators")]
        [RequestBodyType(typeof(PostSchoolComparatorsRequest), "The comparator characteristics object")]
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
                var body = req.ReadAsJson<PostSchoolComparatorsRequest>();
                //TODO : Add request validation
                var comparators = await _comparatorService.ComparatorsAsync(body);
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