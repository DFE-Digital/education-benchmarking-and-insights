using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
//todo: reinstate once DSI permissions resolved [SchoolAuthorization]
[FeatureGate(FeatureFlags.UserDefinedComparators)]
[Route("school/{urn}/comparators/create/by")]
public class SchoolComparatorsCreateByController(
    ILogger<SchoolComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetService comparatorSetService
) : Controller
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
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create school comparators by: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    public async Task<IActionResult> Index(string urn, [FromForm] string? by)
    {
        if (!string.IsNullOrWhiteSpace(by))
        {
            return by.Equals("name", StringComparison.OrdinalIgnoreCase)
                ? RedirectToAction("Name", new
                {
                    urn
                })
                : RedirectToAction("Characteristic", new
                {
                    urn
                });
        }

        ModelState.AddModelError(nameof(by), "Select whether to choose schools by name or characteristic");
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

        var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        var viewModel = new SchoolComparatorsViewModel(school, by);
        return View(viewModel);
    }

    [HttpGet]
    [Route("name")]
    [ImportModelState]
    public async Task<IActionResult> Name(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = comparatorSetService.ReadUserDefinedComparatorSet(urn);
                var viewModel = new SchoolComparatorsByNameViewModel(school, set);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create school comparators by name: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("name")]
    [ExportModelState]
    public IActionResult Name([FromRoute] string urn, [FromForm] SchoolComparatorsByNameAddViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name");
        }

        var set = comparatorSetService.ReadUserDefinedComparatorSet(urn);
        if (!string.IsNullOrWhiteSpace(viewModel.Urn) && !set.Urns.Contains(viewModel.Urn))
        {
            set.Urns = set.Urns.Append(viewModel.Urn);
            comparatorSetService.SetUserDefinedComparatorSet(urn, set);
        }

        return RedirectToAction("Name", new
        {
            urn
        });
    }

    [HttpGet]
    [Route("characteristic")]
    public async Task<IActionResult> Characteristic(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school);

                // todo: up later
                return StatusCode(StatusCodes.Status302Found);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create school comparators by characteristic: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}