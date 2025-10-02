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
}