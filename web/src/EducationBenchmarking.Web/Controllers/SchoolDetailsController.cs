using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/details")]
public class SchoolDetailsController(ILogger<SchoolDetailsController> logger, IEstablishmentApi establishmentApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        try
        {
            ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "School", new { urn }));

            var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
            return View(new SchoolDetailsViewModel(school));
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
            return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
        }
    }
}