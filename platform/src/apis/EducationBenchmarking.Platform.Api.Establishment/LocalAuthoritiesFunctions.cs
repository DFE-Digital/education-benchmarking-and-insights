using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Api.Establishment.Models;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Local Authorities")]
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
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authority/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QueryLocalAuthoritiesAsync))]
    public async Task<IActionResult> QueryLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authorities")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchLocalAuthoritiesAsync))]
    public async Task<IActionResult> SearchLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/search")] 
        [RequestBodyType(typeof(PostSearchRequest), "The search object")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestLocalAuthoritiesAsync))]
    public async Task<IActionResult> SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/suggest")] 
        [RequestBodyType(typeof(PostSuggestRequest), "The suggest object")] HttpRequest req)
    {
        return new OkResult();
    }
}