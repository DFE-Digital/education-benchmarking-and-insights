using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/financial-planning/{year:int}")]
public class SchoolPlanningYearController : Controller
{
    private readonly ILogger<SchoolPlanningYearController> _logger;

    public SchoolPlanningYearController(ILogger<SchoolPlanningYearController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string urn, int year)
    {
        using (_logger.BeginScope(new { urn, year }))
        {
            try
            {
                return new AcceptedResult();
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}