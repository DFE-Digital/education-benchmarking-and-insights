using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[ApiController]
[Route("api")]
public class ProxyController : Controller
{
    private readonly ILogger<ProxyController> _logger;
    private readonly IInsightApi _insightApi;
    private readonly IEstablishmentApi _establishmentApi;

    public ProxyController(ILogger<ProxyController> logger, IInsightApi insightApi, IEstablishmentApi establishmentApi)
    {
        _logger = logger;
        _insightApi = insightApi;
        _establishmentApi = establishmentApi;
    }

    [HttpGet]
    [Produces("application/json")]
    [Route("school/{urn}/expenditure")]
    public async Task<IActionResult> GetSchoolExpenditure(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var urns = new[]
                {
                    "140558", "143633", "142769", "141155", "142424", 
                    "146726", "141197", "141634", "139696", "140327",
                    "147334", "147380", "143226", "142197", "140183"
                };

                var query = new ApiQuery();
                foreach (var value in urns)
                {
                    query.AddIfNotNull("urns", value);
                }
                var schools = await _insightApi.QuerySchoolsExpenditure(query).GetPagedResultOrThrow<SchoolExpenditure>();
                return new JsonResult(schools);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error getting school expenditure: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/suggest")]
    public async Task<IActionResult> EstablishmentSuggest([FromQuery]string search, [FromQuery]string type, CancellationToken cancellation)
    {
        using (_logger.BeginScope(new {search}))
        {
            try
            {
                switch (type.ToLower())
                {
                    case "school":
                        return await SchoolSuggestions(search,cancellation);
                    case "trust":
                        return await TrustSuggestions(search,cancellation);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
            catch (TaskCanceledException)
            {
                _logger.LogInformation("Suggestion request cancelled");
                return new EmptyResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error getting suggestion: {Search} - {Type}", search, type);
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> SchoolSuggestions(string search, CancellationToken cancellation)
    {
        var suggestions = await _establishmentApi.SuggestSchools(search, cancellation).GetResultOrThrow<SuggestOutput<School>>();
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
    
    private async Task<IActionResult> TrustSuggestions(string search, CancellationToken cancellation)
    {
        var suggestions = await _establishmentApi.SuggestTrusts(search, cancellation).GetResultOrThrow<SuggestOutput<Trust>>();
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
}