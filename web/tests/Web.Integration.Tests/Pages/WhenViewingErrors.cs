using Xunit;

namespace Web.Integration.Tests.Pages;

public class WhenViewingErrors(BenchmarkingWebAppClient client) : PageBase(client)
{
    [Fact]
    public async Task CanDisplayProblemWithService()
    {
        var page = await Client.Navigate(Paths.Error);

        PageAssert.IsProblemPage(page);
    }

    [Theory]
    [InlineData(403)]
    [InlineData(404)]
    [InlineData(500)]
    public async Task CanDisplayStatusError(int statusCode)
    {
        var page = await Client.Navigate(Paths.StatusError(statusCode));

        switch (statusCode)
        {
            case 403:
                PageAssert.IsAccessDeniedPage(page);
                break;
            case 404:
                PageAssert.IsNotFoundPage(page);
                break;
            case 500:
                PageAssert.IsProblemPage(page);
                break;
        }
    }
}