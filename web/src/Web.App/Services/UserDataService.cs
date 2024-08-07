using System.Security.Claims;
using Web.App.Domain;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Benchmark;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface IUserDataService
{
    Task<UserData?> GetSchoolComparatorSetAsync(ClaimsPrincipal user, string identifier, string urn);
    Task<UserData?> GetCustomDataAsync(ClaimsPrincipal user, string identifier, string urn);
    Task<UserData?> GetTrustComparatorSetAsync(ClaimsPrincipal user, string identifier, string companyNumber);
    Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(ClaimsPrincipal user, string urn);
    Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(ClaimsPrincipal user, string companyNumber);
}

public class UserDataService(IUserDataApi api) : IUserDataService
{
    private const string OrganisationSchool = "school";
    private const string OrganisationTrust = "trust";
    private const string ComparatorSet = "comparator-set";
    private const string CustomData = "custom-data";

    public async Task<UserData?> GetSchoolComparatorSetAsync(ClaimsPrincipal user, string identifier, string urn)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }

        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("userId", user.UserId())
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetCustomDataAsync(ClaimsPrincipal user, string identifier, string urn)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }


        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("userId", user.UserId())
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", CustomData)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetTrustComparatorSetAsync(ClaimsPrincipal user, string identifier,
        string companyNumber)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return null;
        }


        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("userId", user.UserId())
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(ClaimsPrincipal user, string urn)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return (null, null);
        }


        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("userId", user.UserId())
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(ClaimsPrincipal user,
        string companyNumber)
    {
        if (user.Identity is not { IsAuthenticated: true })
        {
            return (null, null);
        }


        var query = new ApiQuery()
            .AddIfNotNull("userId", user.UserGuid().ToString())
            .AddIfNotNull("userId", user.UserId())
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }
}