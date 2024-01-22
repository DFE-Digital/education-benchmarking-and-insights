using System.Net;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.ApiTests.TestSupport;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions.Extensions;
using FluentAssertions;
using Xunit.Abstractions;
using Xunit.Sdk;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class InsightAcademiesSteps
{
    private const string GetAcademyKey = "get-academy";
    private readonly ApiDriver _api;

    public InsightAcademiesSteps(ITestOutputHelper output)
    {
        _api = new ApiDriver(Config.Apis.Insight ?? throw new NullException(Config.Apis.Insight), output);
    }
    
    [Given(@"a valid academy request with urn '(.*)'")]
    public void GivenAValidAcademyRequestWithUrn(string urn)
    {
        _api.CreateRequest(GetAcademyKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/academy/{urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When(@"I submit the academies request")]
    public async Task WhenISubmitTheAcademiesRequest()
    {
        await _api.Send();
    }

    [Then(@"the academies result should be ok")]
    public async Task ThenTheAcademiesResultShouldBeOk()
    {
        var response = _api[GetAcademyKey].Response ?? throw new NullException(_api[GetAcademyKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.OK);
        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<Finances>() ?? throw new NullException(content);
        result.SchoolName.Should().Be("Mansel Primary");
        result.Urn.Should().Be("139137");
    }

    [Given(@"a invalid academy request")]
    public void GivenAInvalidAcademyRequest()
    {
        _api.CreateRequest(GetAcademyKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/academy/00", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Then(@"the academies result should be not found")]
    public void ThenTheAcademiesResultShouldBeNotFound()
    {
        var response = _api[GetAcademyKey].Response ?? throw new NullException(_api[GetAcademyKey].Response);

        response.Should().NotBeNull();
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }
}