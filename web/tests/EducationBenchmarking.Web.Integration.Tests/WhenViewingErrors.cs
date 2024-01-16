using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingErrors : BenchmarkingWebAppClient
{
    public WhenViewingErrors(BenchmarkingWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task CanDisplayProblemWithServicePage()
    {
        var page = await Navigate(Paths.Error);
            
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights","Sorry, there is a problem with the service");
    }
    
    [Theory]
    [InlineData(404, "Page not found")]
    [InlineData(500, "Sorry, there is a problem with the service")]
    public async Task CanDisplayStatusErrorPage(int statusCode, string heading)
    {
        var page = await Navigate(Paths.StatusError(statusCode));
            
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights",heading);
    }
}