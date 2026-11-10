using Moq;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Insight;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenComparisonExpenditureServiceIsCalled
{
    private const string Urn = "123456";
    private readonly Mock<IExpenditureApi> _expenditureApi;
    private readonly ComparisonExpenditureService _service;

    public WhenComparisonExpenditureServiceIsCalled()
    {
        _expenditureApi = new Mock<IExpenditureApi>();
        _service = new ComparisonExpenditureService(_expenditureApi.Object);
    }

    [Fact]
    public async Task ShouldGetCustomExpendituresWhenCustomDataIdProvided()
    {
        const string customDataId = "custom-id";
        string[] buildingUrns = [Urn, "111111"];
        string[] pupilUrns = [Urn, "222222"];

        var customSchoolExpenditure = new SchoolExpenditure { URN = Urn };
        var buildingPeers = new[] { new SchoolExpenditure { URN = "111111" } };
        var pupilPeers = new[] { new SchoolExpenditure { URN = "222222" } };

        _expenditureApi
            .Setup(e => e.SchoolCustom(Urn, customDataId, It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(customSchoolExpenditure));

        _expenditureApi
            .SetupSequence(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(buildingPeers))
            .ReturnsAsync(ApiResult.Ok(pupilPeers));

        var actual = await _service.GetExpendituresAsync(
            Urn, buildingUrns, pupilUrns, SchoolSpendingDimensions.ResultAsOptions.Actuals, customDataId);

        Assert.NotNull(actual.BuildingExpenditure);
        Assert.Equal(2, actual.BuildingExpenditure.Length);
        Assert.Contains(actual.BuildingExpenditure, x => x.URN == "111111");
        Assert.Contains(actual.BuildingExpenditure, x => x.URN == Urn);

        Assert.NotNull(actual.PupilExpenditure);
        Assert.Equal(2, actual.PupilExpenditure.Length);
        Assert.Contains(actual.PupilExpenditure, x => x.URN == "222222");
        Assert.Contains(actual.PupilExpenditure, x => x.URN == Urn);

        _expenditureApi.Verify(e => e.SchoolCustom(Urn, customDataId, It.IsAny<ApiQuery>()), Times.Once);
        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ShouldGetDistinctExpendituresWhenBuildingAndPupilSetsDiffer()
    {
        string[] buildingUrns = ["111111"];
        string[] pupilUrns = ["222222"];

        var buildingResults = new[] { new SchoolExpenditure { URN = "111111" } };
        var pupilResults = new[] { new SchoolExpenditure { URN = "222222" } };

        _expenditureApi
            .SetupSequence(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(buildingResults))
            .ReturnsAsync(ApiResult.Ok(pupilResults));

        var actual = await _service.GetExpendituresAsync(Urn, buildingUrns, pupilUrns);

        Assert.Equal(buildingResults, actual.BuildingExpenditure);
        Assert.Equal(pupilResults, actual.PupilExpenditure);

        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ShouldQueryOnceWhenBuildingAndPupilSetsAreEqual()
    {
        string[] sharedUrns = ["333333", "444444"];

        var expenditures = new[]
        {
            new SchoolExpenditure { URN = "333333" },
            new SchoolExpenditure { URN = "444444" }
        };

        _expenditureApi
            .Setup(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(expenditures));

        var actual = await _service.GetExpendituresAsync(Urn, sharedUrns, sharedUrns);

        Assert.Equal(expenditures, actual.BuildingExpenditure);
        Assert.Equal(expenditures, actual.PupilExpenditure);

        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Once);
    }

    [Fact]
    public async Task ShouldReturnEmptyArraysWhenUrnSetsAreNullOrEmpty()
    {
        var actual = await _service.GetExpendituresAsync(Urn, null, []);

        Assert.Empty(actual.BuildingExpenditure!);
        Assert.Empty(actual.PupilExpenditure!);
        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Never);
    }
}
