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
using Platform.Domain;
using Platform.Functions;
using Platform.Functions.Extensions;
using Platform.Infrastructure.Search;

namespace Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Organisations")]
public class OrganisationsFunctions
{
    private readonly ILogger<OrganisationsFunctions> _logger;
    private readonly ISearchService<OrganisationResponseModel> _search;
    private readonly IValidator<PostSuggestRequestModel> _validator;

    public OrganisationsFunctions(ILogger<OrganisationsFunctions> logger, ISearchService<OrganisationResponseModel> search, IValidator<PostSuggestRequestModel> validator)
    {
        _logger = logger;
        _search = search;
        _validator = validator;
    }

    [FunctionName(nameof(SuggestOrganisationsAsync))]
    [ProducesResponseType(typeof(SuggestResponseModel<OrganisationResponseModel>), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public async Task<IActionResult> SuggestOrganisationsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "organisations/suggest")]
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

                var schools = await _search.SuggestAsync(body);
                return new JsonContentResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to get organisation suggestions");
                return new StatusCodeResult(StatusCodes.Status500InternalServerError);
            }
        }
    }
}