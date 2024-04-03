using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;

namespace Web.App.Controllers;

[ApiController]
[Route("api")]
public class ProxyController(
    ILogger<ProxyController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/expenditure")]
    public async Task<IActionResult> EstablishmentExpenditure([FromQuery] string type, [FromQuery] string id)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                switch (type.ToLower())
                {
                    case OrganisationTypes.School:
                        return await SchoolExpenditure(id);
                    case OrganisationTypes.Trust:
                        return await TrustExpenditure(id);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/workforce")]
    public async Task<IActionResult> EstablishmentWorkforce([FromQuery] string type, [FromQuery] string id)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                switch (type.ToLower())
                {
                    case OrganisationTypes.School:
                        return await SchoolWorkforce(id);
                    case OrganisationTypes.Trust:
                        return await TrustWorkforce(id);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting workforce data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/workforce/history")]
    public async Task<IActionResult> EstablishmentWorkforceHistory([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension = "")
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await financeService.GetSchoolWorkforceHistory(id, dimension),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting workforce history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/balance/history")]
    public async Task<IActionResult> EstablishmentBalanceHistory([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await financeService.GetSchoolBalanceHistory(id, dimension),
                    OrganisationTypes.Trust => await financeService.GetTrustBalanceHistory(id, dimension),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting balance history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/income/history")]
    public async Task<IActionResult> EstablishmentIncomeHistory([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await financeService.GetSchoolIncomeHistory(id, dimension),
                    OrganisationTypes.Trust => await financeService.GetTrustIncomeHistory(id, dimension),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting income history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/suggest")]
    public async Task<IActionResult> EstablishmentSuggest([FromQuery] string search, [FromQuery] string type)
    {
        using (logger.BeginScope(new { search }))
        {
            try
            {
                return type.ToLower() switch
                {
                    OrganisationTypes.School => await SchoolSuggestions(search),
                    OrganisationTypes.Trust => await TrustSuggestions(search),
                    _ => await OrganisationSuggestions(search)
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting suggestion: {Search} - {Type}", search, type);
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> TrustExpenditure(string id)
    {
        var schools = await establishmentApi.GetTrustSchools(id).GetResultOrThrow<IEnumerable<School>>();
        var result = await financeService.GetExpenditure(schools.Select(x => x.Urn).OfType<string>());
        return new JsonResult(result);
    }

    private async Task<IActionResult> SchoolExpenditure(string id)
    {
        var set = await comparatorSetService.ReadComparatorSet(id);
        var result = await financeService.GetExpenditure(set.DefaultPupil);
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustWorkforce(string id)
    {
        var schools = await establishmentApi.GetTrustSchools(id).GetResultOrThrow<IEnumerable<School>>();
        var result = await financeService.GetWorkforce(schools.Select(x => x.Urn).OfType<string>());
        return new JsonResult(result);
    }

    private async Task<IActionResult> SchoolWorkforce(string id)
    {
        var set = await comparatorSetService.ReadComparatorSet(id);
        var result = await financeService.GetWorkforce(set.DefaultPupil);
        return new JsonResult(result);
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

    private async Task<IActionResult> OrganisationSuggestions(string search)
    {
        var suggestions = await establishmentApi.SuggestOrganisations(search)
            .GetResultOrThrow<SuggestOutput<Organisation>>();
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
}