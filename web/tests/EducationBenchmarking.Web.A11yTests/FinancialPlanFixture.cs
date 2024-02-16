using System.Net;
using System.Text;
using EducationBenchmarking.Web.A11yTests.Drivers;
using EducationBenchmarking.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

[CollectionDefinition(nameof(FinancialPlanCollection))]
public class FinancialPlanCollection : ICollectionFixture<FinancialPlanFixture>;

public class FinancialPlanFixture : IDisposable
{
    private const string CreateKey = nameof(CreateKey);
    private const string RemoveKey = nameof(RemoveKey);
    private readonly IMessageSink _messageSink;
    private readonly BenchmarkApiDriver _apiDriver;

    public int Year { get; }
    public string Urn { get; }

    public FinancialPlanFixture(IMessageSink messageSink)
    {
        _messageSink = messageSink;
        _apiDriver = new BenchmarkApiDriver(messageSink);

        Year = TestConfiguration.Instance.GetValue<int?>("PlanYear") ?? DateTime.UtcNow.Year + 1;
        Urn = TestConfiguration.School;

        SeedFinancialPlan().GetAwaiter().GetResult();
    }

    public async void Dispose()
    {
        await Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual async Task Dispose(bool disposing)
    {
        if (disposing)
        {
            await CleanupFinancialPlan();
            _apiDriver.Dispose();
        }
    }

    private async Task SeedFinancialPlan()
    {
        _messageSink.OnMessage($"Seeding financial plan [year:{Year}, school:{Urn}]".ToDiagnosticMessage());

        var content = new { User = "ally-test-user" };
        _apiDriver.CreateRequest(CreateKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{Urn}/{Year}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });

        await _apiDriver.Send();
        Assert.True(_apiDriver[CreateKey].Response.StatusCode is HttpStatusCode.Created or HttpStatusCode.NoContent);
    }

    private async Task CleanupFinancialPlan()
    {
        _messageSink.OnMessage($"Cleaning up financial plan [year:{Year}, school:{Urn}]".ToDiagnosticMessage());
        _apiDriver.CreateRequest(RemoveKey, new HttpRequestMessage
        {
            RequestUri =
                new Uri($"/api/financial-plan/{Urn}/{Year}", UriKind.Relative),
            Method = HttpMethod.Delete
        });

        await _apiDriver.Send();
    }
}