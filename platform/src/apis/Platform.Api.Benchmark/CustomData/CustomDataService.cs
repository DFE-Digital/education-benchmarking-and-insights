using System;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Infrastructure.Sql;

namespace Platform.Api.Benchmark.CustomData;

public interface ICustomDataService
{
    Task UpsertCustomDataAsync(CustomDataSchool data);
    Task<CustomDataSchool?> CustomDataSchoolAsync(string urn, string identifier);
    Task UpsertUserDataAsync(CustomDataUserData userData);
    Task<string> CurrentYearAsync();
    Task DeleteSchoolAsync(CustomDataSchool data);
}

[ExcludeFromCodeCoverage]
public class CustomDataService : ICustomDataService
{
    private readonly IDatabaseFactory _dbFactory;

    public CustomDataService(IDatabaseFactory dbFactory)
    {
        _dbFactory = dbFactory;
    }


    public Task UpsertCustomDataAsync(CustomDataSchool data)
    {
        throw new System.NotImplementedException();
    }
    public Task<CustomDataSchool?> CustomDataSchoolAsync(string urn, string identifier)
    {
        throw new System.NotImplementedException();
    }

    public Task UpsertUserDataAsync(CustomDataUserData userData)
    {
        throw new System.NotImplementedException();
    }

    public Task<string> CurrentYearAsync()
    {
        throw new NotImplementedException();
    }

    public Task DeleteSchoolAsync(CustomDataSchool data)
    {
        throw new NotImplementedException();
    }
}