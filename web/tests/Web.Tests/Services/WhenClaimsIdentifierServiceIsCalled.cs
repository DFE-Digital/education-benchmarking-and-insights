using Moq;
using Web.App.Domain;
using Web.App.Identity.Models;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Services;
using Xunit;

namespace Web.Tests.Services;

public class WhenClaimsIdentifierServiceIsCalled
{
    private readonly Mock<IEstablishmentApi> _mockApi = new();

    [Fact]
    public async Task ShouldReturnSchoolsAndTrustCorrectlyWhenSingleAcademyTrust()
    {
        var organisationItem = new OrganisationItem
        {
            Id = "013"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            CompanyRegistrationNumber = 12345678
        };
        var response = new Trust
        {
            Schools = [new TrustSchool { URN = "123456" }]
        };

        _mockApi.Setup(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Single(schools);
        Assert.Contains("123456", schools);
        Assert.Single(trusts);
        Assert.Contains("12345678", trusts);
    }

    [Fact]
    public async Task ShouldReturnTrustSchoolsCorrectlyWhenMultiAcademyTrust()
    {
        var organisationItem = new OrganisationItem
        {
            Id = "010"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            CompanyRegistrationNumber = 12345678
        };
        var response = new Trust
        {
            Schools =
            [
                new TrustSchool { URN = "123456" },
                new TrustSchool { URN = "987654" }
            ]
        };

        _mockApi.Setup(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Equal(2, schools.Length);
        Assert.Contains("123456", schools);
        Assert.Contains("987654", schools);
        Assert.Single(trusts);
        Assert.Contains("12345678", trusts);
    }

    [Fact]
    public async Task ShouldReturnLaSchoolsCorrectlyWhenLocalAuthority()
    {
        var organisationItem = new OrganisationItem
        {
            Id = "002"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            EstablishmentNumber = 123
        };
        var response = new LocalAuthority
        {
            Schools =
            [
                new LocalAuthoritySchool { URN = "123456" },
                new LocalAuthoritySchool { URN = "987654" }
            ]
        };
        _mockApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Equal(2, schools.Length);
        Assert.Contains("123456", schools);
        Assert.Contains("987654", schools);
        Assert.Empty(trusts);
    }

    [Fact]
    public async Task ShouldReturnCorrectlyWhenAcademy()
    {
        var organisationItem = new OrganisationItem
        {
            Id = "001"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            URN = 123456
        };

        var response = new School { URN = "123456", TrustCompanyNumber = "12345678" };
        _mockApi.Setup(api => api.GetSchool(organisation.URN.ToString(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Single(schools);
        Assert.Contains("123456", schools);
        Assert.Single(trusts);
        Assert.Contains("12345678", trusts);
    }

    [Fact]
    public async Task ShouldReturnCorrectlyWhenNotAcademy()
    {
        var organisationItem = new OrganisationItem
        {
            Id = "001"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            URN = 123456
        };
        var response = new School { URN = "123456" };

        _mockApi.Setup(api => api.GetSchool(organisation.URN.ToString(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Single(schools);
        Assert.Contains("123456", schools);
        Assert.Empty(trusts);
    }

    [Fact]
    public async Task ShouldReturnCorrectlyWhenOrganisationIsNull()
    {
        Organisation? organisation = null;
        _mockApi.Setup(api => api.GetSchool(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok());

        _mockApi.Setup(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok());

        _mockApi.Setup(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok());
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Empty(schools);
        Assert.Empty(trusts);
        _mockApi.Verify(api => api.GetSchool(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockApi.Verify(api => api.GetTrust(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
        _mockApi.Verify(api => api.GetLocalAuthority(It.IsAny<string>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}