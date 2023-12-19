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

    public ProxyController(ILogger<ProxyController> logger, IInsightApi insightApi)
    {
        _logger = logger;
        _insightApi = insightApi;
    }

    [HttpGet]
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
                return await Task.FromResult(new JsonResult(schools));
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error getting school expenditure: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }
}