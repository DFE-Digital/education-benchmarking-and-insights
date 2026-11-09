using System.Security.Claims;
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
    private readonly ClaimsPrincipal _user = new(new ClaimsIdentity());
    private readonly Mock<IExpenditureApi> _expenditureApi;
    private readonly Mock<IUserDataService> _userDataService;
    private readonly Mock<ISchoolComparatorSetService> _schoolComparatorSetService;
    private readonly ComparisonExpenditureService _service;

    public WhenComparisonExpenditureServiceIsCalled()
    {
        _expenditureApi = new Mock<IExpenditureApi>();
        _userDataService = new Mock<IUserDataService>();
        _schoolComparatorSetService = new Mock<ISchoolComparatorSetService>();

        _service = new ComparisonExpenditureService(
            _expenditureApi.Object,
            _userDataService.Object,
            _schoolComparatorSetService.Object);
    }

    [Fact]
    public async Task ShouldGetCustomExpendituresWhenCustomDataIdProvided()
    {
        const string customDataId = "custom-id";
        var customComparatorSet = new SchoolComparatorSet
        {
            Building = [Urn, "111111"],
            Pupil = [Urn, "222222"]
        };

        var customSchoolExpenditure = new SchoolExpenditure { URN = Urn };
        var buildingPeers = new[] { new SchoolExpenditure { URN = "111111" } };
        var pupilPeers = new[] { new SchoolExpenditure { URN = "222222" } };

        _schoolComparatorSetService
            .Setup(s => s.ReadComparatorSet(Urn, customDataId))
            .ReturnsAsync(customComparatorSet);

        _expenditureApi
            .Setup(e => e.SchoolCustom(Urn, customDataId, It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(customSchoolExpenditure));

        _expenditureApi
            .SetupSequence(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(buildingPeers))
            .ReturnsAsync(ApiResult.Ok(pupilPeers));

        var actual = await _service.GetExpendituresAsync(
            _user, Urn, SchoolSpendingDimensions.ResultAsOptions.Actuals, customDataId);

        Assert.NotNull(actual.BuildingExpenditure);
        Assert.Equal(2, actual.BuildingExpenditure.Length);
        Assert.Contains(actual.BuildingExpenditure, x => x.URN == "111111");
        Assert.Contains(actual.BuildingExpenditure, x => x.URN == Urn);

        Assert.NotNull(actual.PupilExpenditure);
        Assert.Equal(2, actual.PupilExpenditure.Length);
        Assert.Contains(actual.PupilExpenditure, x => x.URN == "222222");
        Assert.Contains(actual.PupilExpenditure, x => x.URN == Urn);

        _expenditureApi.Verify(e => e.SchoolCustom(Urn, customDataId, It.IsAny<ApiQuery>()), Times.Exactly(2));
        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ShouldGetDefaultExpendituresWhenNoUserComparatorSet()
    {
        _userDataService
            .Setup(u => u.GetSchoolDataAsync(_user, Urn, It.IsAny<CancellationToken>()))
            .ReturnsAsync(((string?)null, (string?)null));

        var defaultSet = new SchoolComparatorSet
        {
            Building = ["111111"],
            Pupil = ["222222"]
        };

        _schoolComparatorSetService
            .Setup(s => s.ReadComparatorSet(Urn))
            .ReturnsAsync(defaultSet);

        var buildingResults = new[] { new SchoolExpenditure { URN = "111111" } };
        var pupilResults = new[] { new SchoolExpenditure { URN = "222222" } };

        _expenditureApi
            .SetupSequence(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(buildingResults))
            .ReturnsAsync(ApiResult.Ok(pupilResults));

        var actual = await _service.GetExpendituresAsync(_user, Urn);

        Assert.Equal(buildingResults, actual.BuildingExpenditure);
        Assert.Equal(pupilResults, actual.PupilExpenditure);

        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ShouldGetUserDefinedExpendituresWhenUserComparatorSetExists()
    {
        const string comparatorSetId = "user-set-id";
        _userDataService
            .Setup(u => u.GetSchoolDataAsync(_user, Urn, It.IsAny<CancellationToken>()))
            .ReturnsAsync(((string?)null, comparatorSetId));

        var userDefinedSet = new UserDefinedSchoolComparatorSet
        {
            Set = ["333333", "444444"]
        };

        _schoolComparatorSetService
            .Setup(s => s.ReadUserDefinedComparatorSet(Urn, comparatorSetId))
            .ReturnsAsync(userDefinedSet);

        var expenditures = new[]
        {
            new SchoolExpenditure { URN = "333333" },
            new SchoolExpenditure { URN = "444444" }
        };

        _expenditureApi
            .Setup(e => e.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok(expenditures));

        var actual = await _service.GetExpendituresAsync(_user, Urn);

        Assert.Equal(expenditures, actual.BuildingExpenditure);
        Assert.Equal(expenditures, actual.PupilExpenditure);

        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Exactly(2));
    }

    [Fact]
    public async Task ShouldReturnEmptyArraysWhenComparatorSetsAreMissing()
    {
        _userDataService
            .Setup(u => u.GetSchoolDataAsync(_user, Urn, It.IsAny<CancellationToken>()))
            .ReturnsAsync(((string?)null, (string?)null));

        _schoolComparatorSetService
            .Setup(s => s.ReadComparatorSet(Urn))
            .ReturnsAsync((SchoolComparatorSet?)null);

        var actual = await _service.GetExpendituresAsync(_user, Urn);

        Assert.Empty(actual.BuildingExpenditure!);
        Assert.Empty(actual.PupilExpenditure!);
        _expenditureApi.Verify(e => e.QuerySchools(It.IsAny<ApiQuery>()), Times.Never);
    }
}
