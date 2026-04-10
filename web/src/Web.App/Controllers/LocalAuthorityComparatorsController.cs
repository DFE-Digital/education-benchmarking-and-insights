using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/comparators")]
[ValidateLaCode]
public class LocalAuthorityComparatorsController(
    ILogger<LocalAuthorityComparatorsController> logger,
    IEstablishmentApi establishmentApi,
    ILocalAuthorityComparatorSetService localAuthorityComparatorSetService)
    : Controller
{
    [HttpGet]

    public async Task<IActionResult> Index(string code, [FromQuery] LocalAuthorityBenchmarkType type, [FromQuery] string? referrer = null)
    {
        using (logger.BeginScope(new { code, type, referrer }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var localAuthority = await LocalAuthorityStatisticalNeighbours(code);
                var viewModel = new LocalAuthorityComparatorsViewModel(
                    localAuthority,
                    GetComparators(localAuthority.StatisticalNeighbours, code),
                    type,
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
    public async Task<IActionResult> Index([FromRoute] string code, [FromQuery] LocalAuthorityBenchmarkType type, [FromForm] LocalAuthorityComparatorSelectionViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            code,
            viewModel
        }))
        {
            try
            {
                var localAuthority = await LocalAuthorityStatisticalNeighbours(code);

                var comparators = new List<string>(viewModel.Selected);
                FormAction action = viewModel.Action ?? throw new ArgumentNullException(nameof(viewModel));

                switch (action.Action)
                {
                    case FormAction.Add when string.IsNullOrWhiteSpace(viewModel.LaInput):
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select a local authority");
                        break;
                    case FormAction.Add when comparators.Count >= 19:
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select up to 19 comparator local authorities");
                        break;
                    case FormAction.Add:
                        comparators.Add(viewModel.LaInput);
                        break;
                    case FormAction.Remove when !string.IsNullOrWhiteSpace(action.Identifier):
                        comparators.Remove(action.Identifier);
                        break;
                    case FormAction.Continue when comparators.Count is < 1 or > 19:
                        ModelState.AddModelError(nameof(viewModel.LaInput), "Select between 1 and 19 comparator local authorities");
                        break;
                    case FormAction.Reset:
                        comparators = InitialComparatorSetFromNeighbours(localAuthority.StatisticalNeighbours);
                        break;
                    case FormAction.Clear:
                        comparators = [];
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

                    return ContinueActionResult(code, type);
                }

                return View(nameof(Index), new LocalAuthorityComparatorsViewModel(localAuthority, comparators.ToArray(), type, viewModel.Referrer));
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred managing local authority comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    private RedirectToActionResult ContinueActionResult(string code, LocalAuthorityBenchmarkType type)
    {
        switch (type)
        {
            case LocalAuthorityBenchmarkType.EducationHealthCarePlans:
                return RedirectToAction("Index", "LocalAuthorityEducationHealthCarePlans", new { code });
            case LocalAuthorityBenchmarkType.HighNeeds:
            default:
                return RedirectToAction("Index", "LocalAuthorityHighNeedsBenchmarking", new { code });
        }

    }

    private string[] GetComparators(IEnumerable<LocalAuthorityStatisticalNeighbour>? neighbours, string code)
    {
        var sessionComparators = localAuthorityComparatorSetService
            .ReadUserDefinedComparatorSetFromSession(code)
            .Set;

        return sessionComparators.Length > 0
            ? sessionComparators
            : InitialComparatorSetFromNeighbours(neighbours).ToArray();
    }

    private async Task<LocalAuthorityStatisticalNeighbours> LocalAuthorityStatisticalNeighbours(string code) => await establishmentApi
        .GetLocalAuthorityStatisticalNeighbours(code)
        .GetResultOrThrow<LocalAuthorityStatisticalNeighbours>();

    private static List<string> InitialComparatorSetFromNeighbours(IEnumerable<LocalAuthorityStatisticalNeighbour>? neighbours) => (neighbours ?? [])
        .Select(n => n.Code)
        .Cast<string>()
        .ToList();
}