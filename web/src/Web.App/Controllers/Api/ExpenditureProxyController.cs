using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.ActionResults;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Infrastructure.Apis.Insight;
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
    ITrustComparatorSetService trustComparatorSetService,
    IUserDataService userDataService) : Controller
{
    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="category" example="TotalExpenditure"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="phase"></param>
    /// <param name="customDataId"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<SchoolExpenditure[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async Task<IActionResult> Query(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string category,
        [FromQuery] string dimension,
        [FromQuery] string? phase,
        [FromQuery] string? customDataId)
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
                    case OrganisationTypes.School when customDataId is not null:
                        return await CustomSchoolExpenditureResult(id, category, dimension, customDataId);
                    case OrganisationTypes.School:
                        return await SchoolExpenditureResult(id, category, dimension);
                    case OrganisationTypes.Trust:
                        return await TrustExpenditureResult(id, phase, category, dimension);
                    case OrganisationTypes.LocalAuthority:
                        return await LocalAuthorityExpenditureResult(id, phase, category, dimension);
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
    [Produces("application/zip")]
    [ProducesResponseType<byte[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("download")]
    public async Task<IActionResult> Download([FromQuery] string type, [FromQuery] string id, [FromQuery] string? customDataId)
    {
        using (logger.BeginScope(new
        {
            type,
            id
        }))
        {
            try
            {
                SchoolExpenditure[]? buildingResult;
                SchoolExpenditure[]? pupilResult;
                const string dimension = "Actuals";
                switch (type.ToLower())
                {
                    case OrganisationTypes.School when customDataId is not null:
                        buildingResult = await GetCustomSchoolExpenditure(id, true, null, dimension, customDataId);
                        pupilResult = await GetCustomSchoolExpenditure(id, false, null, dimension, customDataId);
                        break;
                    case OrganisationTypes.School:
                        buildingResult = await GetDefaultSchoolExpenditure(id, true, null, dimension);
                        pupilResult = await GetDefaultSchoolExpenditure(id, false, null, dimension);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException(nameof(type));
                }

                IEnumerable<CsvResult> csvResults =
                [
                    new(buildingResult, $"expenditure-{id}-building.csv"),
                    new(pupilResult, $"expenditure-{id}-pupil.csv")
                ];
                return new CsvResults(csvResults, $"expenditure-{id}.zip");
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error downloading expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<ExpenditureHistoryRows>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension)
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
                    OrganisationTypes.School => await SchoolExpenditureHistory(id, dimension, CancellationToken.None),
                    OrganisationTypes.Trust => await TrustExpenditureHistory(id, dimension),
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

    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="phase" example="Secondary"></param>
    /// <param name="financeType" example="Academy"></param>
    /// <param name="cancellationToken"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<HistoryComparison<ExpenditureHistory>>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history/comparison")]
    public async Task<IActionResult> HistoryComparison(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] string? phase,
        [FromQuery] string? financeType,
        CancellationToken cancellationToken)
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
                logger.LogError(e, "An error getting expenditure history trends data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    /// <param name="type" example="trust"></param>
    /// <param name="id" example="07465701"></param>
    /// <param name="category" example="TotalExpenditure"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="excludeCentralServices"></param>
    [Route("user-defined")]
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<TrustExpenditure[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Authorize]
    public async Task<IActionResult> UserDefined(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string category,
        [FromQuery] string dimension,
        [FromQuery] bool? excludeCentralServices)
    {
        using (logger.BeginScope(new
        {
            id
        }))
        {
            try
            {
                return type.ToLower() switch
                {
                    OrganisationTypes.Trust => await TrustExpenditureUserDefinedResult(id, null, category, dimension, excludeCentralServices),
                    _ => throw new ArgumentOutOfRangeException(nameof(type))
                };
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error getting user-defined expenditure data: {DisplayUrl}", Request.GetDisplayUrl());
                return StatusCode(500);
            }
        }
    }

    private async Task<IActionResult> LocalAuthorityExpenditureResult(string laCode, string? phase, string? category, string? dimension)
    {
        var la = await establishmentApi.GetLocalAuthority(laCode).GetResultOrThrow<LocalAuthority>();
        var query = BuildQuery(category, dimension, phase);
        query.AddIfNotNull("laCode", la.Code);
        var result = await expenditureApi
            .QuerySchools(query)
            .GetResultOrThrow<SchoolExpenditure[]>();
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditureResult(string companyNumber, string? phase, string? category, string? dimension)
    {
        var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
        var query = BuildQuery(category, dimension, phase);
        query.AddIfNotNull("companyNumber", trust.CompanyNumber);
        var result = await expenditureApi
            .QuerySchools(query)
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditureUserDefinedResult(string id, string? phase, string? category, string? dimension, bool? excludeCentralServices)
    {
        var userData = await userDataService.GetTrustDataAsync(User, id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            return new NotFoundResult();
        }

        var userDefinedSet = await trustComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        var userDefinedResult = await expenditureApi
            .QueryTrusts(BuildQuery(userDefinedSet.Set, "companyNumbers", category, dimension, excludeCentralServices, phase))
            .GetResultOrThrow<TrustExpenditure[]>();

        return new JsonResult(userDefinedResult);
    }

    private async Task<IActionResult> CustomSchoolExpenditureResult(string id, string category, string dimension, string customDataId)
    {
        var result = await GetCustomSchoolExpenditure(id, IsBuildingSet(category), category, dimension, customDataId);
        return new JsonResult(result);
    }

    private async Task<SchoolExpenditure[]?> GetCustomSchoolExpenditure(string id, bool useBuildingSet, string? category, string? dimension, string customDataId)
    {
        var customSet = await schoolComparatorSetService.ReadComparatorSet(id, customDataId);
        var set = useBuildingSet
            ? customSet?.Building
            : customSet?.Pupil;

        if (set == null || set.Length == 0)
        {
            return [];
        }

        var schools = set.Where(x => x != id).ToArray();
        var customResult = await expenditureApi
            .SchoolCustom(id, customDataId, BuildQuery(category, dimension))
            .GetResultOrDefault<SchoolExpenditure>();

        var defaultResult = await expenditureApi
            .QuerySchools(BuildQuery(schools, "urns", category, dimension))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null
            ? defaultResult?.Append(customResult).ToArray()
            : defaultResult;
    }

    private async Task<IActionResult> SchoolExpenditureResult(string id, string? category, string? dimension)
    {
        var result = await GetDefaultSchoolExpenditure(id, IsBuildingSet(category), category, dimension);
        return new JsonResult(result);
    }

    private async Task<SchoolExpenditure[]> GetDefaultSchoolExpenditure(string id, bool useBuildingSet, string? category, string? dimension)
    {
        var userData = await userDataService.GetSchoolDataAsync(User, id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            var set = useBuildingSet
                ? defaultSet?.Building
                : defaultSet?.Pupil;

            if (set == null || set.Length == 0)
            {
                return [];
            }

            var defaultResult = await expenditureApi
                .QuerySchools(BuildQuery(set, "urns", category, dimension))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return defaultResult;
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
        {
            return [];
        }

        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildQuery(userDefinedSet.Set, "urns", category, dimension))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return userDefinedResult;
    }

    private static bool IsBuildingSet(string? category) => category is "PremisesStaffServices" or "Utilities";

    private static ApiQuery BuildQuery(
        string? category,
        string? dimension,
        string? phase = null,
        bool? excludeCentralServices = null,
        string? financeType = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("excludeCentralServices", excludeCentralServices)
            .AddIfNotNull("financeType", financeType)
            .AddIfNotNull("phase", phase);

        return query;
    }

    private static ApiQuery BuildQuery(
        IEnumerable<string> ids,
        string idQueryName,
        string? category,
        string? dimension,
        bool? excludeCentralServices = null,
        string? phase = null)
    {
        var query = BuildQuery(category, dimension, phase, excludeCentralServices);

        foreach (var id in ids)
        {
            query.AddIfNotNull(idQueryName, id);
        }

        return query;
    }

    private async Task<ExpenditureHistoryRows?> SchoolExpenditureHistory(string urn, string dimension, CancellationToken cancellationToken) => await expenditureApi
        .SchoolHistory(urn, BuildQuery(null, dimension), cancellationToken)
        .GetResultOrDefault<ExpenditureHistoryRows>();

    private async Task<ExpenditureHistoryRows?> TrustExpenditureHistory(string companyNumber, string dimension) => await expenditureApi
        .TrustHistory(companyNumber, BuildQuery(null, dimension))
        .GetResultOrDefault<ExpenditureHistoryRows>();

    private async Task<ExpenditureHistoryRows?> SchoolExpenditureHistoryComparatorSetAverage(string urn, string dimension, CancellationToken cancellationToken) => await expenditureApi
        .SchoolHistoryComparatorSetAverage(urn, BuildQuery(null, dimension), cancellationToken)
        .GetResultOrDefault<ExpenditureHistoryRows>();

    private async Task<ExpenditureHistoryRows?> SchoolExpenditureHistoryNationalAverage(string urn, string dimension, CancellationToken cancellationToken)
    {
        var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        return await SchoolExpenditureHistoryNationalAverage(school.OverallPhase, school.FinanceType, dimension, cancellationToken);
    }

    private async Task<ExpenditureHistoryRows?> SchoolExpenditureHistoryNationalAverage(string? phase, string? financeType, string dimension, CancellationToken cancellationToken)
    {
        var query = BuildQuery(null, dimension, phase, financeType: financeType);
        return await expenditureApi
            .SchoolHistoryNationalAverage(query, cancellationToken)
            .GetResultOrDefault<ExpenditureHistoryRows>();
    }

    private async Task<HistoryComparison<ExpenditureHistory>?> GetSchoolHistoryComparison(string urn, string dimension, string? phase, string? financeType, CancellationToken cancellationToken)
    {
        var school = await SchoolExpenditureHistory(urn, dimension, cancellationToken);
        if (school == null)
        {
            return null;
        }

        var comparatorSetAverage = await SchoolExpenditureHistoryComparatorSetAverage(urn, dimension, cancellationToken);
        var nationalAverage = string.IsNullOrWhiteSpace(phase) || string.IsNullOrWhiteSpace(financeType)
            ? await SchoolExpenditureHistoryNationalAverage(urn, dimension, cancellationToken)
            : await SchoolExpenditureHistoryNationalAverage(phase, financeType, dimension, cancellationToken);

        return new HistoryComparison<ExpenditureHistory>
        {
            StartYear = school.StartYear,
            EndYear = school.EndYear,
            School = school.Rows.ToArray(),
            ComparatorSetAverage = comparatorSetAverage?.Rows.ToArray(),
            NationalAverage = nationalAverage?.Rows.ToArray()
        };
    }
}