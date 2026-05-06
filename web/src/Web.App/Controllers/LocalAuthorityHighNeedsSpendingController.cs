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
    public async Task<IActionResult> Index(string code,
        HighNeedsSpendingCategories.SubCategoryFilter[] selectedSubCategories,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart,
        HighNeedsDimensions.ResultAsOptions resultAs = HighNeedsDimensions.ResultAsOptions.PerPupil,
        HighNeedsDimensions.SubmissionTypeOptions type = HighNeedsDimensions.SubmissionTypeOptions.Outturn)
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
                resultAs,
                type);


                var expenditures = await api
                    .QueryHighNeedsV2Async(query)
                    .GetResultOrThrow<HighNeedsSpending[]>();

                var subCategories = new HighNeedsSpendingComparisonSubCategoriesViewModel(expenditures, selectedSubCategories, code);

                var viewModel = new LocalAuthorityHighNeedsSpendingViewModel(la, set, subCategories)
                {
                    SelectedSubCategories = selectedSubCategories,
                    ViewAs = viewAs,
                    ResultAs = resultAs,
                    Type = type
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

    [HttpPost]
    public IActionResult Index(string code, int[]? selectedSubCategories, int viewAs, int resultAs, int type) => RedirectToAction("Index", new
    {
        code,
        selectedSubCategories,
        viewAs,
        resultAs,
        type
    });

    private static ApiQuery BuildQuery(
        string[] codes,
        HighNeedsDimensions.ResultAsOptions dimension,
        HighNeedsDimensions.SubmissionTypeOptions submissionType)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension.GetResultAsQueryParam());

        query.AddIfNotNull("type", submissionType.GetSubmissionTypeQueryParam());

        return query;
    }
}