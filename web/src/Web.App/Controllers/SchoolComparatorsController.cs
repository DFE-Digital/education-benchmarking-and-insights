using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparators")]
public class SchoolComparatorsController(ILogger<SchoolComparatorsController> logger, IEstablishmentApi establishmentApi, IComparatorSetApi comparatorSetApi, ISchoolInsightApi schoolInsightApi, IUserDataApi userDataApi) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetApi.GetDefaultSchoolAsync(urn).GetResultOrThrow<ComparatorSet>();
                var pupil = await GetSchoolCharacteristics<SchoolCharacteristicPupil>(set.Pupil);
                var building = await GetSchoolCharacteristics<SchoolCharacteristicBuilding>(set.Building);

                var viewModel = new SchoolComparatorsViewModel(school, pupil: pupil, building: building);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("user-defined")]
    [Authorize]
    [FeatureGate(FeatureFlags.UserDefinedComparators)]
    public async Task<IActionResult> UserDefined(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.SchoolComparators(urn);

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var query = new ApiQuery()
                    .AddIfNotNull("userId", User.UserId())
                    .AddIfNotNull("status", "active")
                    .AddIfNotNull("type", "comparator-set");

                var userSets = await userDataApi.GetAsync(query).GetResultOrDefault<UserData[]>();
                SchoolCharacteristicUserDefined[]? schools = null;

                if (userSets != null)
                {
                    var setId = userSets.FirstOrDefault()?.Id;
                    var userDefinedSet = await comparatorSetApi.GetUserDefinedSchoolAsync(urn, setId).GetResultOrDefault<ComparatorSetUserDefined>();
                    if (userDefinedSet != null)
                    {
                        schools = await GetSchoolCharacteristics<SchoolCharacteristicUserDefined>(userDefinedSet.Set);
                    }
                }

                var viewModel = new SchoolComparatorsViewModel(school, userDefined: schools);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
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