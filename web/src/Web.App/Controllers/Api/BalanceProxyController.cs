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
    ISchoolApi schoolApi,
    ITrustApi trustApi,
    IBalanceApi balanceApi,
    IUserDataService userDataService,
    ITrustComparatorSetService trustComparatorSetService) : Controller
{

    #region history

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
                    OrganisationTypes.School => await schoolApi
                        .QueryBalanceHistoryAsync(id, BuildQuery(dimension), cancellationToken)
                        .GetResultOrDefault<BalanceHistoryRows>(),
                    OrganisationTypes.Trust => await trustApi
                        .QueryBalanceHistoryAsync(id, BuildQuery(dimension), cancellationToken)
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

    #endregion

    #region history/comparison

    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="phase" example="Secondary"></param>
    /// <param name="financeType" example="Academy"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<HistoryComparison<BalanceHistory>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [Route("history/comparison")]
    [ValidateId]
    public async Task<IActionResult> HistoryComparison(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] string? phase,
        [FromQuery] string? financeType,
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
                    OrganisationTypes.School => await GetSchoolHistoryComparison(id, dimension, phase, financeType, cancellationToken),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                if (result == null)
                {
                    return new NotFoundResult();
                }

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting balance history trends data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<HistoryComparison<BalanceHistory>?> GetSchoolHistoryComparison(string urn, string dimension, string? phase, string? financeType, CancellationToken cancellationToken = default)
    {
        var school = await SchoolHistory(urn, dimension, cancellationToken);
        if (school == null)
        {
            return null;
        }

        var comparatorSetAverage = await SchoolHistoryComparatorSetAverage(urn, dimension, cancellationToken);
        var nationalAverage = string.IsNullOrWhiteSpace(phase) || string.IsNullOrWhiteSpace(financeType)
            ? await SchoolHistoryNationalAverage(urn, dimension, cancellationToken)
            : await SchoolHistoryNationalAverage(phase, financeType, dimension, cancellationToken);

        return new HistoryComparison<BalanceHistory>
        {
            StartYear = school.StartYear,
            EndYear = school.EndYear,
            School = school.Rows.ToArray(),
            ComparatorSetAverage = comparatorSetAverage?.Rows.ToArray(),
            NationalAverage = nationalAverage?.Rows.ToArray()
        };
    }

    private async Task<BalanceHistoryRows?> SchoolHistory(string urn, string dimension, CancellationToken cancellationToken = default) => await schoolApi
        .QueryBalanceHistoryAsync(urn, BuildQuery(dimension), cancellationToken)
        .GetResultOrDefault<BalanceHistoryRows>();

    private async Task<BalanceHistoryRows?> SchoolHistoryComparatorSetAverage(string urn, string dimension, CancellationToken cancellationToken = default) => await schoolApi
        .QueryBalanceComparatorSetAverageHistoryAsync(urn, BuildQuery(dimension), cancellationToken)
        .GetResultOrDefault<BalanceHistoryRows>();

    private async Task<BalanceHistoryRows?> SchoolHistoryNationalAverage(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        var school = await schoolApi.SingleAsync(urn, cancellationToken).GetResultOrThrow<School>();
        return await SchoolHistoryNationalAverage(school.OverallPhase, school.FinanceType, dimension, cancellationToken);
    }

    private async Task<BalanceHistoryRows?> SchoolHistoryNationalAverage(string? phase, string? financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(dimension, phase: phase, financeType: financeType);
        return await schoolApi
            .QueryBalanceNationalAverageHistoryAsync(query, cancellationToken)
            .GetResultOrDefault<BalanceHistoryRows>();
    }

    #endregion

    #region user-defined

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
        var userDefinedResult = await balanceApi
            .QueryTrusts(BuildQuery(dimension, userDefinedSet.Set, "companyNumbers"), cancellationToken)
            .GetResultOrThrow<TrustBalance[]>();

        return new JsonResult(userDefinedResult);
    }

    #endregion

    #region Utilities

    private static ApiQuery BuildQuery(
        string? dimension,
        IEnumerable<string>? ids = null,
        string? idQueryName = null,
        string? category = null,
        string? phase = null,
        string? financeType = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("financeType", financeType)
            .AddIfNotNull("phase", phase);

        if (ids != null && !string.IsNullOrWhiteSpace(idQueryName))
        {
            foreach (var id in ids)
            {
                query.AddIfNotNull(idQueryName, id);
            }
        }

        return query;
    }

    #endregion
}