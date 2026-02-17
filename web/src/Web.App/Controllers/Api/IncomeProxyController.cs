using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Attributes;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Controllers.Api;

[ApiController]
[Route("api/income")]
public class IncomeProxyController(ILogger<IncomeProxyController> logger, ISchoolApi schoolApi, ITrustApi trustApi) : Controller
{
    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<IncomeHistoryRows>(StatusCodes.Status200OK)]
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
                var query = new ApiQuery()
                    .AddIfNotNull("dimension", dimension);

                var result = type.ToLower() switch
                {
                    OrganisationTypes.School => await schoolApi.QueryIncomeHistoryAsync(id, query, cancellationToken).GetResultOrDefault<IncomeHistoryRows>(),
                    OrganisationTypes.Trust => await trustApi.QueryIncomeHistoryAsync(id, query, cancellationToken).GetResultOrDefault<IncomeHistoryRows>(),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };

                return new JsonResult(result);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting income history data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="phase" example="Secondary"></param>
    /// <param name="financeType" example="Academy"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<HistoryComparison<IncomeHistory>>(StatusCodes.Status200OK)]
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
                logger.LogError(e, "An error getting income history trends data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private static ApiQuery BuildQuery(
        string? category,
        string? dimension,
        string? phase = null,
        string? financeType = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("financeType", financeType)
            .AddIfNotNull("phase", phase);

        return query;
    }

    private async Task<IncomeHistoryRows?> SchoolHistory(string urn, string dimension, CancellationToken cancellationToken = default) => await schoolApi
        .QueryIncomeHistoryAsync(urn, BuildQuery(null, dimension), cancellationToken)
        .GetResultOrDefault<IncomeHistoryRows>();

    private async Task<IncomeHistoryRows?> SchoolHistoryComparatorSetAverage(string urn, string dimension, CancellationToken cancellationToken = default) => await schoolApi
        .QueryIncomeComparatorSetAverageHistoryAsync(urn, BuildQuery(null, dimension), cancellationToken)
        .GetResultOrDefault<IncomeHistoryRows>();

    private async Task<IncomeHistoryRows?> SchoolHistoryNationalAverage(string urn, string dimension, CancellationToken cancellationToken = default)
    {
        var school = await schoolApi.SingleAsync(urn, cancellationToken).GetResultOrThrow<School>();
        return await SchoolHistoryNationalAverage(school.OverallPhase, school.FinanceType, dimension, cancellationToken);
    }

    private async Task<IncomeHistoryRows?> SchoolHistoryNationalAverage(string? phase, string? financeType, string dimension, CancellationToken cancellationToken = default)
    {
        var query = BuildQuery(null, dimension, phase, financeType: financeType);
        return await schoolApi
            .QueryIncomeNationalAverageHistoryAsync(query, cancellationToken)
            .GetResultOrDefault<IncomeHistoryRows>();
    }
    private async Task<HistoryComparison<IncomeHistory>?> GetSchoolHistoryComparison(string urn, string dimension, string? phase, string? financeType, CancellationToken cancellationToken = default)
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

        return new HistoryComparison<IncomeHistory>
        {
            StartYear = school.StartYear,
            EndYear = school.EndYear,
            School = school.Rows.ToArray(),
            ComparatorSetAverage = comparatorSetAverage?.Rows.ToArray(),
            NationalAverage = nationalAverage?.Rows.ToArray()
        };
    }
}