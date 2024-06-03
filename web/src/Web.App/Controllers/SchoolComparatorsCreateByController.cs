using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
//todo: reinstate once DSI permissions resolved [SchoolAuthorization]
[FeatureGate(FeatureFlags.UserDefinedComparators)]
[Route("school/{urn}/comparators/create/by")]
public class SchoolComparatorsCreateByController(
    ILogger<SchoolComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetService comparatorSetService,
    ISchoolInsightApi schoolInsightApi
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
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new { urn }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);

                var schoolsQuery = new ApiQuery();
                foreach (var selectedUrn in userDefinedSet.Set)
                {
                    schoolsQuery.AddIfNotNull("urns", selectedUrn);
                }

                var schoolCharacteristics = await GetSchoolCharacteristics<SchoolCharacteristicUserDefined>(userDefinedSet.Set);
                var viewModel = new SchoolComparatorsByNameViewModel(school, schoolCharacteristics);
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
    public IActionResult Name([FromRoute] string urn, [FromForm] SchoolComparatorsUrnViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name");
        }

        var userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);
        if (!string.IsNullOrWhiteSpace(viewModel.Urn) && !userDefinedSet.Set.Contains(viewModel.Urn))
        {
            userDefinedSet.Set = userDefinedSet.Set.ToList().Append(viewModel.Urn).ToArray();
            comparatorSetService.SetUserDefinedComparatorSet(urn, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            urn
        });
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult Remove([FromRoute] string urn, [FromForm] SchoolComparatorsUrnViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name", new
            {
                urn
            });
        }

        var userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);
        if (!string.IsNullOrWhiteSpace(viewModel.Urn) && userDefinedSet.Set.Contains(viewModel.Urn))
        {
            var set = userDefinedSet.Set.ToList();
            set.Remove(viewModel.Urn);
            userDefinedSet.Set = set.ToArray();
            comparatorSetService.SetUserDefinedComparatorSet(urn, userDefinedSet);
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
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new { urn }));

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

    private async Task<T[]?> GetSchoolCharacteristics<T>(IEnumerable<string> set)
    {
        var query = new ApiQuery();
        var schools = set as string[] ?? set.ToArray();
        if (schools.Length != 0)
        {
            foreach (var urn in schools)
            {
                query.AddIfNotNull("urns", urn);
            }
        }
        return await schoolInsightApi.GetCharacteristicsAsync(query).GetResultOrDefault<T[]>();
    }
}