using System.Threading.Tasks;
using EducationBenchmarking.Platform.Infrastructure.Search;
using EducationBenchmarking.Platform.Shared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;

namespace EducationBenchmarking.Platform.Api.Establishment;

public class SchoolsFunctions
{
    private readonly ILogger<SchoolsFunctions> _logger;
    private readonly ISearchService<School> _search;

    public SchoolsFunctions(ILogger<SchoolsFunctions> logger, ISearchService<School> search)
    {
        _logger = logger;
        _search = search;
    }
    
    [FunctionName(nameof(GetSchoolAsync))]
    public async Task<IActionResult> GetSchoolAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "school/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QuerySchoolsAsync))]
    public async Task<IActionResult> QuerySchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "schools")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchSchoolsAsync))]
    public async Task<IActionResult> SearchSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "schools/search")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestSchoolsAsync))]
    public async Task<IActionResult> SuggestSchoolsAsync(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "schools/suggest")] HttpRequest req)
    {
        return new OkResult();
    }
}