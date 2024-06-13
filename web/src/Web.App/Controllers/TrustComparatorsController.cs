using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/comparators")]
public class TrustComparatorsController(
    ILogger<TrustComparatorsController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetApi comparatorSetApi,
    ITrustInsightApi trustInsightApi,
    IUserDataService userDataService,
    ITrustComparatorSetService trustComparatorSetService) : Controller
{
    [HttpGet]
    public IActionResult Index(string companyNumber) => new StatusCodeResult(StatusCodes.Status302Found);

    [HttpGet]
    [Route("user-defined")]
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> UserDefined(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userData = await userDataService.GetTrustDataAsync(User.UserId(), companyNumber);
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
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
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
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
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

                var userData = await userDataService.GetTrustDataAsync(User.UserId(), companyNumber);
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