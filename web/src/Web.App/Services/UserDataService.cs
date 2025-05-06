using System.Security.Claims;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IUserDataService
{
    Task<UserData?> GetSchoolComparatorSetActiveAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default);
    Task<UserData?> GetCustomDataActiveAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default);
    Task<UserData?> GetTrustComparatorSetAsync(ClaimsPrincipal user, string companyNumber, CancellationToken cancellationToken = default);
    Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default);
    Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(ClaimsPrincipal user, string companyNumber, CancellationToken cancellationToken = default);
}

public class UserDataService(IUserDataApi api, ILogger<UserDataService> logger) : IUserDataService
{
    private const string OrganisationSchool = "school";
    private const string OrganisationTrust = "trust";
    private const string ComparatorSet = "comparator-set";
    private const string CustomData = "custom-data";

    public async Task<UserData?> GetSchoolComparatorSetActiveAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query, cancellationToken).GetResultOrDefault<UserData[]>();
        if (userSets?.Length > 1)
        {
            logger.LogWarning(
                "Unexpected {Length} active {Type} UserData rows returned for {OrganisationType} {OrganisationId} for user {userId}",
                userSets.Length,
                ComparatorSet,
                OrganisationSchool,
                urn,
                user.UserGuid());
        }

        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetCustomDataActiveAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("type", CustomData)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query, cancellationToken).GetResultOrDefault<UserData[]>();
        if (userSets?.Length > 1)
        {
            logger.LogWarning(
                "Unexpected {Length} active {Type} UserData rows returned for {OrganisationType} {OrganisationId} for user {userId}",
                userSets.Length,
                CustomData,
                OrganisationSchool,
                urn,
                user.UserGuid());
        }

        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetTrustComparatorSetAsync(ClaimsPrincipal user, string companyNumber, CancellationToken cancellationToken = default)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query, cancellationToken).GetResultOrDefault<UserData[]>();
        if (userSets?.Length > 1)
        {
            logger.LogWarning(
                "Unexpected {Length} active {Type} UserData rows returned for {OrganisationType} {OrganisationId} for user {userId}",
                userSets.Length,
                ComparatorSet,
                OrganisationTrust,
                companyNumber,
                user.UserGuid());
        }

        return userSets?.FirstOrDefault();
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(ClaimsPrincipal user, string urn, CancellationToken cancellationToken = default)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return (null, null);
        }


        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query, cancellationToken).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(ClaimsPrincipal user,
        string companyNumber, CancellationToken cancellationToken = default)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return (null, null);
        }

        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query, cancellationToken).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }
}