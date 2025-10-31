using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.ActionResults;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;
using Web.App.ViewModels.Components;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}")]
[ValidateLaCode]
public class LocalAuthorityController(
    ILogger<LocalAuthorityController> logger,
    IEstablishmentApi establishmentApi,
    IMetricRagRatingApi metricRagRatingApi,
    ICommercialResourcesService commercialResourcesService,
    ILocalAuthoritiesApi localAuthoritiesApi)
    : Controller
{
    [HttpGet]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.Home)]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var authority = await LocalAuthority(code);
                var ragRatings = await RagRatings(code);

                var viewModel = new LocalAuthorityViewModel(authority, ragRatings);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public IActionResult Index(string code, IFormCollection form)
    {
        var routeValues = new RouteValueDictionary
        {
            { "code", code }
        };

        ResetFormFields(form, routeValues);
        MergeOtherFormFields(form, routeValues);
        return RedirectToAction("Index", routeValues);
    }

    [HttpGet]
    [Route("find-ways-to-spend-less")]
    [LocalAuthorityRequestTelemetry(TrackedRequestFeature.Resources)]
    public async Task<IActionResult> Resources(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(code);

                var authority = await LocalAuthority(code);
                var resources = await commercialResourcesService.GetSubCategoryLinks();

                var viewModel = new LocalAuthorityResourcesViewModel(authority, resources);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download/schools/finance")]
    public async Task<IActionResult> DownloadSchoolsFinance(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                var results = await localAuthoritiesApi
                    .GetSchoolsFinance(code, [new QueryParameter("dimension", Dimensions.ResultAsOptions.Actuals.GetQueryParam())])
                    .GetResultOrDefault<LocalAuthoritySchoolFinancial[]>() ?? [];

                return new CsvResults([new CsvResult(results, $"la-schools-finance-{code}.csv")], $"la-schools-finance-{code}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading LA schools financial data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download/schools/workforce")]
    public async Task<IActionResult> DownloadSchoolsWorkforce(string code)
    {
        using (logger.BeginScope(new
        {
            code
        }))
        {
            try
            {
                var results = await localAuthoritiesApi
                    .GetSchoolsWorkforce(code, [new QueryParameter("dimension", SchoolsSummaryWorkforceDimensions.ResultAsOptions.Actuals.GetQueryParam())])
                    .GetResultOrDefault<LocalAuthoritySchoolWorkforce[]>() ?? [];

                return new CsvResults([new CsvResult(results, $"la-schools-workforce-{code}.csv")], $"la-schools-workforce-{code}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading LA schools workforce data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();

    private async Task<RagRatingSummary[]> RagRatings(string code) => await metricRagRatingApi
        .SummaryAsync(BuildQuery(code))
        .GetResultOrDefault<RagRatingSummary[]>() ?? [];

    private BacklinkInfo HomeLink(string code) => new(Url.Action("Index", new
    {
        code
    }));

    private static ApiQuery BuildQuery(string code)
    {
        var query = new ApiQuery();
        query.AddIfNotNull("laCode", code);
        return query;
    }

    private static void ResetFormFields(IFormCollection form, RouteValueDictionary routeValues)
    {
        var resetFields = form[LocalAuthorityViewModel.FormFieldNames.ResetFields]
            .SelectMany(v => (v ?? string.Empty).Split(","))
            .ToArray();
        foreach (var key in form.Keys)
        {
            // exclude special fields
            if (key.StartsWith("__") || resetFields.Contains(key))
            {
                continue;
            }

            var values = form[key];

            // disallow multiple sort and filter fields
            if (key.EndsWith(LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.Sort)
                || key.EndsWith(LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.FiltersVisible))
            {
                routeValues[key] = values.Last();
            }
            else
            {
                routeValues[key] = values;
            }
        }
    }

    private static void MergeOtherFormFields(IFormCollection form, RouteValueDictionary routeValues)
    {
        // include fields from 'other' form in route values, if present
        var otherForm = form[LocalAuthorityViewModel.FormFieldNames.OtherFormFields].ToString();
        if (string.IsNullOrWhiteSpace(otherForm))
        {
            return;
        }

        var otherValues = otherForm.FromJson<Dictionary<string, string[]>>();
        if (otherValues == null)
        {
            return;
        }

        foreach (var key in otherValues.Keys)
        {
            routeValues[key] = new StringValues(otherValues[key]);
        }
    }
}