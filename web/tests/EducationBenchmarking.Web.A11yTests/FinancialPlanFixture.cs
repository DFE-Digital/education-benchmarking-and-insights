using System.Net;
using System.Text;
using EducationBenchmarking.Web.A11yTests.Drivers;
using EducationBenchmarking.Web.Extensions;
using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.A11yTests;

[CollectionDefinition(nameof(FinancialPlanCollection))]
public class FinancialPlanCollection : ICollectionFixture<FinancialPlanFixture>;

[CollectionDefinition(nameof(FinancialPlanMinimalDataCollection))]
public class FinancialPlanMinimalDataCollection : ICollectionFixture<FinancialPlanMinimalDataFixture>;

public class FinancialPlanFixture(IMessageSink messageSink)
    : FinancialPlanBaseFixture(
        DateTime.UtcNow.Year + 1,
        new { User = "ally-test-user" },
        messageSink);

public class FinancialPlanMinimalDataFixture(IMessageSink messageSink)
    : FinancialPlanBaseFixture(
        DateTime.UtcNow.Year + 2,
        new { UseFigures = true, User = "ally-test-user" },
        messageSink);


public abstract class FinancialPlanBaseFixture : IDisposable
{
    private const string CreateKey = nameof(CreateKey);
    private const string RemoveKey = nameof(RemoveKey);
    private readonly IMessageSink _messageSink;
    private readonly BenchmarkApiDriver _apiDriver;

    public object Content { get; }
    public int Year { get; }
    public string Urn { get; }

    protected FinancialPlanBaseFixture(int year, object content, IMessageSink messageSink)
    {
        _messageSink = messageSink;
        _apiDriver = new BenchmarkApiDriver(messageSink);

        Year = year;
        Content = content;
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

        _apiDriver.CreateRequest(CreateKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{Urn}/{Year}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(Content.ToJson(), Encoding.UTF8, "application/json")
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