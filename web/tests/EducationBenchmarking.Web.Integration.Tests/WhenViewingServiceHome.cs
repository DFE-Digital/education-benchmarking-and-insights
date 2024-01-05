using Xunit;

namespace EducationBenchmarking.Web.Integration.Tests;

public class WhenViewingServiceHome : BenchmarkingWebAppClient
{
    public WhenViewingServiceHome(BenchmarkingWebAppFactory factory) : base(factory)
    {
    }
    
    [Fact]
    public async Task CanDisplayHomepage()
    {
        var page = await Navigate("/");
            
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights","Education benchmarking and insights");
        
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        DocumentAssert.PrimaryCTA(startButton, "Start now", "/find-organisation");
    }
    
    [Fact]
    public async Task CanNavigateToFindOrganisation()
    {
        var page = await Navigate("/");
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        
        page = await Follow(startButton);
        
        DocumentAssert.AssertPageUrl(page, "https://localhost/find-organisation");
    }
}