using System.Net;
using System.Text;
using EducationBenchmarking.Web.Extensions;
using Microsoft.Extensions.Configuration;
using Xunit;

namespace EducationBenchmarking.Web.A11yTests;

[CollectionDefinition(nameof(FinancialPlanCollection))]
public class FinancialPlanCollection : ICollectionFixture<FinancialPlanFixture> { }

public class FinancialPlanFixture : IDisposable
{
    private const string CreateKey = nameof(CreateKey);
    private const string RemoveKey = nameof(RemoveKey);
    
    private readonly BenchmarkApiDriver _apiDriver = new();

    public int Year { get; } 
    public string Urn { get; }
    
    public FinancialPlanFixture()
    {
        
        Year = TestConfiguration.Instance.GetValue<int?>("PlanYear") ?? DateTime.UtcNow.Year + 1;
        Urn = TestConfiguration.School;
        
        var content = new { User = "ally-test-user" };
        _apiDriver.CreateRequest(CreateKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{Urn}/{Year}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });

        _apiDriver.Send().GetAwaiter().GetResult();
        Assert.True(_apiDriver[CreateKey].Response.StatusCode is HttpStatusCode.Created or HttpStatusCode.NoContent);
    }
    
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
        if (disposing)
        {
            _apiDriver.CreateRequest(RemoveKey, new HttpRequestMessage
            {
                RequestUri =
                    new Uri($"/api/financial-plan/{Urn}/{Year}", UriKind.Relative),
                Method = HttpMethod.Delete
            });

            _apiDriver.Send().GetAwaiter().GetResult();
        }
    }
}