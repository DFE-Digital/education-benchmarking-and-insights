using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/spending-and-costs")]
public class TrustSpendingController(ILogger<TrustController> logger, IEstablishmentApi establishmentApi, IInsightApi insightApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string companyNumber,
        [FromQuery(Name = "category")] int[]? costCategoryIds,
        [FromQuery(Name = "status")] string[]? statuses)
    {
        using (logger.BeginScope(new
        {
            companyNumber,
            costCategoryId = costCategoryIds,
            status = statuses
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "Trust", new
                {
                    companyNumber
                }));

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var trustQuery = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
                var schools = await establishmentApi.QuerySchools(trustQuery).GetResultOrDefault<School[]>() ?? [];

                var schoolsQuery = new ApiQuery();
                foreach (var school in schools)
                {
                    schoolsQuery.AddIfNotNull("urns", school.Urn);
                }

                if (costCategoryIds != null)
                {
                    foreach (var costCategoryId in costCategoryIds)
                    {
                        schoolsQuery.AddIfNotNull("categories", costCategoryId.ToString());
                    }
                }

                if (statuses != null)
                {
                    foreach (var status in statuses)
                    {
                        schoolsQuery.AddIfNotNull("statuses", status);
                    }
                }

                var ratings = await insightApi.GetRatings(schoolsQuery).GetResultOrThrow<RagRating[]>();
                var viewModel = new TrustSpendingViewModel(trust, schools, ratings);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying trust spending and costs: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}