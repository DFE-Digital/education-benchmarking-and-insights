using AutoFixture;
using Microsoft.Extensions.Logging;
using Moq;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Web.App.Services;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.Services;

public class WhenTrustItSpendChartServiceIsCalled
{
    private readonly Mock<IChartRenderingApi> _chartRenderingApiMock = new();
    private readonly Mock<ILogger<TrustItSpendChartService>> _loggerMock = new();
    private readonly Fixture _fixture = new();

    public static TheoryData<TrustItSpend[], TrustItSpendForecastYear[]?, decimal?[], decimal?[]> BuildChartsDomainTestData = new()
    {
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 0
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            null, [
                null // no forecast data
            ],
            [
                null // no forecast data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 0
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    Connectivity = 100
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    Connectivity = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    Connectivity = 300
                }
            ],
            [
                null // no matching forecast data
            ],
            [
                null // no matching forecast data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 0
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 300
                }
            ],
            [
                null // 0 resolves to null
            ],
            [
                1000 // maximum value from spend data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 50
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 3000
                }
            ],
            [
                50 // minimum value from spend data
            ],
            [
                3000 // maximum value from forecast data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 3000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 50
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                50 // minimum value from forecast data
            ],
            [
                3000 // maximum value from spend data
            ]
        }
    };

    public static TheoryData<TrustItSpend[], TrustItSpendForecastYear[]?, decimal?[], decimal?[]> BuildForecastChartsDomainTestData = new()
    {
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 0
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 300
                }
            ],
            [
                null // 0 resolves to null
            ],
            [
                1000 // maximum value from spend data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 50
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 3000
                }
            ],
            [
                50 // minimum value from spend data
            ],
            [
                3000 // maximum value from forecast data
            ]
        },
        {
            [
                new TrustItSpend
                {
                    CompanyNumber = "12345678",
                    AdministrationSoftwareAndSystems = 100
                },
                new TrustItSpend
                {
                    CompanyNumber = "23456789",
                    AdministrationSoftwareAndSystems = 3000
                }
            ],
            [
                new TrustItSpendForecastYear
                {
                    Year = 2022,
                    AdministrationSoftwareAndSystems = 50
                },
                new TrustItSpendForecastYear
                {
                    Year = 2023,
                    AdministrationSoftwareAndSystems = 200
                },
                new TrustItSpendForecastYear
                {
                    Year = 2024,
                    AdministrationSoftwareAndSystems = 1000
                }
            ],
            [
                50 // minimum value from forecast data
            ],
            [
                3000 // maximum value from spend data
            ]
        }
    };

    [Fact]
    public async Task BuildChartsAsyncReturnsChartResponseWhenApiSucceeds()
    {
        // Arrange
        var expectedResponse = new[] { new ChartResponse() };
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustComparisonDatum>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expectedResponse));

        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(10).ToArray();

        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();

        var filters = new[]
        {
            ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems,
            ItSpendingCategories.SubCategoryFilter.Connectivity
        };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, forecasts, filters);

        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        var result = await service.BuildChartsAsync("123", Dimensions.ResultAsOptions.Actuals, subCategories, _ => "url/");

        // Assert
        Assert.Equivalent(expectedResponse, result);
    }

    [Fact]
    public async Task BuildChartsAsyncReturnsEmptyWhenApiFails()
    {
        // Arrange
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustComparisonDatum>>(), It.IsAny<CancellationToken>()))
            .ThrowsAsync(new Exception("API error"));

        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(10).ToArray();
        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();
        var filters = new[] { ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, forecasts, filters);
        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        var result = await service.BuildChartsAsync("123", Dimensions.ResultAsOptions.Actuals, subCategories, s => $"url/{s}");

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task BuildChartsAsyncBuildsExpectedRequest()
    {
        PostHorizontalBarChartsRequest<TrustComparisonDatum>? actualRequest = null;
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustComparisonDatum>>(), It.IsAny<CancellationToken>()))
            .Callback<PostHorizontalBarChartsRequest<TrustComparisonDatum>, CancellationToken>((request, _) =>
            {
                actualRequest = request;
            });

        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(10).ToArray();

        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();

        var filters = new[]
        {
            ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems,
            ItSpendingCategories.SubCategoryFilter.Connectivity
        };

        const string companyNumber = "123";
        var subCategories = new TrustComparisonSubCategoriesViewModel(companyNumber, expenditures, forecasts, filters);

        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        await service.BuildChartsAsync(companyNumber, Dimensions.ResultAsOptions.Actuals, subCategories, u => $"url/{u}");

        // Assert
        Assert.Equal(subCategories.Items.Count, actualRequest?.Count);
        for (var i = 0; i < subCategories.Items.Count; i++)
        {
            var item = actualRequest?.ElementAtOrDefault(i);
            var viewModel = subCategories.Items.ElementAtOrDefault(i);
            Assert.Equal(viewModel?.Uuid, item?.Id);
            Assert.Equal(companyNumber, item?.HighlightKey);
            Assert.Equal(viewModel?.Data, item?.Data);
            Assert.Equal("url/%1$s", item?.LinkFormat);
            Assert.Equal("currency", item?.ValueType);
            Assert.Equal("actuals", item?.XAxisLabel);
        }
    }

    [Theory]
    [MemberData(nameof(BuildChartsDomainTestData))]
    public async Task BuildChartsAsyncReturnsExpectedRequestDomain(TrustItSpend[] expenditures, TrustItSpendForecastYear[]? forecasts, decimal?[] expectedDomainsMin, decimal?[] expectedDomainsMax)
    {
        // Arrange
        PostHorizontalBarChartsRequest<TrustComparisonDatum>? actualRequest = null;
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustComparisonDatum>>(), It.IsAny<CancellationToken>()))
            .Callback<PostHorizontalBarChartsRequest<TrustComparisonDatum>, CancellationToken>((request, _) =>
            {
                actualRequest = request;
            });

        var filters = new[] { ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, forecasts, filters);
        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        await service.BuildChartsAsync("123", Dimensions.ResultAsOptions.Actuals, subCategories, s => $"url/{s}");

        // Assert
        Assert.Equal(subCategories.Items.Count, actualRequest?.Count);
        for (var i = 0; i < subCategories.Items.Count; i++)
        {
            var item = actualRequest?.ElementAtOrDefault(i);
            Assert.Equal(expectedDomainsMin.ElementAtOrDefault(i), item?.DomainMin);
            Assert.Equal(expectedDomainsMax.ElementAtOrDefault(i), item?.DomainMax);
        }
    }

    [Fact]
    public async Task BuildForecastChartsAsyncReturnsChartResponseWhenApiSucceeds()
    {
        // Arrange
        var expectedResponse = new[] { new ChartResponse() };
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustForecastDatum>>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(ApiResult.Ok(expectedResponse));

        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(10).ToArray();
        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();
        var filters = new[] { ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, forecasts, filters);

        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        var result = await service.BuildForecastChartsAsync(Dimensions.ResultAsOptions.Actuals, subCategories);

        // Assert
        Assert.Equivalent(expectedResponse, result);
    }

    [Fact]
    public async Task BuildForecastChartsAsyncReturnsEmptyWhenNoForecastData()
    {
        // Arrange
        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(5).ToArray();
        var filters = new[] { ItSpendingCategories.SubCategoryFilter.Connectivity };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, null, filters);

        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        var result = await service.BuildForecastChartsAsync(Dimensions.ResultAsOptions.Actuals, subCategories);

        // Assert
        Assert.Empty(result);
    }

    [Fact]
    public async Task BuildForecastChartsAsyncBuildsExpectedRequest()
    {
        PostHorizontalBarChartsRequest<TrustForecastDatum>? actualRequest = null;
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustForecastDatum>>(), It.IsAny<CancellationToken>()))
            .Callback<PostHorizontalBarChartsRequest<TrustForecastDatum>, CancellationToken>((request, _) =>
            {
                actualRequest = request;
            });

        var expenditures = _fixture.Build<TrustItSpend>().CreateMany(10).ToArray();

        var forecasts = _fixture.Build<TrustItSpendForecastYear>().CreateMany(3).ToArray();

        var filters = new[]
        {
            ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems,
            ItSpendingCategories.SubCategoryFilter.Connectivity
        };

        const string companyNumber = "123";
        var subCategories = new TrustComparisonSubCategoriesViewModel(companyNumber, expenditures, forecasts, filters);

        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        await service.BuildForecastChartsAsync(Dimensions.ResultAsOptions.Actuals, subCategories);

        // Assert
        Assert.Equal(subCategories.Items.Count, actualRequest?.Count);
        for (var i = 0; i < subCategories.Items.Count; i++)
        {
            var item = actualRequest?.ElementAtOrDefault(i);
            var viewModel = subCategories.Items.ElementAtOrDefault(i);
            Assert.Equal(viewModel?.Uuid, item?.Id);
            Assert.Null(item?.HighlightKey);
            Assert.Equal(viewModel?.ForecastData, item?.Data);
            Assert.Null(item?.LinkFormat);
            Assert.Equal("currency", item?.ValueType);
            Assert.Equal("actuals", item?.XAxisLabel);
        }
    }

    [Theory]
    [MemberData(nameof(BuildForecastChartsDomainTestData))]
    public async Task BuildForecastChartsAsyncReturnsExpectedRequestDomain(TrustItSpend[] expenditures, TrustItSpendForecastYear[]? forecasts, decimal?[] expectedDomainsMin, decimal?[] expectedDomainsMax)
    {
        // Arrange
        PostHorizontalBarChartsRequest<TrustForecastDatum>? actualRequest = null;
        _chartRenderingApiMock
            .Setup(api => api.PostHorizontalBarCharts(It.IsAny<PostHorizontalBarChartsRequest<TrustForecastDatum>>(), It.IsAny<CancellationToken>()))
            .Callback<PostHorizontalBarChartsRequest<TrustForecastDatum>, CancellationToken>((request, _) =>
            {
                actualRequest = request;
            });

        var filters = new[] { ItSpendingCategories.SubCategoryFilter.AdministrationSoftwareSystems };

        var subCategories = new TrustComparisonSubCategoriesViewModel("123", expenditures, forecasts, filters);
        var service = new TrustItSpendChartService(_chartRenderingApiMock.Object, _loggerMock.Object);

        // Act
        await service.BuildForecastChartsAsync(Dimensions.ResultAsOptions.Actuals, subCategories);

        // Assert
        Assert.Equal(subCategories.Items.Count, actualRequest?.Count);
        for (var i = 0; i < subCategories.Items.Count; i++)
        {
            var item = actualRequest?.ElementAtOrDefault(i);
            Assert.Equal(expectedDomainsMin.ElementAtOrDefault(i), item?.DomainMin);
            Assert.Equal(expectedDomainsMax.ElementAtOrDefault(i), item?.DomainMax);
        }
    }
}