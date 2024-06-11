// TODO: review for public beta
//using Xunit;

//namespace Web.Integration.Tests.Pages;

//public class WhenViewingAskForHelp(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
//{
//    [Fact]
//    public async Task CanDisplay()
//    {
//        var page = await Client.Navigate(Paths.AskForHelp);

//        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

//        DocumentAssert.AssertPageUrl(page, Paths.AskForHelp.ToAbsolute());
//        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
//        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Ask for help from an SRMA");
//    }
//}