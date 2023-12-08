using EducationBenchmarking.Web.Domain;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers.Api;

[ApiController]
[Route("api/school-expenditure")]
public class SchoolExpenditureController : Controller
{
    private readonly ILogger<SchoolExpenditureController> _logger;

    public SchoolExpenditureController(ILogger<SchoolExpenditureController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    [Route("{urn}")]
    public async Task<IActionResult> Get(string urn)
    {
        var schools = new SchoolExpenditure[]
        {
            new() { Urn = "140558", SchoolName = "St Joseph's Catholic Primary School, Moorthorpe", Kind ="Academy sponsor led", LocalAuthority = "" },
            new() { Urn = "135558", SchoolName = "Hawkswood Primary Pru", Kind ="Pupil referral unit", LocalAuthority = "" },
            new() { Urn = "105376", SchoolName = "Cloughside College", Kind ="Community special school", LocalAuthority = "" },
            new() { Urn = "112858", SchoolName = "Stoney Middleton Cofe (C) Primary School", Kind ="Voluntary controlled school", LocalAuthority = "" },
            new() { Urn = "122233", SchoolName = "Kielder Primary School And Nursery", Kind ="Community school", LocalAuthority = "" },
            new() { Urn = "118155", SchoolName = "Chillerton And Rookley Primary School", Kind ="Community school", LocalAuthority = "" },
            new() { Urn = "112267", SchoolName = "Asby Endowed School", Kind ="Voluntary controlled school", LocalAuthority = "" }
        };
        
        return await Task.FromResult(new JsonResult(schools));
    }
}