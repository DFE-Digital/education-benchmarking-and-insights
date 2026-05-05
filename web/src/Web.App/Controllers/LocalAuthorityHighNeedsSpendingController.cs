using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
using LocalAuthority = Web.App.Domain.LocalAuthority;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/high-needs-spending")]
[ValidateLaCode]
public class LocalAuthorityHighNeedsSpendingController(
    ILogger<LocalAuthorityHighNeedsSpendingController> logger,
    ILocalAuthorityApi api,
    ILocalAuthorityComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {

                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

                var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code, type = LocalAuthorityBenchmarkType.HighNeedsSpending });
                }

                var query = BuildQuery(new[]
                {
                    code
                }.Concat(set).ToArray(),
                    HighNeedsDimensions.ResultAsOptions.PerEhcp.GetResultAsQueryParam(),
                    HighNeedsDimensions.SubmissionTypeOptions.Budget.GetSubmissionTypeQueryParam());


                var expenditures = await api
                    .QueryHighNeedsV2Async(query)
                    .GetResultOrThrow<HighNeedsSpending[]>();

                var subCategories = new HighNeedsSpendingComparisonSubCategoriesViewModel(expenditures, HighNeedsSpendingCategories.All, code);

                var viewModel = new LocalAuthorityHighNeedsSpendingViewModel(la, set, subCategories)
                {
                    SelectedSubCategories = HighNeedsSpendingCategories.All,
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority high needs spending: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(string[] codes, string dimension, string submissionType)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension);

        query.AddIfNotNull("type", submissionType);

        return query;
    }
}