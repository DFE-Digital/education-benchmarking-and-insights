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
[Route("school/{urn}/comparators")]
public class SchoolComparatorsController(ILogger<SchoolComparatorsController> logger, IComparatorSetService comparatorSetService, IEstablishmentApi establishmentApi, IFinanceService financeService) : Controller
{
    [HttpGet]
    [Route("building")]
    public async Task<IActionResult> Building(string urn, string referrer)
    {
        using (logger.BeginScope(new { urn, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadComparatorSet(urn);
                var finances = await financeService.GetFinances(set.DefaultArea);
                var viewModel = new SchoolComparatorsViewModel(school, referrer, finances);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school building comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("pupil")]
    public async Task<IActionResult> Pupil(string urn, string referrer)
    {
        using (logger.BeginScope(new { urn, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadComparatorSet(urn);
                var finances = await financeService.GetFinances(set.DefaultPupil);
                var viewModel = new SchoolComparatorsViewModel(school, referrer, finances);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school pupil comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("custom")]
    public async Task<IActionResult> Custom(string urn, string referrer)
    {
        using (logger.BeginScope(new { urn, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school, referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }


    [HttpGet]
    [Route("custom/create")]
    public async Task<IActionResult> Create(string urn, string referrer)
    {
        using (logger.BeginScope(new { urn, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school, referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    /*[HttpPost]
    [Route("Create")]
    public async Task<IActionResult> Create(string urn)
    {
        using (logger.BeginScope(new { urn, }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = RefererBackInfo(referrer, urn);
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school, referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }*/

    private BacklinkInfo RefererBackInfo(string referrer, string urn)
    {
        return referrer switch
        {
            Referrers.SchoolSpending => new BacklinkInfo(Url.Action("Index", "SchoolSpending", new { urn }), $"Back to {PageTitles.Spending.ToLower()}"),
            Referrers.SchoolComparison => new BacklinkInfo(Url.Action("Index", "SchoolComparison", new { urn }), $"Back to {PageTitles.Comparison.ToLower()}"),
            Referrers.SchoolWorkforce => new BacklinkInfo(Url.Action("Index", "SchoolWorkforce", new { urn }), $"Back to {PageTitles.Workforce.ToLower()}"),
            _ => throw new ArgumentOutOfRangeException(nameof(referrer))
        };
    }
}