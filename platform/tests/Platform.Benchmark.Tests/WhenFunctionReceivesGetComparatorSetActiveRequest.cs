using System.Net;
using Moq;
using Platform.Api.Benchmark.ComparatorSets;
using Platform.Api.Benchmark.UserData;
using Xunit;

namespace Platform.Benchmark.Tests;

public class WhenFunctionReceivesGetComparatorSetActiveRequest : ComparatorSetsFunctionsTestBase
{
    [Fact]
    public async Task UserDefinedShouldBeOkOnValidRequest()
    {
        const string urn = nameof(urn);
        const string userId = nameof(userId);
        const string identifier = nameof(identifier);

        var userData = new List<UserData>
        {
            new()
            {
                Id = identifier
            }
        };
        UserDataService
            .Setup(u => u.QueryAsync(new[]
            {
                userId
            }, "comparator-set", null, null, urn, "school", true))
            .ReturnsAsync(userData);

        ComparatorSetsService
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, "default"))
            .ReturnsAsync(new ComparatorSetUserDefinedSchool());

        var response =
            await Functions.UserDefinedSchoolComparatorSetActiveAsync(CreateHttpRequestData(), urn, userId);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedShouldBeNotFoundOnMissingUserData()
    {
        const string urn = nameof(urn);
        const string userId = nameof(userId);

        UserDataService
            .Setup(u => u.QueryAsync(new[]
            {
                userId
            }, "comparator-set", null, null, urn, "school", true))
            .ReturnsAsync(new List<UserData>());

        var response =
            await Functions.UserDefinedSchoolComparatorSetActiveAsync(CreateHttpRequestData(), urn, userId);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedShouldBeNotFoundOnMissingComparatorSet()
    {
        const string urn = nameof(urn);
        const string userId = nameof(userId);
        const string identifier = nameof(identifier);

        var userData = new List<UserData>
        {
            new()
            {
                Id = identifier
            }
        };
        UserDataService
            .Setup(u => u.QueryAsync(new[]
            {
                userId
            }, "comparator-set", null, null, urn, "school", true))
            .ReturnsAsync(userData);

        ComparatorSetsService
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, "default"));

        var response =
            await Functions.UserDefinedSchoolComparatorSetActiveAsync(CreateHttpRequestData(), urn, userId);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.NotFound, response.StatusCode);
    }

    [Fact]
    public async Task UserDefinedShouldBe500OnError()
    {
        const string urn = nameof(urn);
        const string userId = nameof(userId);
        const string identifier = nameof(identifier);

        var userData = new List<UserData>
        {
            new()
            {
                Id = identifier
            }
        };
        UserDataService
            .Setup(u => u.QueryAsync(new[]
            {
                userId
            }, "comparator-set", null, null, urn, "school", true))
            .ReturnsAsync(userData);

        ComparatorSetsService
            .Setup(d => d.UserDefinedSchoolAsync(urn, identifier, "default"))
            .Throws(new Exception());

        var response = await Functions
            .UserDefinedSchoolComparatorSetActiveAsync(CreateHttpRequestData(), urn, userId);

        Assert.NotNull(response);
        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}