using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[SchoolAuthorization]
[FeatureGate(FeatureFlags.CustomData)]
[Route("school/{urn}/custom-data")]
public class SchoolCustomDataChangeController(
    IEstablishmentApi establishmentApi,
    IFinanceService financeService,
    ILogger<SchoolCustomDataChangeController> logger)
    : Controller
{
    [HttpGet]
    [Route("financial-data")]
    public async Task<IActionResult> FinancialData(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolCustomData(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var finances = await financeService.GetFinances(urn);
                var income = await financeService.GetSchoolIncome(urn);
                var expenditure = await financeService.GetSchoolExpenditure(urn);
                var census = await financeService.GetSchoolCensus(urn);
                var floorArea = await financeService.GetSchoolFloorArea(urn);

                // todo: merge in previous custom data set
                var viewModel = new SchoolCustomDataChangeViewModel(school, finances, income, expenditure, census, floorArea);

                // todo: build view
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school custom data: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("financial-data")]
    public IActionResult FinancialData(string urn, [FromForm] SchoolCustomDataViewModel viewModel)
    {
        using (logger.BeginScope(new { urn, viewModel }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    // todo
                    return View();
                }
                
                return RedirectToAction(nameof(NonFinancialData));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred saving custom data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    [HttpGet]
    [Route("school-characteristics")]
    public IActionResult NonFinancialData(string urn)
    {
        return StatusCode(StatusCodes.Status204NoContent);
    }
}