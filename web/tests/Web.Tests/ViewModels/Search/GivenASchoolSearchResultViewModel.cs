using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels.Search;
using Xunit;

namespace Web.Tests.ViewModels.Search;

public class GivenASchoolSearchResultViewModel
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void WhenCreateShouldMapFacetsIfSupplied()
    {
        var addressStreet = _fixture.Create<string>();
        var school = _fixture
            .Build<SchoolSummary>()
            .With(s => s.SchoolName, "SchoolName")
            .With(s => s.AddressStreet, $"SchoolName, {addressStreet}")
            .With(s => s.AddressLocality, "AddressLocality")
            .With(s => s.AddressLine3, "AddressLocality")
            .Create();

        var actual = SchoolSearchResultViewModel.Create(school);

        Assert.Equal(school.URN, actual.URN);
        Assert.Equal(school.SchoolName, actual.SchoolName);
        Assert.Equal(school.AddressStreet, actual.AddressStreet);
        Assert.Equal(school.AddressLocality, actual.AddressLocality);
        Assert.Equal(school.AddressLine3, actual.AddressLine3);
        Assert.Equal(school.AddressTown, actual.AddressTown);
        Assert.Equal(school.AddressCounty, actual.AddressCounty);
        Assert.Equal(school.AddressPostcode, actual.AddressPostcode);
        Assert.Equal($"{addressStreet}, {school.AddressLocality}, {school.AddressTown}, {school.AddressCounty}, {school.AddressPostcode}", actual.Address);
        Assert.Equal(school.OverallPhase, actual.OverallPhase);
        Assert.Equal(school.PeriodCoveredByReturn, actual.PeriodCoveredByReturn);
        Assert.Equal(school.TotalPupils, actual.TotalPupils);
    }
}