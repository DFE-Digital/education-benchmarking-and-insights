﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
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
    /// <param name="excludeCentralServices"></param>
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
        [FromQuery] bool? excludeCentralServices,
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
                        return await CustomSchoolExpenditure(id, category, dimension, customDataId);
                    case OrganisationTypes.School:
                        return await SchoolExpenditure(id, category, dimension, excludeCentralServices);
                    case OrganisationTypes.Trust:
                        return await TrustExpenditure(id, phase, category, dimension, excludeCentralServices);
                    case OrganisationTypes.LocalAuthority:
                        return await LocalAuthorityExpenditure(id, phase, category, dimension, excludeCentralServices);
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

    /// <param name="type" example="school"></param>
    /// <param name="id" example="140565"></param>
    /// <param name="dimension" example="PerUnit"></param>
    /// <param name="excludeCentralServices"></param>
    [HttpGet]
    [Produces("application/json")]
    [ProducesResponseType<ExpenditureHistory[]>(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    [Route("history")]
    public async Task<IActionResult> History(
        [FromQuery] string type,
        [FromQuery] string id,
        [FromQuery] string dimension,
        [FromQuery] bool? excludeCentralServices)
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
                    .AddIfNotNull("dimension", dimension)
                    .AddIfNotNull("excludeCentralServices", excludeCentralServices);

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
                    OrganisationTypes.Trust => await TrustExpenditureUserDefined(id, null, category, dimension, excludeCentralServices),
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

    private async Task<IActionResult> LocalAuthorityExpenditure(string laCode, string? phase, string? category, string? dimension, bool? excludeCentralServices)
    {
        var la = await establishmentApi.GetLocalAuthority(laCode).GetResultOrThrow<LocalAuthority>();
        var query = BuildQuery(category, dimension, excludeCentralServices, phase);
        query.AddIfNotNull("laCode", la.Code);
        var result = await expenditureApi
            .QuerySchools(query)
            .GetResultOrThrow<SchoolExpenditure[]>();
        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditure(string companyNumber, string? phase, string? category, string? dimension, bool? excludeCentralServices)
    {
        var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
        var query = BuildQuery(category, dimension, excludeCentralServices, phase);
        query.AddIfNotNull("companyNumber", trust.CompanyNumber);
        var result = await expenditureApi
            .QuerySchools(query)
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(result);
    }

    private async Task<IActionResult> TrustExpenditureUserDefined(string id, string? phase, string? category, string? dimension, bool? excludeCentralServices)
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

    private async Task<IActionResult> CustomSchoolExpenditure(string id, string category, string dimension, string customDataId)
    {
        var customSet = await schoolComparatorSetService.ReadComparatorSet(id, customDataId);
        var set = category is "PremisesStaffServices" or "Utilities"
            ? customSet?.Building
            : customSet?.Pupil;

        if (set == null || set.Length == 0)
        {
            return new JsonResult(Array.Empty<SchoolExpenditure>());
        }

        var schools = set.Where(x => x != id).ToArray();
        var customResult = await expenditureApi
            .SchoolCustom(id, customDataId, BuildQuery(category, dimension))
            .GetResultOrDefault<SchoolExpenditure>();

        var defaultResult = await expenditureApi
            .QuerySchools(BuildQuery(schools, "urns", category, dimension))
            .GetResultOrDefault<SchoolExpenditure[]>();

        return customResult != null
            ? new JsonResult(defaultResult?.Append(customResult).ToArray())
            : new JsonResult(defaultResult);
    }

    private async Task<IActionResult> SchoolExpenditure(string id, string? category, string? dimension, bool? excludeCentralServices)
    {
        var userData = await userDataService.GetSchoolDataAsync(User, id);
        if (string.IsNullOrEmpty(userData.ComparatorSet))
        {
            var defaultSet = await schoolComparatorSetService.ReadComparatorSet(id);
            var set = category is "PremisesStaffServices" or "Utilities"
                ? defaultSet?.Building
                : defaultSet?.Pupil;

            if (set == null || set.Length == 0)
            {
                return new JsonResult(Array.Empty<SchoolExpenditure>());
            }

            var defaultResult = await expenditureApi
                .QuerySchools(BuildQuery(set, "urns", category, dimension, excludeCentralServices))
                .GetResultOrThrow<SchoolExpenditure[]>();

            return new JsonResult(defaultResult);
        }

        var userDefinedSet = await schoolComparatorSetService.ReadUserDefinedComparatorSet(id, userData.ComparatorSet);
        if (userDefinedSet == null || userDefinedSet.Set.Length == 0)
        {
            return new JsonResult(Array.Empty<SchoolExpenditure>());
        }


        var userDefinedResult = await expenditureApi
            .QuerySchools(BuildQuery(userDefinedSet.Set, "urns", category, dimension, excludeCentralServices))
            .GetResultOrThrow<SchoolExpenditure[]>();

        return new JsonResult(userDefinedResult);
    }

    private static ApiQuery BuildQuery(string? category, string? dimension, bool? excludeCentralServices = null, string? phase = null)
    {
        var query = new ApiQuery()
            .AddIfNotNull("category", category)
            .AddIfNotNull("dimension", dimension)
            .AddIfNotNull("excludeCentralServices", excludeCentralServices)
            .AddIfNotNull("phase", phase);

        return query;
    }

    private static ApiQuery BuildQuery(IEnumerable<string> ids, string idQueryName, string? category, string? dimension, bool? excludeCentralServices = null, string? phase = null)
    {
        var query = BuildQuery(category, dimension, excludeCentralServices, phase);

        foreach (var id in ids)
        {
            query.AddIfNotNull(idQueryName, id);
        }

        return query;
    }
}