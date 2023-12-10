using System.Threading.Tasks;
using EducationBenchmarking.Platform.Domain;
using EducationBenchmarking.Platform.Infrastructure.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

public class LocalAuthoritiesFunctions
{
    private readonly ILogger<LocalAuthoritiesFunctions> _logger;
    private readonly ISearchService<LocalAuthority> _search;

    public LocalAuthoritiesFunctions(ILogger<LocalAuthoritiesFunctions> logger, ISearchService<LocalAuthority> search)
    {
        _logger = logger;
        _search = search;
    }
    
    [FunctionName(nameof(GetLocalAuthorityAsync))]
    public async Task<IActionResult> GetLocalAuthorityAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "local-authority/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QueryLocalAuthoritiesAsync))]
    public async Task<IActionResult> QueryLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "local-authorities")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchLocalAuthoritiesAsync))]
    public async Task<IActionResult> SearchLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "local-authorities/search")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestLocalAuthoritiesAsync))]
    public async Task<IActionResult> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "local-authorities/suggest")] HttpRequest req)
    {
        return new OkResult();
    }
    
}