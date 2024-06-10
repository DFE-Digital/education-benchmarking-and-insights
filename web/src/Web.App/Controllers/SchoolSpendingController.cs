using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/spending-and-costs")]
public class SchoolSpendingController(
    ILogger<SchoolController> logger,
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    ISchoolComparatorSetService schoolComparatorSetService,
    IMetricRagRatingApi metricRagRatingApi,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolSpending(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userData = await userDataService.GetSchoolDataAsync(User.UserId(), urn);

                var ratings = await metricRagRatingApi.GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn)).GetResultOrThrow<RagRating[]>();
                var set = await schoolComparatorSetService.ReadComparatorSet(urn);

                var pupilExpenditure = await financeService.GetExpenditure(set.Pupil);
                var areaExpenditure = await financeService.GetExpenditure(set.Building);

                var viewModel = new SchoolSpendingViewModel(school, ratings, pupilExpenditure, areaExpenditure, userData.ComparatorSet);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school spending and costs: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}