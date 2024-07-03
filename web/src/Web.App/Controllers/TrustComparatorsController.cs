using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Authorize]
[FeatureGate(FeatureFlags.Trusts, FeatureFlags.TrustComparison)]
[Route("trust/{companyNumber}/comparators")]
[TrustRequestTelemetry(TrackedRequestFeature.Comparators)]
public class TrustComparatorsController(
    ILogger<TrustComparatorsController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetApi comparatorSetApi,
    ITrustInsightApi trustInsightApi,
    IUserDataService userDataService,
    ITrustComparatorSetService trustComparatorSetService) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber,
        [FromQuery(Name = "comparator-generated")] bool? comparatorGenerated)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                if (userData.ComparatorSet == null)
                {
                    return RedirectToAction("Index", "TrustComparatorsCreateBy", new
                    {
                        companyNumber
                    });
                }

                var viewModel = new TrustToTrustViewModel(trust, comparatorGenerated);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust-to-trust landing: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("user-defined")]
    public async Task<IActionResult> UserDefined(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                TrustCharacteristicUserDefined[]? trusts = null;

                if (userData.ComparatorSet != null)
                {
                    var userDefinedSet = await comparatorSetApi.GetUserDefinedTrustAsync(companyNumber, userData.ComparatorSet)
                        .GetResultOrDefault<UserDefinedSchoolComparatorSet>();
                    if (userDefinedSet != null)
                    {
                        trusts = await GetTrustCharacteristics<TrustCharacteristicUserDefined>(userDefinedSet.Set);
                    }
                }

                var viewModel = new TrustComparatorsViewModel(trust, userDefined: trusts, userDefinedSetId: userData.ComparatorSet);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("revert")]
    public async Task<IActionResult> Revert(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
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
                logger.LogError(e, "An error reverting trust comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("revert")]
    public async Task<IActionResult> RevertSet(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var userData = await userDataService.GetTrustDataAsync(User, companyNumber);
                if (userData.ComparatorSet != null)
                {
                    await comparatorSetApi.RemoveUserDefinedTrustAsync(companyNumber, userData.ComparatorSet).EnsureSuccess();
                    trustComparatorSetService.ClearUserDefinedComparatorSet(companyNumber, userData.ComparatorSet);
                }

                trustComparatorSetService.ClearUserDefinedComparatorSet(companyNumber);
                trustComparatorSetService.ClearUserDefinedCharacteristic(companyNumber);
                return RedirectToAction("Index", "Trust", new
                {
                    companyNumber
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error reverting trust comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

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