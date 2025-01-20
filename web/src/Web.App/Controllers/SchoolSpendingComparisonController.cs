﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[SchoolAuthorization]
[FeatureGate(FeatureFlags.CustomData)]
[Route("school/{urn}/spending-comparison")]
[SchoolRequestTelemetry(TrackedRequestFeature.CustomisedData)]
public class SchoolSpendingComparisonController(
    IEstablishmentApi establishmentApi,
    IUserDataService userDataService,
    IMetricRagRatingApi metricRagRatingApi,
    ILogger<SchoolSpendingComparisonController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var userCustomData = await userDataService.GetCustomDataActiveAsync(User, urn);
                if (userCustomData?.Status != Pipeline.JobStatus.Complete)
                {
                    return RedirectToAction("Index", "School", new
                    {
                        urn
                    });
                }

                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolSpendingComparison(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var originalRating = await metricRagRatingApi.GetDefaultAsync(new ApiQuery().AddIfNotNull("urns", urn)).GetResultOrThrow<RagRating[]>();
                var customRating = await metricRagRatingApi.CustomAsync(userCustomData.Id!).GetResultOrThrow<RagRating[]>();

                var viewModel = new SchoolSpendingComparisonViewModel(school, originalRating, customRating);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying customised side by side data: {DisplayUrl}",
                    Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}