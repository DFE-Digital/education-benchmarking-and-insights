using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers
{
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
}