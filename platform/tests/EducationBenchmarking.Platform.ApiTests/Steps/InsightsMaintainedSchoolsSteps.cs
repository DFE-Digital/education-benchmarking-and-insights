using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightsMaintainedSchoolsSteps
{
    private const string GetMaintainedSchoolKey = "get-maintained-school";
    private readonly ApiDriver _api = new(Config.Apis.Insight ?? throw new NullException(Config.Apis.Insight));
    [When(@"I submit the maintained school request")]
    public async Task WhenISubmitTheMaintainedSchoolRequest()
    {
        await _api.Send();
    }

    [Given(@"a valid maintained school request with urn '(.*)'")]
    public void GivenAValidMaintainedSchoolRequestWithUrn(string urn)
    {
        _api.CreateRequest(GetMaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/maintained-school/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the maintained school result should be ok")]
    public async Task ThenTheMaintainedSchoolResultShouldBeOk()
    {
        var response = _api[GetMaintainedSchoolKey].Response ?? throw new NullException(_api[GetMaintainedSchoolKey].Response);

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
        _api.CreateRequest(GetMaintainedSchoolKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/maintained-school/00", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the maintained school result should be not found")]
    public void ThenTheMaintainedSchoolResultShouldBeNotFound()
    {
        var response = _api[GetMaintainedSchoolKey].Response ?? throw new NullException(_api[GetMaintainedSchoolKey].Response);
        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}