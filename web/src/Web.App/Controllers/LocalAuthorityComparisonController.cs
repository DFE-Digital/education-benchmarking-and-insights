﻿using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.LocalAuthorities)]
[Route("local-authority/{code}/comparison")]
[ValidateLaCode]
[LocalAuthorityRequestTelemetry(TrackedRequestFeature.BenchmarkCosts)]
public class LocalAuthorityComparisonController(
    IEstablishmentApi establishmentApi,
    ILogger<LocalAuthorityComparisonController> logger)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityComparison(code);

                var localAuthority = await LocalAuthority(code);
                var viewModel = new LocalAuthorityComparisonViewModel(localAuthority);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();
}