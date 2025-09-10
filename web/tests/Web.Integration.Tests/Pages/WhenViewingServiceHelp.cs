// TODO: review for public beta
//using Xunit;

//namespace Web.Integration.Tests.Pages;

//public class WhenViewingServiceHelp(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
//{
//    [Fact]
//    public async Task CanDisplay()
//    {
//        var page = await Client.Navigate(Paths.ServiceHelp);

//        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

//        DocumentAssert.AssertPageUrl(page, Paths.ServiceHelp.ToAbsolute());
//        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
//        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Help with this service");
//    }
//}