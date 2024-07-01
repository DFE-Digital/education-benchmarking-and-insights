using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/spending-and-costs")]
[TrustRequestTelemetry(TrackedRequestFeature.Spending)]
public class TrustSpendingController(ILogger<TrustController> logger, IEstablishmentApi establishmentApi, IMetricRagRatingApi metricRagRatingApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(
        string companyNumber,
        [FromQuery(Name = "category")] string[]? categories,
        [FromQuery(Name = "priority")] string[]? priorities)
    {
        using (logger.BeginScope(new
        {
            companyNumber,
            category = categories,
            priority = priorities
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
                    schoolsQuery.AddIfNotNull("urns", school.URN);
                }

                if (categories != null)
                {
                    foreach (var category in categories)
                    {
                        var categoryParsed = Category.FromSlug(category);
                        if (!string.IsNullOrWhiteSpace(categoryParsed))
                        {
                            schoolsQuery.AddIfNotNull("categories", categoryParsed);
                        }
                    }
                }

                if (priorities != null)
                {
                    foreach (var priority in priorities)
                    {
                        var statusParsed = priority?.ToLowerInvariant() switch
                        {
                            "high" => "Red",
                            "medium" => "Amber",
                            "low" => "Green",
                            _ => null
                        };

                        if (!string.IsNullOrWhiteSpace(statusParsed))
                        {
                            schoolsQuery.AddIfNotNull("statuses", statusParsed);
                        }
                    }
                }

                var ratings = await metricRagRatingApi.GetDefaultAsync(schoolsQuery).GetResultOrThrow<RagRating[]>();
                var viewModel = new TrustSpendingViewModel(trust, schools, ratings, categories, priorities);

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