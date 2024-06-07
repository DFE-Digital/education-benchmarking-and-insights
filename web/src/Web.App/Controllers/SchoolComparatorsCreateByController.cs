using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;
namespace Web.App.Controllers;

[Controller]
[Authorize]
[FeatureGate(FeatureFlags.UserDefinedComparators)]
[Route("school/{urn}/comparators/create/by")]
public class SchoolComparatorsCreateByController(
    ILogger<SchoolComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    IComparatorSetService comparatorSetService,
    ISchoolInsightApi schoolInsightApi,
    IComparatorSetApi comparatorSetApi,
    IComparatorApi comparatorApi
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
    public async Task<IActionResult> Name(string urn, [FromQuery] string? identifier = null)
    {
        using (logger.BeginScope(new
        {
            urn,
            identifier
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    urn
                }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                ComparatorSetUserDefined userDefinedSet;
                if (string.IsNullOrEmpty(identifier))
                {
                    userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);
                }
                else
                {
                    userDefinedSet = await comparatorSetService.ReadUserDefinedComparatorSet(urn, identifier);
                    comparatorSetService.ClearUserDefinedComparatorSet(urn, identifier);
                    comparatorSetService.SetUserDefinedComparatorSet(urn, userDefinedSet);
                }

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
            var countOthers = userDefinedSet.Set.Count(s => s != urn);
            if (countOthers >= 29)
            {
                ModelState.AddModelError(nameof(SchoolComparatorsUrnViewModel.Urn), "Maximum number of comparison schools reached");
                return RedirectToAction("Name");
            }

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
    [Route("submit")]
    [ImportModelState]
    public async Task<IActionResult> Submit(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);
                if (userDefinedSet.Set.Length == 0)
                {
                    return RedirectToAction("Index", new
                    {
                        urn
                    });
                }

                if (!userDefinedSet.Set.Contains(urn))
                {
                    //Ensure current school is in the set
                    var list = userDefinedSet.Set.ToList();
                    list.Add(urn);
                    userDefinedSet.Set = list.ToArray();
                }

                var request = new PutComparatorSetUserDefinedRequest
                {
                    Identifier = userDefinedSet.RunId == null ? Guid.NewGuid() : Guid.Parse(userDefinedSet.RunId),
                    URN = urn,
                    Set = userDefinedSet.Set,
                    UserId = User.UserId()
                };

                await comparatorSetApi.UpsertUserDefinedSchoolAsync(request).EnsureSuccess();
                comparatorSetService.ClearUserDefinedComparatorSet(urn);
                comparatorSetService.ClearUserDefinedCharacteristic(urn);
                var viewModel = new SchoolComparatorsSubmittedViewModel(school, request);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error submitting school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("characteristic")]
    [ImportModelState]
    public async Task<IActionResult> Characteristic(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    urn
                }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var characteristics = await GetSchoolCharacteristics<SchoolCharacteristic>(new[]
                {
                    urn
                });

                var userDefinedCharacteristic = comparatorSetService.ReadUserDefinedCharacteristic(urn);
                var viewModel = new SchoolComparatorsByCharacteristicViewModel(school, characteristics?.FirstOrDefault(), userDefinedCharacteristic);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create school comparators by characteristic: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("characteristic")]
    [ExportModelState]
    public async Task<IActionResult> Characteristic([FromRoute] string urn, [FromForm] UserDefinedCharacteristicViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            urn,
            viewModel
        }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted Characteristic failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                    return RedirectToAction(nameof(Characteristic));
                }

                // append/remove LA rather than submit entire form
                if (Request.Form.TryGetValue("action", out var action)
                    && (action == FormAction.Add || action.ToString().StartsWith(FormAction.Remove)))
                {
                    if (action == FormAction.Add)
                    {
                        if (!string.IsNullOrWhiteSpace(viewModel.LaInput))
                        {
                            viewModel.LaNames = viewModel.LaNames.Concat([viewModel.LaInput]).ToArray();
                            viewModel.LaInput = null;
                            viewModel.Code = null;
                        }
                    }
                    else
                    {
                        var laName = action.ToString()[(FormAction.Remove.Length + 1)..];
                        viewModel.LaNames = viewModel.LaNames.Except([laName]).ToArray();
                    }

                    comparatorSetService.SetUserDefinedCharacteristic(urn, viewModel);
                    return RedirectToAction(nameof(Characteristic), new
                    {
                        urn
                    });
                }

                if (!string.IsNullOrWhiteSpace(viewModel.LaInput))
                {
                    viewModel.LaNames = viewModel.LaNames.Concat([viewModel.LaInput]).ToArray();
                    viewModel.LaInput = null;
                    viewModel.Code = null;
                }

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var request = new PostSchoolComparatorsRequest(urn, school.LAName, viewModel);
                var results = await comparatorApi.CreateSchoolsAsync(request).GetResultOrThrow<ComparatorSchools>();

                // try again if too few results returned
                // todo: unhappy path(s) under review as part of other ticket(s)
                if (results.TotalSchools < 2)
                {
                    ModelState.AddModelError(string.Empty, "Unable to find any matching schools. Modify the characteristics and try again.");
                    return RedirectToAction(nameof(Characteristic));
                }

                comparatorSetService.SetUserDefinedCharacteristic(urn, viewModel);
                comparatorSetService.SetUserDefinedComparatorSet(urn, new ComparatorSetUserDefined
                {
                    Set = results.Schools.ToArray(),
                    TotalSchools = results.TotalSchools
                });

                return RedirectToAction(nameof(Preview), new
                {
                    urn
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error occurred managing user defined characteristics: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(StatusCodes.Status400BadRequest);
            }
        }
    }

    [HttpGet]
    [Route("characteristic/preview")]
    [ImportModelState]
    public async Task<IActionResult> Preview(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    urn
                }));

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userDefinedSet = comparatorSetService.ReadUserDefinedComparatorSet(urn);
                if (userDefinedSet.Set.Length <= 1)
                {
                    return RedirectToAction(nameof(Characteristic), new
                    {
                        urn
                    });
                }

                var userDefinedCharacteristic = comparatorSetService.ReadUserDefinedCharacteristic(urn);
                var characteristics = await GetSchoolCharacteristics<SchoolCharacteristic>(userDefinedSet.Set.Where(s => s != urn));
                var viewModel = new SchoolComparatorsPreviewViewModel(
                    school,
                    characteristics,
                    userDefinedSet.Set.Length,
                    userDefinedSet.TotalSchools.GetValueOrDefault(),
                    userDefinedCharacteristic);
                return View(viewModel);
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