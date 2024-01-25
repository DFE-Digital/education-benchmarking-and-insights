using Xunit;
using Xunit.Abstractions;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingErrors(BenchmarkingWebAppFactory factory, ITestOutputHelper output)
    : BenchmarkingWebAppClient(factory, output)
{
    [Fact]
    public async Task CanDisplayProblemWithServicePage()
    {
        var page = await Navigate(Paths.Error);
            
        DocumentAssert.TitleAndH1(page, "Sorry, there is a problem with the service","Sorry, there is a problem with the service");
    }
    
    [Theory]
    [InlineData(404, "Page not found")]
    [InlineData(500, "Sorry, there is a problem with the service")]
    public async Task CanDisplayStatusErrorPage(int statusCode, string heading)
    {
        var page = await Navigate(Paths.StatusError(statusCode));
            
        DocumentAssert.TitleAndH1(page, heading,heading);
    }
}