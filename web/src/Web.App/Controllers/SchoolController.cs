using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}")]
public class SchoolController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    IComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolHome(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(school);

                var set = await comparatorSetService.ReadComparatorSet(urn);

                var pupilExpenditure = await financeService.GetExpenditure(set.DefaultPupil);
                var areaExpenditure = await financeService.GetExpenditure(set.DefaultArea);

                var viewModel = new SchoolViewModel(school, finances, pupilExpenditure, areaExpenditure);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}