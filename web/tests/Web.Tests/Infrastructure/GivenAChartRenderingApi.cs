using Web.App.Domain.Charts;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.ChartRendering;
using Xunit;
using Xunit.Abstractions;

namespace Web.Tests.Infrastructure;

public class GivenAChartRenderingApi(ITestOutputHelper output) : ApiClientTestBase(output)
{
    [Fact]
    public void SetsFunctionKeyIfProvided()
    {
        _ = new ChartRenderingApi(HttpClient, "my-key");
        Assert.Equal("my-key", HttpClient.DefaultRequestHeaders.GetValues("x-functions-key").First());
    }

    [Fact]
    public async Task PostHorizontalBarChartShouldCallCorrectUrl()
    {
        var api = new ChartRenderingApi(HttpClient);

        var request = new PostHorizontalBarChartRequest<SchoolComparisonDatum>();

        await api.PostHorizontalBarChart(request);

        VerifyCall(HttpMethod.Post, "api/horizontalBarChart");
    }

    [Fact]
    public async Task PostHorizontalBarChartsShouldCallCorrectUrl()
    {
        var api = new ChartRenderingApi(HttpClient);

        var requests = new PostHorizontalBarChartsRequest<SchoolComparisonDatum>(new[]
        {
            new PostHorizontalBarChartRequest<SchoolComparisonDatum>
            {
                BarHeight = 12,
                LabelField = "Foo",
                Data =
                [
                    new SchoolComparisonDatum
                    {
                        Urn = "123456",
                        SchoolName = "School A",
                        Expenditure = 100
                    }
                ]
            },
            new PostHorizontalBarChartRequest<SchoolComparisonDatum>
            {
                BarHeight = 21,
                LabelField = "Bar",
                Data =
                [
                    new SchoolComparisonDatum
                    {
                        Urn = "654321",
                        SchoolName = "School B",
                        Expenditure = 200
                    }
                ]
            }
        });

        await api.PostHorizontalBarCharts(requests);

        VerifyCall(HttpMethod.Post, "api/horizontalBarChart");
    }

    [Fact]
    public async Task PostVerticalBarChartShouldCallCorrectUrl()
    {
        var api = new ChartRenderingApi(HttpClient);

        var request = new PostVerticalBarChartRequest<PriorityCostCategoryDatum>();

        await api.PostVerticalBarChart(request);

        VerifyCall(HttpMethod.Post, "api/verticalBarChart");
    }

    [Fact]
    public async Task PostVerticalBarChartsShouldCallCorrectUrl()
    {
        var api = new ChartRenderingApi(HttpClient);

        var requests = new PostVerticalBarChartsRequest<PriorityCostCategoryDatum>(new[]
        {
            new PostVerticalBarChartRequest<PriorityCostCategoryDatum>
            {
                Data =
                [
                    new PriorityCostCategoryDatum
                    {
                        Urn = "123456",
                        Amount = 100
                    }
                ]
            },
            new PostVerticalBarChartRequest<PriorityCostCategoryDatum>
            {
                Data =
                [
                    new PriorityCostCategoryDatum
                    {
                        Urn = "654321",
                        Amount = 200
                    }
                ]
            }
        });

        await api.PostVerticalBarCharts(requests);

        VerifyCall(HttpMethod.Post, "api/verticalBarChart");
    }
}