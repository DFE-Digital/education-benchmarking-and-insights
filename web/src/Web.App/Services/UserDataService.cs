using Web.App.Domain;
using Web.App.Domain.Benchmark;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
namespace Web.App.Services;

public interface IUserDataService
{
    Task<UserData?> GetSchoolComparatorSetAsync(string userId, string identifier, string urn);
    Task<UserData?> GetCustomDataAsync(string userId, string identifier, string urn);
    Task<UserData?> GetTrustComparatorSetAsync(string userId, string identifier, string companyNumber);
    Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(string userId, string urn);
    Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(string userId, string companyNumber);
}

public class UserDataService(IUserDataApi api) : IUserDataService
{
    private const string OrganisationSchool = "school";
    private const string OrganisationTrust = "trust";
    private const string ComparatorSet = "comparator-set";
    private const string CustomData = "custom-data";

    public async Task<UserData?> GetSchoolComparatorSetAsync(string userId, string identifier, string urn)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetCustomDataAsync(string userId, string identifier, string urn)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", CustomData)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetTrustComparatorSetAsync(string userId, string identifier, string companyNumber)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", ComparatorSet)
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetSchoolDataAsync(string userId, string urn)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("organisationType", OrganisationSchool)
            .AddIfNotNull("organisationId", urn);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }

    public async Task<(string? CustomData, string? ComparatorSet)> GetTrustDataAsync(string userId, string companyNumber)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("organisationType", OrganisationTrust)
            .AddIfNotNull("organisationId", companyNumber);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == ComparatorSet)?.Id);
    }
}