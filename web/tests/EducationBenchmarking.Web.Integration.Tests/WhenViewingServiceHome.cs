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
        var page = await Navigate(Paths.ServiceHome);
        
        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };
        
        DocumentAssert.AssertPageUrl(page, Paths.ServiceHome.ToAbsolute());
        DocumentAssert.Breadcrumbs(page,expectedBreadcrumbs);
        DocumentAssert.TitleAndH1(page, "Education benchmarking and insights","Education benchmarking and insights");
        
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        DocumentAssert.PrimaryCta(startButton, "Start now", Paths.FindOrganisation);
    }
    
    [Fact]
    public async Task CanNavigateToFindOrganisation()
    {
        var page = await Navigate(Paths.ServiceHome);
        var startButton = page.GetElementsByClassName("govuk-button--start").FirstOrDefault();
        Assert.NotNull(startButton);
        
        page = await Follow(startButton);
        
        DocumentAssert.AssertPageUrl(page, Paths.FindOrganisation.ToAbsolute());
    }
}