using Platform.Api.Content.Features.Files.Responses;
using Platform.ApiTests.Assertion;
using Platform.ApiTests.Drivers;
using Platform.Json;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Content files endpoints")]
public class ContentFilesSteps(ContentApiDriver api)
{
    private const string FilesKey = "files";

    [Given("a transparency files request")]
    public void GivenATransparencyFilesRequest()
    {
        api.CreateRequest(FilesKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/files/transparency", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the files request")]
    public async Task WhenISubmitTheFilesRequest()
    {
        await api.Send();
    }

    [Then("the result should be ok and contain:")]
    public async Task ThenTheResultShouldBeOkAndContain(DataTable table)
    {
        var response = api[FilesKey].Response;
        AssertHttpResponse.IsOk(response);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<FileResponse[]>();
        table.CompareToSet(result);
    }
}