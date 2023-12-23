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
    private readonly IBenchmarkApi _benchmarkApi;

    public ProxyController(ILogger<ProxyController> logger, IInsightApi insightApi, IEstablishmentApi establishmentApi, IBenchmarkApi benchmarkApi)
    {
        _logger = logger;
        _insightApi = insightApi;
        _establishmentApi = establishmentApi;
        _benchmarkApi = benchmarkApi;
    }
    
    [HttpGet]
    [Produces("application/json")]
    [Route("school/{urn}/expenditure")]
    public async Task<IActionResult> SchoolExpenditure(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var comparatorSet = await _benchmarkApi.CreateComparatorSet().GetResultOrThrow<ComparatorSet<School>>();
                var query = new ApiQuery().Page(1, comparatorSet.TotalResults);
                foreach (var school in comparatorSet.Results)
                {
                    query.AddIfNotNull("urns", school.Urn);
                }
                
                var result = await _insightApi.GetSchoolsExpenditure(query).GetPagedResultOrThrow<SchoolExpenditure>();
                return new JsonResult(result);
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