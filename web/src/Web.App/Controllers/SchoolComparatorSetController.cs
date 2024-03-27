using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparator-set")]
public class SchoolComparatorSetController(ILogger<SchoolComparatorSetController> logger, IComparatorSetService comparatorSetService, IEstablishmentApi establishmentApi) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn, string referrer)
    {
        using (logger.BeginScope(new { urn, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadComparatorSet(urn);
                var viewModel = new SchoolComparatorSetViewModel(school, set);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparator set: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private BacklinkInfo RefererBackInfo(string referrer, string urn)
    {
        return referrer switch
        {
            Referrers.SchoolComparison => new BacklinkInfo(Url.Action("Index", "SchoolComparison", new { urn })),
            Referrers.SchoolWorkforce => new BacklinkInfo(Url.Action("Index", "SchoolWorkforce", new { urn })),
            _ => throw new ArgumentOutOfRangeException(nameof(referrer))
        };
    }
}