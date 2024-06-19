using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/balance")]
public class BalanceProxyController(
    ILogger<BalanceProxyController> logger,
    IBalanceApi api,
    IUserDataService userDataService,
    ITrustComparatorSetService trustComparatorSetService) : Controller
{
    /// <param name="type" example="school"></param>
    /// <param name="id" example="148793"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="includeBreakdown"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<BalanceHistory[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] bool? includeBreakdown)
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
                    OrganisationTypes.School => await api
                        .SchoolHistory(id, BuildQuery(dimension, includeBreakdown))
                        .GetResultOrDefault<BalanceHistory[]>(),
                    OrganisationTypes.Trust => await api
                        .TrustHistory(id, BuildQuery(dimension, includeBreakdown))
                        .GetResultOrDefault<BalanceHistory[]>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting balance history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    /// <param name="type" example="trust"></param>
    /// <param name="id" example="07465701"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="includeBreakdown"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<TrustBalance[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("user-defined")]
    [Authorize]
    public async Task<IActionResult> UserDefined(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] bool? includeBreakdown)
    {
        using (logger.BeginScope(new
        {
            type,
            id
        }))
        {
            try
            {
                return type.ToLower() switch
                {
                    OrganisationTypes.Trust => await TrustBalanceUserDefined(id, dimension, includeBreakdown),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting user-defined balance data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> TrustBalanceUserDefined(string id, string? dimension, bool? includeBreakdown)
    {
        var userData = await userDataService.GetTrustDataAsync(User.UserId(), id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            return new NotFoundResult();
        }

        var userDefinedSet = await trustComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        var userDefinedResult = await api
            .QueryTrusts(BuildQuery(dimension, includeBreakdown, userDefinedSet.Set, "companyNumbers"))
            .GetResultOrThrow<TrustBalance[]>();

        return new JsonResult(userDefinedResult);
    }

    private static ApiQuery BuildQuery(string? dimension, bool? includeBreakdown, IEnumerable<string>? ids = null, string? idQueryName = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("includeBreakdown", includeBreakdown);

        if (ids != null && !string.IsNullOrWhiteSpace(idQueryName))
        {
            foreach (var id in ids)
            {
                query.AddIfNotNull(idQueryName, id);
            }
        }

        return query;
    }
}