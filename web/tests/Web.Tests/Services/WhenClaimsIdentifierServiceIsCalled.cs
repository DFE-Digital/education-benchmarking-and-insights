﻿using Moq;
using Xunit;
using Web.App.Services;
using Web.App.Infrastructure.Apis.Establishment;
using Web.App.Domain;
using Web.App.Identity.Models;
using Web.App.Infrastructure.Apis;

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
            CompanyRegistrationNumber = 12345678,
        };
        var response = new[] {
            new School { URN = "123456", TrustCompanyNumber = "12345678" },
        };
        ApiQuery? query = null;
        _mockApi.Setup(api => api.QuerySchools(It.IsAny<ApiQuery>()))
            .Callback<ApiQuery>(x =>
            {
                query = x;
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Single(schools);
        Assert.Contains("123456", schools);
        Assert.Single(trusts);
        Assert.Contains("12345678", trusts);
        Assert.Equal("?companyNumber=12345678", query?.ToQueryString());
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
            CompanyRegistrationNumber = 12345678,
        };
        var response = new[] {
            new School { URN = "123456", TrustCompanyNumber = "12345678" },
            new School { URN = "987654", TrustCompanyNumber = "12345678" }
        };
        ApiQuery? query = null;
        _mockApi.Setup(api => api.QuerySchools(It.IsAny<ApiQuery>()))
            .Callback<ApiQuery>(x =>
            {
                query = x;
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Equal(2, schools.Length);
        Assert.Contains("123456", schools);
        Assert.Contains("987654", schools);
        Assert.Single(trusts);
        Assert.Contains("12345678", trusts);
        Assert.Equal("?companyNumber=12345678", query?.ToQueryString());
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
            EstablishmentNumber = 123,
        };
        var response = new[] {
            new School { URN = "123456", LACode = "123" },
            new School { URN = "987654", LACode = "123" }
        };
        ApiQuery? query = null;
        _mockApi.Setup(api => api.QuerySchools(It.IsAny<ApiQuery>()))
            .Callback<ApiQuery>(x =>
            {
                query = x;
            })
            .ReturnsAsync(ApiResult.Ok(response));
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Equal(2, schools.Length);
        Assert.Contains("123456", schools);
        Assert.Contains("987654", schools);
        Assert.Empty(trusts);
        Assert.Equal("?laCode=123", query?.ToQueryString());
    }

    [Fact]
    public async Task ShouldReturnCorrectlyWhenAcademy()
    {
        var organisationItem = new OrganisationItem()
        {
            Id = "001"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            URN = 123456
        };

        var response = new School { URN = "123456", TrustCompanyNumber = "12345678" };
        _mockApi.Setup(api => api.GetSchool(organisation.URN.ToString()))
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
        var organisationItem = new OrganisationItem()
        {
            Id = "001"
        };
        var organisation = new Organisation
        {
            Category = organisationItem,
            URN = 123456
        };
        var response = new School { URN = "123456" };

        _mockApi.Setup(api => api.GetSchool(organisation.URN.ToString()))
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
        _mockApi.Setup(api => api.GetSchool(It.IsAny<string>()))
            .ReturnsAsync(ApiResult.Ok());

        _mockApi.Setup(api => api.QuerySchools(It.IsAny<ApiQuery>()))
            .ReturnsAsync(ApiResult.Ok());
        var service = new ClaimsIdentifierService(_mockApi.Object);

        var (schools, trusts) = await service.IdentifyValidClaims(organisation);

        Assert.Empty(schools);
        Assert.Empty(trusts);
        _mockApi.Verify(api => api.GetSchool(It.IsAny<string>()), Times.Never);
        _mockApi.Verify(api => api.QuerySchools(It.IsAny<ApiQuery>()), Times.Never);
    }
}