// TODO: review for public beta
//using Xunit;

//namespace Web.Integration.Tests.Pages;

//public class WhenViewingSubmitEnquiry(SchoolBenchmarkingWebAppClient client) : PageBase<SchoolBenchmarkingWebAppClient>(client)
//{
//    [Fact]
//    public async Task CanDisplay()
//    {
//        var page = await Client.Navigate(Paths.SubmitEnquiry);

//        var expectedBreadcrumbs = new[] { ("Home", Paths.ServiceHome.ToAbsolute()) };

//        DocumentAssert.AssertPageUrl(page, Paths.SubmitEnquiry.ToAbsolute());
//        DocumentAssert.Breadcrumbs(page, expectedBreadcrumbs);
//        DocumentAssert.TitleAndH1(page, "Financial Benchmarking and Insights Tool - GOV.UK", "Submit an enquiry");
//    }
//}