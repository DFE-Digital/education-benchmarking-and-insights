using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

public class TrustsFunctions
{
    private readonly ILogger<TrustsFunctions> _logger;
    private readonly ISearchService<Trust> _search;
    
    public TrustsFunctions(ILogger<TrustsFunctions> logger, ISearchService<Trust> search)
    {
        _logger = logger;
        _search = search;
    }
    
    
    [FunctionName(nameof(GetTrustAsync))]
    public async Task<IActionResult> GetTrustAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trust/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QueryTrustsAsync))]
    public async Task<IActionResult> QueryTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "trusts")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchTrustsAsync))]
    public async Task<IActionResult> SearchTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/search")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestTrustsAsync))]
    public async Task<IActionResult> SuggestTrustsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "trusts/suggest")] HttpRequest req)
    {
        return new OkResult();
    }
}