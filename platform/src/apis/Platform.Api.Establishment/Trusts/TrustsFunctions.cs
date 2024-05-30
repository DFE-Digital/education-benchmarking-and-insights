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

namespace Platform.Api.Establishment.Trusts;

[ApiExplorerSettings(GroupName = "Trusts")]
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ITrustService _service;
    private readonly IValidator<PostSuggestRequest> _validator;

    public TrustsFunctions(ILogger<TrustsFunctions> logger, ITrustService service, IValidator<PostSuggestRequest> validator)
    {
        _logger = logger;
        _service = service;
        _validator = validator;
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
                _logger.LogError(e, "Failed to get trust");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }

    [FunctionName(nameof(SuggestTrustsAsync))]
    [ProducesResponseType(typeof(SuggestResponse<Trust>), (int)HttpStatusCode.OK)]
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

                var trusts = await _service.SuggestAsync(body);
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