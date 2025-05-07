using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
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
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<BalanceHistoryRows>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("history")]
    [ValidateId]
    public async Task<IActionResult> History(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        CancellationToken cancellationToken = default)
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
                        .SchoolHistory(id, BuildQuery(dimension), cancellationToken)
                        .GetResultOrDefault<BalanceHistoryRows>(),
                    OrganisationTypes.Trust => await api
                        .TrustHistory(id, BuildQuery(dimension), cancellationToken)
                        .GetResultOrDefault<BalanceHistoryRows>(),
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
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<TrustBalance[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("user-defined")]
    [Authorize]
    [ValidateId]
    public async Task<IActionResult> UserDefined(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        CancellationToken cancellationToken = default)
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
                    OrganisationTypes.Trust => await TrustBalanceUserDefined(id, dimension, cancellationToken),
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

    private async Task<IActionResult> TrustBalanceUserDefined(string id, string? dimension, CancellationToken cancellationToken = default)
    {
        var userData = await userDataService.GetTrustDataAsync(User, id, cancellationToken);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            return new NotFoundResult();
        }

        var userDefinedSet = await trustComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet, cancellationToken);
        var userDefinedResult = await api
            .QueryTrusts(BuildQuery(dimension, userDefinedSet.Set, "companyNumbers"), cancellationToken)
            .GetResultOrThrow<TrustBalance[]>();

        return new JsonResult(userDefinedResult);
    }

    private static ApiQuery BuildQuery(string? dimension, IEnumerable<string>? ids = null, string? idQueryName = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("dimension", dimension);

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