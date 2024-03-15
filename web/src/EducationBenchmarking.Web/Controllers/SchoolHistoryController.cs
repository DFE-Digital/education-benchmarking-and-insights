using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;


namespace EducationBenchmarking.Web.Controllers
{
    [Route("school/{urn}/history")]
    public class SchoolHistoryController : Controller
    {
        private readonly IEstablishmentApi establishmentApi;
        private readonly ILogger<SchoolHistoryController> logger;

        public SchoolHistoryController(IEstablishmentApi establishmentApi, ILogger<SchoolHistoryController> logger)
        {
            this.establishmentApi = establishmentApi;
            this.logger = logger;
        }

        [HttpGet]
        public async Task<IActionResult> Index(string urn)
        {
            using (logger.BeginScope(new { urn }))
            {
                try
                {
                    ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "School", new { urn }));

                    var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                    var viewModel = new SchoolHistoryViewModel(school, null); // Adjust according to actual data fetching
                    return View(viewModel);
                }
                catch (Exception e)
                {
                    logger.LogError(e, "An error displaying school history: {DisplayUrl}", Request.GetDisplayUrl());
                    return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
                }
            }
        }
    }
}

