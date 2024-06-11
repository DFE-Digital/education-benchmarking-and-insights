using System;
using System.Collections.Generic;
using System.Linq;
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

namespace Platform.Api.Establishment.LocalAuthorities;

[ApiExplorerSettings(GroupName = "Local Authorities")]
public class LocalAuthoritiesFunctions
{
    private readonly ILogger<LocalAuthoritiesFunctions> _logger;
    private readonly ILocalAuthoritiesService _service;
    private readonly IValidator<SuggestRequest> _validator;

    public LocalAuthoritiesFunctions(ILogger<LocalAuthoritiesFunctions> logger,
        ILocalAuthoritiesService service, IValidator<SuggestRequest> validator)
    {
        _logger = logger;
        _service = service;
        _validator = validator;
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
                   { "CorrelationID", correlationId },
                   { "Identifier", identifier}
               }))
        {
            try
            {
                var response = await _service.GetAsync(identifier);

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
    [ProducesResponseType(typeof(SuggestResponse<LocalAuthority>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/suggest")]
        [RequestBodyType(typeof(SuggestRequest), "The suggest object")]
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
                var body = req.ReadAsJson<SuggestRequest>();

                var validationResult = await _validator.ValidateAsync(body);
                if (!validationResult.IsValid)
                {
                    return new ValidationErrorsResult(validationResult.Errors);
                }

                var names = req.Query["names"].ToString().Split(",").Where(x => !string.IsNullOrEmpty(x)).ToArray();
                var localAuthorities = await _service.SuggestAsync(body, names);
                return new JsonContentResult(localAuthorities);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get local authority suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}