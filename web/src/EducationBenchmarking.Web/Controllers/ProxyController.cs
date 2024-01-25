using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[ApiController]
[Route("api")]
public class ProxyController(
    ILogger<ProxyController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService)
    : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [Route("school/{urn}/expenditure")]
    public async Task<IActionResult> SchoolExpenditure(string urn)
    {
        using (logger.BeginScope(new {urn}))
        {
            try
            {
                var result = await financeService.GetExpenditure(urn);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting school expenditure: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("school/{urn}/workforce")]
    public async Task<IActionResult> SchoolWorkforce(string urn)
    {
        using (logger.BeginScope(new {urn}))
        {
            try
            {
                var result = await financeService.GetWorkforce(urn);
                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting school workforce: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/suggest")]
    public async Task<IActionResult> EstablishmentSuggest([FromQuery]string search, [FromQuery]string type, CancellationToken cancellation)
    {
        using (logger.BeginScope(new {search}))
        {
            try
            {
                switch (type.ToLower())
                {
                    case Constants.SchoolOrganisationType:
                        return await SchoolSuggestions(search,cancellation);
                    case Constants.TrustOrganisationType:
                        return await TrustSuggestions(search,cancellation);
                    default:
                        return await OrganisationSuggestions(search,cancellation);
                }
            }
            catch (TaskCanceledException)
            {
                logger.LogInformation("Suggestion request cancelled");
                return new EmptyResult();
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting suggestion: {Search} - {Type}", search, type);
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> SchoolSuggestions(string search, CancellationToken cancellation)
    {
        var suggestions = await establishmentApi.SuggestSchools(search, cancellation).GetResultOrThrow<SuggestOutput<School>>();
        var results = suggestions.Results.Select(value =>
        {
            var text = value.Text.Replace("*", "");
                    
            var additionalDetails = new List<string>();
                    
            if(!string.IsNullOrWhiteSpace(value.Document.Town)) additionalDetails.Add(text == value.Document.Town ? value.Text : value.Document.Town );
            if(!string.IsNullOrWhiteSpace(value.Document.Postcode)) additionalDetails.Add(text == value.Document.Postcode ? value.Text : value.Document.Postcode);

            if (text != value.Document.Name)
            {
                value.Text = value.Document.Name ?? "{Missing school name}";
            }
                    
            var additionalText = additionalDetails.Count > 0
                ? $" ({string.Join(", ", additionalDetails.Select(a => a))})"
                : "";
                    
            value.Text = $"{value.Text}{additionalText}";
                    
            return value;
        });
        
        return new JsonResult(results);
    }
    
    private async Task<IActionResult> TrustSuggestions(string search, CancellationToken cancellation)
    {
        var suggestions = await establishmentApi.SuggestTrusts(search, cancellation).GetResultOrThrow<SuggestOutput<Trust>>();
        var results = suggestions.Results.Select(value =>
        {
            var text = value.Text.Replace("*", "");
            
            if (text != value.Document.Name)
            {
                value.Text = value.Document.Name;
            }
            
            return value;
        });
        
        return new JsonResult(results);
    }
    
    private async Task<IActionResult> OrganisationSuggestions(string search, CancellationToken cancellation)
    {
        var suggestions = await establishmentApi.SuggestOrganisations(search, cancellation).GetResultOrThrow<SuggestOutput<Organisation>>();
        var results = suggestions.Results.Select(value =>
        {
            var text = value.Text.Replace("*", "");
            
            var additionalDetails = new List<string>();
                    
            if(!string.IsNullOrWhiteSpace(value.Document.Town)) additionalDetails.Add(text == value.Document.Town ? value.Text : value.Document.Town );
            if(!string.IsNullOrWhiteSpace(value.Document.Postcode)) additionalDetails.Add(text == value.Document.Postcode ? value.Text : value.Document.Postcode);
            
            if (text != value.Document.Name)
            {
                value.Text = value.Document.Name;
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