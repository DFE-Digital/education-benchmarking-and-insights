using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[SchoolAuthorization]
[FeatureGate(FeatureFlags.CustomData)]
[Route("school/{urn}/custom-data")]
public class SchoolCustomDataChangeController(
    IEstablishmentApi establishmentApi,
    ICustomDataService customDataService,
    IUserDataService userDataService,
    ILogger<SchoolCustomDataChangeController> logger)
    : Controller
{
    [HttpGet]
    [Route("financial-data")]
    [ImportModelState]
    public async Task<IActionResult> FinancialData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] =
                    new BacklinkInfo(Url.Action("Index", "SchoolCustomData", new
                    {
                        urn
                    }));
                var viewModel = await BuildViewModel(urn);
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
    [ExportModelState]
    public IActionResult FinancialData(string urn, [FromForm] FinancialDataCustomDataViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            urn,
            viewModel
        }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted FinancialData failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                    return RedirectToAction(nameof(FinancialData));
                }

                customDataService.MergeCustomDataIntoSession(urn, viewModel);
                return RedirectToAction(nameof(NonFinancialData), new
                {
                    urn
                });
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
    [ImportModelState]
    public async Task<IActionResult> NonFinancialData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(FinancialData), new
                {
                    urn
                }));
                var viewModel = await BuildViewModel(urn);
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
    [Route("school-characteristics")]
    [ExportModelState]
    public IActionResult NonFinancialData(string urn, [FromForm] NonFinancialDataCustomDataViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            urn,
            viewModel
        }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted NonFinancialData failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                    return RedirectToAction(nameof(NonFinancialData));
                }

                customDataService.MergeCustomDataIntoSession(urn, viewModel);
                return RedirectToAction(nameof(WorkforceData), new
                {
                    urn
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred saving custom data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    [HttpGet]
    [Route("workforce")]
    [ImportModelState]
    public async Task<IActionResult> WorkforceData(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(NonFinancialData), new
                {
                    urn
                }));
                var viewModel = await BuildViewModel(urn);
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
    [Route("workforce")]
    [ExportModelState]
    public async Task<IActionResult> WorkforceData(string urn, [FromForm] WorkforceDataCustomDataViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            urn,
            viewModel
        }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted WorkforceData failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                    return RedirectToAction(nameof(WorkforceData));
                }

                customDataService.MergeCustomDataIntoSession(urn, viewModel);

                await customDataService.CreateCustomData(urn, User.UserId());

                customDataService.ClearCustomDataFromSession(urn);
                // todo: persist orchestrator job ID to auth user data

                return RedirectToAction("Index", "SchoolCustomData", new
                {
                    urn
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred saving custom data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    private async Task<SchoolCustomDataChangeViewModel> BuildViewModel(string urn)
    {
        var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        var currentValues = await customDataService.GetCurrentData(urn);
        var customInput = customDataService.GetCustomDataFromSession(urn);

        // attempt to load in custom data from previous submission if not already in the middle of a new submission
        if (customInput == null)
        {
            var (customData, _) = await userDataService.GetSchoolDataAsync(User.UserId(), urn);
            if (!string.IsNullOrWhiteSpace(customData))
            {
                customInput = await customDataService.GetCustomDataById(urn, customData);
            }

            // set session to match view model to sync continue/back CTAs in UI
            if (customInput != null)
            {
                customDataService.SetCustomDataInSession(urn, customInput);
            }
        }

        return new SchoolCustomDataChangeViewModel(school, currentValues, customInput ?? new CustomData());
    }
}