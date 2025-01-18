using System.Net;
using FluentAssertions;
using Platform.Api.Insight.MetricRagRatings;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
public class MetricRagRatingsBalanceSteps(InsightApiDriver api)
{
    private const string MetricRagRatingsKey = "metric-rag-ratings";

    [Given("a valid user defined metric rag rating with runId '(.*)', useCustomData '(.*)'")]
    public void GivenAValidUserDefinedMetricRagRatingWithRunIdUseCustomData(string runId, string useCustomData)
    {
        api.CreateRequest(MetricRagRatingsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/metric-rag/{runId}?useCustomData={useCustomData}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid default metric rag rating with categories '(.*)' and statuses '(.*)' for urns:")]
    public void GivenAValidDefaultMetricRagRatingWithCategoriesAndStatusesForUrns(string categories, string statuses, DataTable table)
    {
        var urns = GetFirstColumnsFromTableRowsAsString(table);
        api.CreateRequest(MetricRagRatingsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/metric-rag/default/?urns={string.Join("&urns=", urns)}&categories={string.Join("&categories=", categories.Split(","))}&statuses={string.Join("&statuses=", statuses.Split(","))}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid default metric rag rating with categories '(.*)' and statuses '(.*)' with company number '(.*)'")]
    public void GivenAValidDefaultMetricRagRatingWithCategoriesAndStatusesWithCompanyNumber(string categories, string statuses, string companyNumber)
    {
        api.CreateRequest(MetricRagRatingsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/metric-rag/default/?companyNumber={companyNumber}&categories={string.Join("&categories=", categories.Split(","))}&statuses={string.Join("&statuses=", statuses.Split(","))}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("a valid default metric rag rating with categories '(.*)' and statuses '(.*)' with LA code '(.*)' and phase '(.*)'")]
    public void GivenAValidDefaultMetricRagRatingWithCategoriesAndStatusesWithLaCodeAndPhase(string categories, string statuses, string laCode, string phase)
    {
        api.CreateRequest(MetricRagRatingsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/metric-rag/default/?laCode={laCode}&phase={phase}&categories={string.Join("&categories=", categories.Split(","))}&statuses={string.Join("&statuses=", statuses.Split(","))}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the metric rag rating request")]
    public async Task WhenISubmitTheMetricRagRatingRequest()
    {
        await api.Send();
    }

    [Then("the metric rag rating result should be ok and contain:")]
    public async Task ThenTheMetricRagRatingResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[MetricRagRatingsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<MetricRagRating[]>();
        table.CompareToSet(result);
    }

    [Then("the metric rag rating result should be bad request")]
    public void ThenTheMetricRagRatingResultShouldBeBadRequest()
    {
        var response = api[MetricRagRatingsKey].Response;

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
    }

    private static IEnumerable<string> GetFirstColumnsFromTableRowsAsString(DataTable table)
    {
        return table.Rows
            .Select(r => r.Select(kvp => kvp.Value).FirstOrDefault())
            .Where(v => !string.IsNullOrWhiteSpace(v))
            .OfType<string>();
    }
}