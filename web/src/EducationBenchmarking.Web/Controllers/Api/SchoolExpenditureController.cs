using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers.Api;

[ApiController]
[Route("api/school-expenditure")]
public class SchoolExpenditureController : Controller
{
    private readonly ILogger<SchoolExpenditureController> _logger;
    private readonly ISchoolApi _schoolApi;

    public SchoolExpenditureController(ILogger<SchoolExpenditureController> logger, ISchoolApi schoolApi)
    {
        _logger = logger;
        _schoolApi = schoolApi;
    }

    [HttpGet]
    [Route("{urn}")]
    public async Task<IActionResult> Get(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var schools = await _schoolApi.QueryExpenditure();
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