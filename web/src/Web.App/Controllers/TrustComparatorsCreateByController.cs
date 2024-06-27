using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Attributes;
using Web.App.Attributes.RequestTelemetry;
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
[FeatureGate(FeatureFlags.UserDefinedComparators, FeatureFlags.Trusts)]
[Route("trust/{companyNumber}/comparators/create")]
[TrustRequestTelemetry(TrackedRequestFeature.Comparators)]
public class TrustComparatorsCreateByController(
    ILogger<TrustComparatorsCreateByController> logger,
    IEstablishmentApi establishmentApi,
    ITrustComparatorSetService trustComparatorSetService,
    ITrustInsightApi trustInsightApi,
    IComparatorSetApi comparatorSetApi,
    IComparatorApi comparatorApi
) : Controller
{
    [HttpGet]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var viewModel = new TrustComparatorsViewModel(trust);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by")]
    public async Task<IActionResult> Index(string companyNumber, [FromForm] string? by)
    {
        if (!string.IsNullOrWhiteSpace(by))
        {
            return by.Equals("name", StringComparison.OrdinalIgnoreCase)
                ? RedirectToAction("Name", new
                {
                    companyNumber
                })
                : RedirectToAction("Characteristic", new
                {
                    companyNumber
                });
        }

        ModelState.AddModelError(nameof(by), "Select whether to choose trusts by name or characteristic");
        ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.TrustComparators(companyNumber);

        var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
        var viewModel = new TrustComparatorsViewModel(trust, by);
        return View(viewModel);
    }

    [HttpGet]
    [Route("by/name")]
    [ImportModelState]
    public async Task<IActionResult> Name(string companyNumber, [FromQuery] string? identifier = null)
    {
        using (logger.BeginScope(new
        {
            companyNumber,
            identifier
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    companyNumber
                }));

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                UserDefinedTrustComparatorSet userDefinedSet;
                if (string.IsNullOrEmpty(identifier))
                {
                    userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
                }
                else
                {
                    userDefinedSet = await trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber, identifier);
                    trustComparatorSetService.ClearUserDefinedComparatorSet(companyNumber, identifier);
                    trustComparatorSetService.SetUserDefinedComparatorSet(companyNumber, userDefinedSet);
                }

                var trustsQuery = new ApiQuery();
                foreach (var selectedCompanyNumber in userDefinedSet.Set)
                {
                    trustsQuery.AddIfNotNull("companyNumbers", selectedCompanyNumber);
                }

                var trustCharacteristics = await GetTrustCharacteristics<TrustCharacteristicUserDefined>(userDefinedSet.Set);
                var viewModel = new TrustComparatorsByNameViewModel(trust, trustCharacteristics);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by name: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by/name")]
    [ExportModelState]
    public IActionResult Name([FromRoute] string companyNumber, [FromForm] TrustComparatorsCompanyNumberViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name");
        }

        var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
        if (!string.IsNullOrWhiteSpace(viewModel.CompanyNumber) && !userDefinedSet.Set.Contains(viewModel.CompanyNumber))
        {
            var countOthers = userDefinedSet.Set.Count(s => s != companyNumber);
            if (countOthers >= 9)
            {
                ModelState.AddModelError(nameof(TrustComparatorsCompanyNumberViewModel.CompanyNumber), "Maximum number of comparison trusts reached");
                return RedirectToAction("Name");
            }

            userDefinedSet.Set = userDefinedSet.Set.ToList().Append(viewModel.CompanyNumber).ToArray();
            trustComparatorSetService.SetUserDefinedComparatorSet(companyNumber, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            companyNumber
        });
    }

    [HttpPost]
    [Route("remove")]
    public IActionResult Remove([FromRoute] string companyNumber, [FromForm] TrustComparatorsCompanyNumberViewModel viewModel)
    {
        if (!ModelState.IsValid)
        {
            return RedirectToAction("Name", new
            {
                companyNumber
            });
        }

        var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
        if (!string.IsNullOrWhiteSpace(viewModel.CompanyNumber) && userDefinedSet.Set.Contains(viewModel.CompanyNumber))
        {
            var set = userDefinedSet.Set.ToList();
            set.Remove(viewModel.CompanyNumber);
            userDefinedSet.Set = set.ToArray();
            trustComparatorSetService.SetUserDefinedComparatorSet(companyNumber, userDefinedSet);
        }

        return RedirectToAction("Name", new
        {
            companyNumber
        });
    }

    [HttpGet]
    [Route("submit")]
    public async Task<IActionResult> Submit(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.HiddenNavigation] = true;

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
                if (userDefinedSet.Set.Length == 0)
                {
                    return RedirectToAction("Index", new
                    {
                        urn = companyNumber
                    });
                }

                if (!userDefinedSet.Set.Contains(companyNumber))
                {
                    // ensure current trust is in the set
                    var list = userDefinedSet.Set.ToList();
                    list.Add(companyNumber);
                    userDefinedSet.Set = list.ToArray();
                }

                var request = new PutComparatorSetUserDefinedRequest
                {
                    Identifier = userDefinedSet.RunId == null ? Guid.NewGuid() : Guid.Parse(userDefinedSet.RunId),
                    Set = userDefinedSet.Set,
                    UserId = User.UserId()
                };

                await comparatorSetApi.UpsertUserDefinedTrustAsync(companyNumber, request).EnsureSuccess();
                trustComparatorSetService.ClearUserDefinedComparatorSet(companyNumber);
                trustComparatorSetService.ClearUserDefinedCharacteristic(companyNumber);
                var viewModel = new TrustComparatorsSubmittedViewModel(trust, request);
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
    [Route("by/characteristic")]
    [ImportModelState]
    public async Task<IActionResult> Characteristic(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    companyNumber
                }));

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var characteristics = await GetTrustCharacteristics<TrustCharacteristic>(new[]
                {
                    companyNumber
                });

                var userDefinedCharacteristic = trustComparatorSetService.ReadUserDefinedCharacteristic(companyNumber);
                var viewModel = new TrustComparatorsByCharacteristicViewModel(trust, characteristics?.FirstOrDefault(), userDefinedCharacteristic);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by characteristic: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpPost]
    [Route("by/characteristic")]
    [ExportModelState]
    public async Task<IActionResult> Characteristic([FromRoute] string companyNumber, [FromForm] UserDefinedTrustCharacteristicViewModel viewModel)
    {
        using (logger.BeginScope(new
        {
            companyNumber,
            viewModel
        }))
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    logger.LogDebug("Posted Characteristic failed validation: {ModelState}",
                        ModelState.Where(m => m.Value != null && m.Value.Errors.Any()).ToJson());
                    trustComparatorSetService.ClearUserDefinedCharacteristic(companyNumber);
                    return RedirectToAction(nameof(Characteristic));
                }

                var request = new PostTrustComparatorsRequest(companyNumber, viewModel);
                var results = await comparatorApi.CreateTrustsAsync(request).GetResultOrThrow<ComparatorTrusts>();

                // try again if too few results returned
                // todo: unhappy path(s) under review as part of other ticket(s)
                if (results.TotalTrusts < 2)
                {
                    ModelState.AddModelError(string.Empty, "Unable to find any matching trusts. Modify the characteristics and try again.");
                    return RedirectToAction(nameof(Characteristic));
                }

                trustComparatorSetService.SetUserDefinedCharacteristic(companyNumber, viewModel);
                trustComparatorSetService.SetUserDefinedComparatorSet(companyNumber, new UserDefinedTrustComparatorSet
                {
                    Set = results.Trusts.ToArray(),
                    TotalTrusts = results.TotalTrusts
                });

                return RedirectToAction(nameof(Preview), new
                {
                    companyNumber
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
    public async Task<IActionResult> Preview(string companyNumber)
    {
        using (logger.BeginScope(new
        {
            companyNumber
        }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action(nameof(Index), new
                {
                    companyNumber
                }));

                var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var userDefinedSet = trustComparatorSetService.ReadUserDefinedComparatorSet(companyNumber);
                if (userDefinedSet.Set.Length <= 1)
                {
                    return RedirectToAction(nameof(Characteristic), new
                    {
                        companyNumber
                    });
                }

                var userDefinedCharacteristic = trustComparatorSetService.ReadUserDefinedCharacteristic(companyNumber);
                var characteristics = await GetTrustCharacteristics<TrustCharacteristic>(userDefinedSet.Set.Where(s => s != companyNumber));
                var viewModel = new TrustComparatorsPreviewViewModel(
                    trust,
                    characteristics,
                    userDefinedSet.Set.Length,
                    userDefinedSet.TotalTrusts.GetValueOrDefault(),
                    userDefinedCharacteristic);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying create trust comparators by characteristic: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<T[]?> GetTrustCharacteristics<T>(IEnumerable<string> set)
    {
        var query = new ApiQuery();
        var trusts = set as string[] ?? set.ToArray();
        if (trusts.Length != 0)
        {
            foreach (var companyNumber in trusts)
            {
                query.AddIfNotNull("companyNumbers", companyNumber);
            }
        }
        return await trustInsightApi.GetCharacteristicsAsync(query).GetResultOrDefault<T[]>();
    }
}