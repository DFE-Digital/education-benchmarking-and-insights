using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Authorize]
[FeatureGate(FeatureFlags.UserDefinedComparators, FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/comparators/create")]
public class TrustComparatorsCreateByController(
    ILogger<TrustComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    ITrustComparatorSetService trustComparatorSetService,
    ITrustInsightApi trustInsightApi
) : Controller
{
    [HttpGet]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            urn = companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var viewModel = new TrustComparatorsViewModel(trust);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber, [FromForm] string? by)
    {
        if (!string.IsNullOrWhiteSpace(by))
        {
            return by.Equals("name", StringComparison.OrdinalIgnoreCase)
                ? RedirectToAction("Name", new
                {
                    companyNumber
                })
                : RedirectToAction("Characteristic", new
                {
                    companyNumber
                });
        }

        ModelState.AddModelError(nameof(by), "Select whether to choose trusts by name or characteristic");
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

        var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
        var viewModel = new TrustComparatorsViewModel(trust, by);
        return View(viewModel);
    }

    [HttpGet]
    [Route("by/name")]
    [ImportModelState]
    public async Task<IActionResult> Name(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber,
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    companyNumber
                }));

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
                var trustsQuery = new ApiQuery();
                foreach (var selectedCompanyNumber in userDefinedSet.Set)
                {
                    trustsQuery.AddIfNotNull("companyNumbers", selectedCompanyNumber);
                }

                var trustCharacteristics = await GetTrustCharacteristics<TrustCharacteristicUserDefined>(userDefinedSet.Set);
                var viewModel = new TrustComparatorsByNameViewModel(trust, trustCharacteristics);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by name: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by/name")]
    [ExportModelState]
    public IActionResult Name([FromRoute] string companyNumber, [FromForm] TrustComparatorsCompanyNumberViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name");
        }

        var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
        if (!string.IsNullOrWhiteSpace(viewModel.CompanyNumber) && !userDefinedSet.Set.Contains(viewModel.CompanyNumber))
        {
            var countOthers = userDefinedSet.Set.Count(s => s != companyNumber);
            if (countOthers >= 9)
            {
                ModelState.AddModelError(nameof(TrustComparatorsCompanyNumberViewModel.CompanyNumber), "Maximum number of comparison trusts reached");
                return RedirectToAction("Name");
            }

            userDefinedSet.Set = userDefinedSet.Set.ToList().Append(viewModel.CompanyNumber).ToArray();
            trustComparatorSetService.SetUserDefinedComparatorSet(companyNumber, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            companyNumber
        });
    }

    [HttpGet]
    [Route("by/characteristic")]
    public IActionResult Characteristic(string companyNumber) => new StatusCodeResult(StatusCodes.Status302Found);

    private async Task<T[]?> GetTrustCharacteristics<T>(IEnumerable<string> set)
    {
        var query = new ApiQuery();
        var trusts = set as string[] ?? set.ToArray();
        if (trusts.Length != 0)
        {
            foreach (var companyNumber in trusts)
            {
                query.AddIfNotNull("companyNumbers", companyNumber);
            }
        }
        return await trustInsightApi.GetCharacteristicsAsync(query).GetResultOrDefault<T[]>();
    }
}