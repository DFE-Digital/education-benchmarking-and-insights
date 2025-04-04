using AutoFixture;
using Moq;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenSchoolSuggestionsIsCalled
{
    private readonly Mock<IEstablishmentApi> _api = new();
    private readonly Fixture _fixture = new();

    [Fact]
    public async Task ShouldDisplayUrnOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.URN,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.URN})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplaySchoolNameOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.SchoolName,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressStreetOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressStreet,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressStreet}, {school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressLocalityOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressLocality,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressLocality}, {school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressLine3OnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressLine3,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressLine3}, {school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressTownOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressTown,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressCountyOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressCounty,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressTown}, {school.AddressCounty}, {school.AddressPostcode})", array.First().Text);
    }

    [Fact]
    public async Task ShouldDisplayAddressPostcodeOnMatch()
    {
        var school = _fixture.Create<School>();
        var value = new SuggestValue<School>
        {
            Text = school.AddressPostcode,
            Document = school
        };

        var response = new SuggestOutput<School>
        {
            Results = [value]
        };

        _api.Setup(x => x.SuggestSchools(It.IsAny<string>(), It.IsAny<string[]?>(), It.IsAny<bool?>())).ReturnsAsync(ApiResult.Ok(response));

        var service = new SuggestService(_api.Object);
        var actual = await service.SchoolSuggestions("school");
        var array = actual as SuggestValue<School>[] ?? actual.ToArray();

        Assert.Single(array);
        Assert.Equal($"{school.SchoolName} ({school.AddressTown}, {school.AddressPostcode})", array.First().Text);
    }
}