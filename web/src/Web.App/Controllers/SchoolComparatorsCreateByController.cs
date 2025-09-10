using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Authorize]
[Route("school/{urn}/comparators/create")]
[ValidateUrn]
public class SchoolComparatorsCreateByController(
    ILogger<SchoolComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    ISchoolInsightApi schoolInsightApi,
    IComparatorSetApi comparatorSetApi,
    IComparatorApi comparatorApi
) : Controller
{
    [HttpGet]
    [Route("by")]
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
    [Route("by")]
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

        ModelState.AddModelError(nameof(by), "Select how you want to choose similar schools");
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

        var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        var viewModel = new SchoolComparatorsViewModel(school, by);
        return View(viewModel);
    }

    [HttpGet]
    [Route("by/name")]
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
                UserDefinedSchoolComparatorSet? userDefinedSet;
                if (string.IsNullOrEmpty(identifier))
                {
                    userDefinedSet = schoolComparatorSetService.ReadUserDefinedComparatorSetFromSession(urn);
                }
                else
                {
                    userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(urn, identifier);
                    schoolComparatorSetService.ClearUserDefinedComparatorSetFromSession(urn, identifier);
                    if (userDefinedSet != null)
                    {
                        schoolComparatorSetService.SetUserDefinedComparatorSetInSession(urn, userDefinedSet);
                    }
                }

                var schoolCharacteristics = userDefinedSet is { Set.Length: > 0 }
                    ? await GetSchoolCharacteristics<SchoolCharacteristicUserDefined>(userDefinedSet.Set)
                    : [];

                var isEdit = !string.IsNullOrEmpty(userDefinedSet?.RunId);

                var viewModel = new SchoolComparatorsByNameViewModel(school, schoolCharacteristics, isEdit);
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
    [Route("by/name")]
    [ExportModelState]
    public IActionResult Name([FromRoute] string urn, [FromForm] SchoolComparatorAddViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name");
        }

        var userDefinedSet = schoolComparatorSetService.ReadUserDefinedComparatorSetFromSession(urn);
        if (!string.IsNullOrWhiteSpace(viewModel.Urn) && !userDefinedSet.Set.Contains(viewModel.Urn))
        {
            var countOthers = userDefinedSet.Set.Count(s => s != urn);
            if (countOthers >= 29)
            {
                ModelState.AddModelError(nameof(SchoolComparatorAddViewModel.Urn), "Maximum number of comparison schools reached");
                return RedirectToAction("Name");
            }

            userDefinedSet.Set = userDefinedSet.Set.ToList().Append(viewModel.Urn).ToArray();
            schoolComparatorSetService.SetUserDefinedComparatorSetInSession(urn, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            urn
        });
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult Remove([FromRoute] string urn, [FromForm] SchoolComparatorRemoveViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name", new
            {
                urn
            });
        }

        var userDefinedSet = schoolComparatorSetService.ReadUserDefinedComparatorSetFromSession(urn);
        if (!string.IsNullOrWhiteSpace(viewModel.Urn) && userDefinedSet.Set.Contains(viewModel.Urn))
        {
            var set = userDefinedSet.Set.ToList();
            set.Remove(viewModel.Urn);
            userDefinedSet.Set = set.ToArray();
            schoolComparatorSetService.SetUserDefinedComparatorSetInSession(urn, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            urn
        });
    }

    [HttpGet]
    [Route("submit")]
    public async Task<IActionResult> Submit(string urn)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.HiddenNavigation] = true;

                await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var userDefinedSet = schoolComparatorSetService.ReadUserDefinedComparatorSetFromSession(urn);
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

                var isEdit = !string.IsNullOrEmpty(userDefinedSet.RunId);

                var request = new PostComparatorSetUserDefinedRequest
                {
                    Set = userDefinedSet.Set,
                    UserId = User.UserGuid().ToString()
                };

                await comparatorSetApi.UpsertUserDefinedSchoolAsync(urn, request).EnsureSuccess();
                schoolComparatorSetService.ClearUserDefinedComparatorSetFromSession(urn);
                schoolComparatorSetService.ClearUserDefinedCharacteristic(urn);

                return RedirectToAction("Submitted", new
                {
                    urn,
                    updating = isEdit ? bool.TrueString.ToLower() : null
                });
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error submitting school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("submitted")]
    public async Task<IActionResult> Submitted(string urn, bool? updating = null)
    {
        using (logger.BeginScope(new
        {
            urn
        }))
        {
            try
            {
                ViewData[ViewDataKeys.HiddenNavigation] = true;
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsSubmittedViewModel(school, updating == true);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators submission status: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("by/characteristic")]
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
                var characteristics = await GetSchoolCharacteristics<SchoolCharacteristic>([urn]);

                var userDefinedCharacteristic = schoolComparatorSetService.ReadUserDefinedCharacteristic(urn);
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
    [Route("by/characteristic")]
    [ExportModelState]
    public async Task<IActionResult> Characteristic([FromRoute] string urn, [FromForm] UserDefinedSchoolCharacteristicViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            urn,
            viewModel
        }))
        {
            try
            {
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

                    viewModel.LaNamesMutated = true;
                    schoolComparatorSetService.SetUserDefinedCharacteristic(urn, viewModel);
                    return RedirectToAction(nameof(Characteristic), new
                    {
                        urn
                    });
                }

                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted Characteristic failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());

                    // ensure model state correctly persists multiple LAs upon validation error elsewhere
                    if (!string.IsNullOrWhiteSpace(viewModel.LaInput))
                    {
                        ModelState.SetModelValue(
                            nameof(viewModel.LaNames),
                            viewModel.LaNames,
                            string.Join(",", viewModel.LaNames.Concat([viewModel.LaInput]).Distinct().ToArray()));
                        ModelState.SetModelValue(nameof(viewModel.LaInput), null, null);
                        ModelState.SetModelValue(nameof(viewModel.Code), null, null);
                    }

                    schoolComparatorSetService.ClearUserDefinedCharacteristic(urn);
                    return RedirectToAction(nameof(Characteristic));
                }

                // build and submit comparator schools based on characteristics
                if (!string.IsNullOrWhiteSpace(viewModel.LaInput))
                {
                    viewModel.LaNames = viewModel.LaNames.Concat([viewModel.LaInput]).ToArray();
                    viewModel.LaInput = null;
                    viewModel.Code = null;
                    viewModel.LaNamesMutated = false;
                }

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var request = new PostSchoolComparatorsRequest(school.LAName, viewModel);
                var results = await comparatorApi.CreateSchoolsAsync(urn, request).GetResultOrThrow<ComparatorSchools>();

                // try again if too few results returned
                // todo: unhappy path(s) under review as part of other ticket(s)
                if (results.TotalSchools < 2)
                {
                    ModelState.AddModelError(string.Empty, "Unable to find any matching schools. Modify the characteristics and try again.");
                    return RedirectToAction(nameof(Characteristic));
                }

                schoolComparatorSetService.SetUserDefinedCharacteristic(urn, viewModel);
                schoolComparatorSetService.SetUserDefinedComparatorSetInSession(urn, new UserDefinedSchoolComparatorSet
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
    [Route("preview")]
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
                var userDefinedSet = schoolComparatorSetService.ReadUserDefinedComparatorSetFromSession(urn);
                if (userDefinedSet.Set.Length <= 1)
                {
                    return RedirectToAction(nameof(Characteristic), new
                    {
                        urn
                    });
                }

                var userDefinedCharacteristic = schoolComparatorSetService.ReadUserDefinedCharacteristic(urn);
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