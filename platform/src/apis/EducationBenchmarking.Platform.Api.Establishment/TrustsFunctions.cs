using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

/// <summary>
/// 
/// </summary>
public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ISearchService<Trust> _search;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="logger"></param>
    /// <param name="search"></param>
    public TrustsFunctions(ILogger<TrustsFunctions> logger, ISearchService<Trust> search)
    {
        _logger = logger;
        _search = search;
    }
    
    
    [FunctionName(nameof(GetTrustAsync))]
    public async Task<IActionResult> GetTrustAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "trust/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QueryTrustsAsync))]
    public async Task<IActionResult> QueryTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "trusts")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchTrustsAsync))]
    public async Task<IActionResult> SearchTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "trusts/search")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestTrustsAsync))]
    public async Task<IActionResult> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "trusts/suggest")] HttpRequest req)
    {
        return new OkResult();
    }
}