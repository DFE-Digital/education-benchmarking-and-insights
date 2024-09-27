using AutoFixture;
using Platform.Api.Establishment.LocalAuthorities;
using Platform.Api.Establishment.Schools;
using Xunit;
namespace Platform.Tests.Establishment;

public class WhenLocalAuthorityResponseFactoryCreatesResponse
{
    private readonly LocalAuthority _localAuthority;
    private readonly IEnumerable<School> _schools;

    public WhenLocalAuthorityResponseFactoryCreatesResponse()
    {
        var fixture = new Fixture();
        _localAuthority = fixture.Create<LocalAuthority>();
        _schools = fixture.Build<School>().CreateMany(5);
    }

    [Fact]
    public void ShouldBuildResponseModelWithMappedLocalAuthorityProperties()
    {
        var response = LocalAuthorityResponseFactory.Create(_localAuthority, _schools);

        Assert.Equal(_localAuthority.Code, response.Code);
        Assert.Equal(_localAuthority.Name, response.Name);
    }

    [Fact]
    public void ShouldBuildResponseModelWithMappedLocalSchoolProperties()
    {
        var response = LocalAuthorityResponseFactory.Create(_localAuthority, _schools);

        Assert.Equal(_schools.Count(), response.Schools.Length);
        for (var i = 0; i < _schools.Count(); i++)
        {
            var school = response.Schools.ElementAtOrDefault(i);
            Assert.NotNull(school);
            Assert.Equal(_schools.ElementAt(i).URN, school.URN);
            Assert.Equal(_schools.ElementAt(i).SchoolName, school.SchoolName);
            Assert.Equal(_schools.ElementAt(i).OverallPhase, school.OverallPhase);
        }
    }
}