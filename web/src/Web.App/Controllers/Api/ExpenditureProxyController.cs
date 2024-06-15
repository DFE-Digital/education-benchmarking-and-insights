using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/expenditure")]
public class ExpenditureProxyController(
    ILogger<ExpenditureProxyController> logger,
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IUserDataService userDataService) : Controller
{
    [HttpGet]
    [Produces("application/json")]
    public async Task<IActionResult> Query([FromQuery] string type, [FromQuery] string id, [FromQuery] string category, [FromQuery] string dimension, [FromQuery] string? phase)
    {
        using (logger.BeginScope(new
        {
            type,
            id
        }))
        {
            try
            {
                switch (type.ToLower())
                {
                    case OrganisationTypes.School:
                        return await SchoolExpenditure(id, category, dimension);
                    case OrganisationTypes.Trust:
                        return await TrustExpenditure(id, phase, category, dimension);
                    case OrganisationTypes.LocalAuthority:
                        return await LocalAuthorityExpenditure(id, phase, category, dimension);
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }


    [HttpGet]
    [Produces("application/json")]
    [Route("history")]
    public async Task<IActionResult> History([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new { type, id }))
        {
            try
            {
                var query = new ApiQuery().AddIfNotNull("dimension", dimension);

                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await expenditureApi.SchoolHistory(id, query).GetResultOrDefault<ExpenditureHistory[]>(),
                    OrganisationTypes.Trust => await expenditureApi.TrustHistory(id, query).GetResultOrDefault<ExpenditureHistory[]>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting expenditure history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> LocalAuthorityExpenditure(string id, string? phase, string? category, string? dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull("laCode", id)
            .AddIfNotNull("phase", phase);

        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await expenditureApi
            .QuerySchools(BuildQuery(schools.Select(x => x.URN).OfType<string>(), category, dimension))
            .GetResultOrThrow<SchoolExpenditure>();
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditure(string id, string? phase, string? category, string? dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull("companyNumber", id)
            .AddIfNotNull("phase", phase);
        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await expenditureApi
            .QuerySchools(BuildQuery(schools.Select(x => x.URN).OfType<string>(), category, dimension))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(result);
    }

    private async Task<IActionResult> SchoolExpenditure(string id, string? category, string? dimension)
    {
        var userData = await userDataService.GetSchoolDataAsync(User.UserId(), id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            var set = category is "PremisesStaffServices" or "Utilities"
                ? defaultSet.Building
                : defaultSet.Pupil;

            var defaultResult = await expenditureApi
                .QuerySchools(BuildQuery(set, category, dimension))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return new JsonResult(defaultResult);
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildQuery(userDefinedSet.Set, category, dimension))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(userDefinedResult);
    }

    private static ApiQuery BuildQuery(IEnumerable<string> urns, string? category, string? dimension)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension);
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}