using System.Threading.Tasks;
using AzureFunctions.Extensions.Swashbuckle.Attribute;
using EducationBenchmarking.Platform.Infrastructure.Search;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EducationBenchmarking.Platform.Api.Establishment;

[ApiExplorerSettings(GroupName = "Local Authorities")]
public class LocalAuthoritiesFunctions
{
    
    [FunctionName(nameof(SingleLocalAuthorityAsync))]
    public IActionResult SingleLocalAuthorityAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authority/{identifier}")] HttpRequest req,
        string identifier)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(QueryLocalAuthoritiesAsync))]
    public IActionResult QueryLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "local-authorities")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SearchLocalAuthoritiesAsync))]
    public IActionResult SearchLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/search")] 
        [RequestBodyType(typeof(PostSearchRequest), "The search object")] HttpRequest req)
    {
        return new OkResult();
    }
    
    [FunctionName(nameof(SuggestLocalAuthoritiesAsync))]
    public IActionResult SuggestLocalAuthoritiesAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "post", Route = "local-authorities/suggest")] 
        [RequestBodyType(typeof(PostSuggestRequest), "The suggest object")] HttpRequest req)
    {
        return new OkResult();
    }
}