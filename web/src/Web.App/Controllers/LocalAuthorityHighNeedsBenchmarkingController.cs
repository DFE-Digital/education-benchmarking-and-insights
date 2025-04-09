using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.LocalAuthorities, FeatureFlags.HighNeeds)]
[Route("local-authority/{code}/high-needs/benchmarking")]
public class LocalAuthorityHighNeedsBenchmarkingController(
    ILogger<LocalAuthorityHighNeedsBenchmarkingController> logger,
    IEstablishmentApi establishmentApi,
    ILocalAuthorityComparatorSetService localAuthorityComparatorSetService)
    : Controller
{
    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.HighNeeds)]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var localAuthority = await establishmentApi
                    .GetLocalAuthority(code)
                    .GetResultOrDefault<LocalAuthority>();
                if (localAuthority == null)
                {
                    return NotFound();
                }

                var set = localAuthorityComparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return NotFound();
                }

                return View(new LocalAuthorityViewModel(localAuthority));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs benchmarking: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.HighNeeds)]
    [Route("comparators")]
    public async Task<IActionResult> Comparators(string code, [FromQuery] string? referrer = null)
    {
        using (logger.BeginScope(new
        {
            code,
            referrer
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var localAuthority = await LocalAuthorityStatisticalNeighbours(code);
                var viewModel = new LocalAuthorityHighNeedsBenchmarkingViewModel(
                    localAuthority,
                    localAuthorityComparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set,
                    referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs benchmarking: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("comparators")]
    public async Task<IActionResult> Comparators([FromRoute] string code, [FromForm] LocalAuthorityComparatorViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            code,
            viewModel
        }))
        {
            try
            {
                var comparators = new List<string>(viewModel.Selected);
                FormAction action = viewModel.Action ?? throw new ArgumentNullException(nameof(viewModel));

                switch (action.Action)
                {
                    case FormAction.Add when string.IsNullOrWhiteSpace(viewModel.LaInput):
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select a local authority");
                        break;
                    case FormAction.Add when comparators.Count >= 9:
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select up to 9 comparator local authorities");
                        break;
                    case FormAction.Add:
                        comparators.Add(viewModel.LaInput);
                        break;
                    case FormAction.Remove when !string.IsNullOrWhiteSpace(action.Identifier):
                        comparators.Remove(action.Identifier);
                        break;
                    case FormAction.Continue when comparators.Count is < 1 or > 9:
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select between 1 and 9 comparator local authorities");
                        break;
                }

                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted local authorities comparators failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                }
                else if (action.Action == FormAction.Continue)
                {
                    localAuthorityComparatorSetService.SetUserDefinedComparatorSetInSession(code, new UserDefinedLocalAuthorityComparatorSet { Set = comparators.ToArray() });
                    return RedirectToAction("Index", new { code });
                }

                var localAuthority = await LocalAuthorityStatisticalNeighbours(code);
                return View(nameof(Comparators), new LocalAuthorityHighNeedsBenchmarkingViewModel(localAuthority, comparators.ToArray(), viewModel.Referrer));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred managing local authority comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    private async Task<LocalAuthorityStatisticalNeighbours> LocalAuthorityStatisticalNeighbours(string code)
    {
        return await establishmentApi
            .GetLocalAuthorityStatisticalNeighbours(code)
            .GetResultOrThrow<LocalAuthorityStatisticalNeighbours>();
    }
}