using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;


[ApiController]
[Route("api/suggest")]
public class SuggestProxyController(
ILogger<ProxyController> logger,
    IEstablishmentApi establishmentApi) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Suggest([FromQuery] string search, [FromQuery] string type)
    {
        using (logger.BeginScope(new { search }))
        {
            try
            {
                return type.ToLower() switch
                {
                    OrganisationTypes.School => await SchoolSuggestions(search),
                    OrganisationTypes.Trust => await TrustSuggestions(search),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting suggestion: {Search} - {Type}", search, type);
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> SchoolSuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestSchools(search).GetResultOrThrow<SuggestOutput<School>>();
        var results = suggestions.Results.Select(value =>
        {
            var text = value.Text?.Replace("*", "");

            var additionalDetails = new List<string?>();

            if (!string.IsNullOrWhiteSpace(value.Document?.Town))
                additionalDetails.Add(text == value.Document.Town ? value.Text : value.Document.Town);
            if (!string.IsNullOrWhiteSpace(value.Document?.Postcode))
                additionalDetails.Add(text == value.Document.Postcode ? value.Text : value.Document.Postcode);

            if (text != value.Document?.Name)
            {
                value.Text = value.Document?.Name;
            }

            var additionalText = additionalDetails.Count > 0
                ? $" ({string.Join(", ", additionalDetails.Select(a => a))})"
                : "";

            value.Text = $"{value.Text}{additionalText}";

            return value;
        });

        return new JsonResult(results);
    }

    private async Task<IActionResult> TrustSuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestTrusts(search).GetResultOrThrow<SuggestOutput<Trust>>();
        var results = suggestions.Results.Select(value =>
        {
            var text = value.Text?.Replace("*", "");

            if (text != value.Document?.Name)
            {
                value.Text = value.Document?.Name;
            }

            return value;
        });

        return new JsonResult(results);
    }
}