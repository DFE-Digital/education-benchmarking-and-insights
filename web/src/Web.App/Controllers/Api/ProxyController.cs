using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
namespace Web.App.Controllers.Api;

[ApiController]
[Route("api")]
public class ProxyController(
    ILogger<ProxyController> logger,
    IEstablishmentApi establishmentApi,
    IExpenditureApi expenditureApi,
    ISchoolComparatorSetService schoolComparatorSetService,
    IUserDataService userDataService)
    : Controller
{
    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/expenditure")]
    public async Task<IActionResult> EstablishmentExpenditure([FromQuery] string type, [FromQuery] string id, [FromQuery] string? phase)
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
                        return await SchoolExpenditure(id);
                    case OrganisationTypes.Trust:
                        return await TrustExpenditure(id, phase);
                    case OrganisationTypes.LocalAuthority:
                        return await LocalAuthorityExpenditure(id, phase);
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

    private async Task<IActionResult> LocalAuthorityExpenditure(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("laCode", id)
            .AddIfNotNull("phase", phase);

        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await expenditureApi
            .QuerySchools(BuildQuery(schools.Select(x => x.URN).OfType<string>()))
            .GetResultOrThrow<SchoolExpenditure>();
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditure(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("companyNumber", id)
            .AddIfNotNull("phase", phase);
        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await expenditureApi
            .QuerySchools(BuildQuery(schools.Select(x => x.URN).OfType<string>()))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(result);
    }

    private async Task<IActionResult> SchoolExpenditure(string id)
    {
        var userData = await userDataService.GetSchoolDataAsync(User.UserId(), id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            var defaultResult = await expenditureApi
                .QuerySchools(BuildQuery(defaultSet.Pupil))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return new JsonResult(defaultResult);
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildQuery(userDefinedSet.Set))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(userDefinedResult);
    }

    private static ApiQuery BuildQuery(IEnumerable<string> urns)
    {
        var query = new ApiQuery();
        foreach (var urn in urns)
        {
            query.AddIfNotNull("urns", urn);
        }

        return query;
    }
}