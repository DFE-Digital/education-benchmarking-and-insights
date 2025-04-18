﻿using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Services;
namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/suggest")]
public class SuggestProxyController(
    ILogger<SuggestProxyController> logger,
    ISuggestService suggestService) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    // https://github.com/dotnet/aspnetcore/issues/55412
    [ProducesResponseType<IEnumerable<SuggestValue<LocalAuthority>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<SuggestValue<TrustSummary>>>(StatusCodes.Status200OK)]
    [ProducesResponseType<IEnumerable<SuggestValue<SchoolSummary>>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Suggest(
        [FromQuery] string search,
        [FromQuery] string type,
        [FromQuery] string[]? exclude = null,
        [FromQuery] bool? excludeMissingFinancialData = null)
    {
        using (logger.BeginScope(new
        {
            search
        }))
        {
            try
            {
                switch (type.ToLower())
                {
                    case OrganisationTypes.School:
                        var schools = await suggestService.SchoolSuggestions(search, exclude, excludeMissingFinancialData == true);
                        return new JsonResult(schools);
                    case OrganisationTypes.Trust:
                        var trusts = await suggestService.TrustSuggestions(search, exclude);
                        return new JsonResult(trusts);
                    case OrganisationTypes.LocalAuthority:
                        var localAuthorities = await suggestService.LocalAuthoritySuggestions(search, exclude);
                        return new JsonResult(localAuthorities);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting suggestion: {Search} - {Type}", search, type);
                return StatusCode(500);
            }
        }
    }
}