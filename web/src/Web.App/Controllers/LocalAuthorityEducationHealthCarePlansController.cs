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
[Route("local-authority/{code}/education-health-care-plans")]
[ValidateLaCode]
public class LocalAuthorityEducationHealthCarePlansController(
    ILogger<LocalAuthorityEducationHealthCarePlansController> logger,
    ILocalAuthorityApi api,
    ILocalAuthorityComparatorSetService comparatorSetService)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string code,
        Views.ViewAsOptions viewAs = Views.ViewAsOptions.Chart)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {

                var la = await api.SingleAsync(code).GetResultOrThrow<LocalAuthority>();

                var set = comparatorSetService.ReadUserDefinedComparatorSetFromSession(code).Set;
                if (set.Length == 0)
                {
                    return RedirectToAction("Index", "LocalAuthorityComparators", new { code, type = LocalAuthorityBenchmarkType.EducationHealthCarePlans });
                }

                var query = BuildQuery(new[]
                {
                    code
                }.Concat(set).ToArray(), "Per1000Pupil");
                var plans = await api
                    .QueryEhcpAsync(query)
                    .GetResultOrThrow<EducationHealthCarePlans[]>();

                var subCategories = new EducationHealthCarePlansComparisonSubCategoriesViewModel(plans, EducationHealthCarePlansCategories.All);

                var viewModel = new LocalAuthorityEducationHealthCarePlansViewModel(la, set, subCategories)
                {
                    ViewAs = viewAs,
                };

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority education, health care plans: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string code, int viewAs, int resultAs) => RedirectToAction("Index", new
    {
        code,
        viewAs
    });

    private static ApiQuery BuildQuery(string[] codes, string dimension)
    {
        var query = new ApiQuery();
        foreach (var c in codes)
        {
            query.AddIfNotNull("code", c);
        }

        query.AddIfNotNull("dimension", dimension);
        return query;
    }
}