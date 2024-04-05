using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/find-ways-to-spend-less")]
public class SchoolResourcesController(
    IEstablishmentApi establishmentApi,
    IInsightApi insightApi,
    ILogger<SchoolResourcesController> logger) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {

        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "School", new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var ratings = await insightApi.GetRatings(new ApiQuery().AddIfNotNull("urns", urn)).GetResultOrThrow<RagRating[]>();
                var viewModel = new SchoolResourcesViewModel(school, ratings);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}