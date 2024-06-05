using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;

namespace Web.App.Services;

public interface IUserDataService
{
    Task<UserData?> GetSchoolComparatorSetAsync(string userId, string identifier);
    Task<UserData?> GetCustomDataAsync(string userId, string identifier);
    Task<UserData?> GetTrustComparatorSetAsync(string userId, string identifier);
    Task<(string? TrustComparatorSet, string? CustomData, string? SchoolComparatorSet)> GetAsync(string userId);
}

public class UserDataService(IUserDataApi api) : IUserDataService
{
    private const string SchoolComparatorSet = "school-comparator-set";
    private const string TrustComparatorSet = "trust-comparator-set";
    private const string CustomData = "custom-data";

    public async Task<UserData?> GetSchoolComparatorSetAsync(string userId, string identifier)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", SchoolComparatorSet);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetCustomDataAsync(string userId, string identifier)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", CustomData);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<UserData?> GetTrustComparatorSetAsync(string userId, string identifier)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId)
            .AddIfNotNull("id", identifier)
            .AddIfNotNull("type", TrustComparatorSet);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return userSets?.FirstOrDefault();
    }

    public async Task<(string? TrustComparatorSet, string? CustomData, string? SchoolComparatorSet)> GetAsync(
        string userId)
    {
        var query = new ApiQuery()
            .AddIfNotNull("userId", userId);

        var userSets = await api.GetAsync(query).GetResultOrDefault<UserData[]>();
        return (userSets?.FirstOrDefault(x => x.Type == TrustComparatorSet)?.Id,
            userSets?.FirstOrDefault(x => x.Type == CustomData)?.Id,
            userSets?.FirstOrDefault(x => x.Type == SchoolComparatorSet)?.Id);
    }
}