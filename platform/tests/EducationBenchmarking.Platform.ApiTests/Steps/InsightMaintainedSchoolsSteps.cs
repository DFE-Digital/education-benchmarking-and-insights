using System.Net;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightMaintainedSchoolsSteps : InsightSteps
{
    private const string GetMaintainedSchoolKey = "get-maintained-school";

    public InsightMaintainedSchoolsSteps(ITestOutputHelper output) : base(output)
    {
    }

    [When(@"I submit the maintained school request")]
    public async Task WhenISubmitTheMaintainedSchoolRequest()
    {
        await Api.Send();
    }

    [Given(@"a valid maintained school request with urn '(.*)'")]
    public void GivenAValidMaintainedSchoolRequestWithUrn(string urn)
    {
        Api.CreateRequest(GetMaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/maintained-school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the maintained school result should be ok")]
    public async Task ThenTheMaintainedSchoolResultShouldBeOk()
    {
        var response = Api[GetMaintainedSchoolKey].Response ??
                       throw new NullException(Api[GetMaintainedSchoolKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Finances>() ?? throw new NullException(content);
        result.SchoolName.Should().Be("Stockingford Maintained Nursery School");
        result.Urn.Should().Be("125491");
    }

    [Given(@"a invalid maintained school request")]
    public void GivenAInvalidMaintainedSchoolRequest()
    {
        Api.CreateRequest(GetMaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/maintained-school/00", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the maintained school result should be not found")]
    public void ThenTheMaintainedSchoolResultShouldBeNotFound()
    {
        var response = Api[GetMaintainedSchoolKey].Response ??
                       throw new NullException(Api[GetMaintainedSchoolKey].Response);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}