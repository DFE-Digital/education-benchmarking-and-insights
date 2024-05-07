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

[ApiExplorerSettings(GroupName = "Local Authorities")]
public class LocalAuthoritiesFunctions
{
    private readonly ILogger<LocalAuthoritiesFunctions> _logger;
    private readonly ISearchService<LocalAuthorityResponseModel> _search;
    private readonly IValidator<PostSuggestRequestModel> _validator;
    private readonly ILocalAuthorityDb _db;

    public LocalAuthoritiesFunctions(ILogger<LocalAuthoritiesFunctions> logger,
        ISearchService<LocalAuthorityResponseModel> search, IValidator<PostSuggestRequestModel> validator, ILocalAuthorityDb db)
    {
        _logger = logger;
        _search = search;
        _validator = validator;
        _db = db;
    }


    [FunctionName(nameof(SingleLocalAuthorityAsync))]
    public async Task<IActionResult> SingleLocalAuthorityAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authority/{identifier}")]
        HttpRequest req,
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
                var response = await _db.Get(identifier);

                return response == null
                    ? new NotFoundResult()
                    : new JsonContentResult(response);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get local authority");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SuggestLocalAuthoritiesAsync))]
    [ProducesResponseType(typeof(SuggestResponseModel<LocalAuthorityResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/suggest")]
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

                var trusts = await _search.SuggestAsync(body);
                return new JsonContentResult(trusts);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get local authority suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}