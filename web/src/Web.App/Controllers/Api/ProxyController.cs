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
    IFinanceService financeService,
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

    [HttpGet]
    [Produces("application/json")]
    [Route("establishments/expenditure/history")]
    public async Task<IActionResult> EstablishmentExpenditureHistory([FromQuery] string type, [FromQuery] string id, [FromQuery] string dimension)
    {
        using (logger.BeginScope(new
        {
            type,
            id
        }))
        {
            try
            {
                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await financeService.GetSchoolExpenditureHistory(id, dimension),
                    OrganisationTypes.Trust => await financeService.GetTrustExpenditureHistory(id, dimension),
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


    private async Task<IActionResult> LocalAuthorityExpenditure(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("laCode", id)
            .AddIfNotNull("phase", phase);

        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await financeService.GetExpenditure(schools.Select(x => x.URN).OfType<string>());
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditure(string id, string? phase)
    {
        var query = new ApiQuery()
            .AddIfNotNull("companyNumber", id)
            .AddIfNotNull("phase", phase);
        var schools = await establishmentApi.QuerySchools(query).GetResultOrThrow<IEnumerable<School>>();
        var result = await financeService.GetExpenditure(schools.Select(x => x.URN).OfType<string>());
        return new JsonResult(result);
    }

    private async Task<IActionResult> SchoolExpenditure(string id)
    {
        var userData = await userDataService.GetSchoolDataAsync(User.UserId(), id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            var defaultResult = await financeService.GetExpenditure(defaultSet.Pupil);
            return new JsonResult(defaultResult);
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        var userDefinedResult = await financeService.GetExpenditure(userDefinedSet.Set);
        return new JsonResult(userDefinedResult);
    }
}