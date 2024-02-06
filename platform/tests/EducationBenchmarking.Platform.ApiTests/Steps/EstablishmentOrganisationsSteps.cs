using System.Net;
using System.Text;
using EducationBenchmarking.Platform.ApiTests.Drivers;
using EducationBenchmarking.Platform.Domain.Responses;
using EducationBenchmarking.Platform.Functions;
using EducationBenchmarking.Platform.Functions.Extensions;
using EducationBenchmarking.Platform.Infrastructure.Search;
using FluentAssertions;
using TechTalk.SpecFlow.Assist;

namespace EducationBenchmarking.Platform.ApiTests.Steps;

[Binding]
public class EstablishmentOrganisationsSteps
{
    private const string SuggestValidRequestKey = "suggest-organisation-valid";
    private const string SuggestInvalidRequestKey = "suggest-organisation-invalid";
    private readonly EstablishmentApiDriver _api;

    public EstablishmentOrganisationsSteps(EstablishmentApiDriver api)
    {
        _api = api;
    }

    [Given("a valid organisations suggest request")]
    private void GivenAValidOrganisationsSuggestRequest()
    {
        var content = new { SearchText = "school", Size = 10, SuggesterName = "organisation-suggester" };

        _api.CreateRequest(SuggestValidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/organisations/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the organisations request")]
    private async Task WhenISubmitTheOrganisationsRequest()
    {
        await _api.Send();
    }

    [Then("the organisations suggest result should be:")]
    private async Task ThenTheOrganisationsSuggestResultShouldBe(Table table)
    {
        var response = _api[SuggestValidRequestKey].Response;
        
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<SuggestOutput<Organisation>>().Results;
        var set = new List<dynamic>();
        
        foreach (var result in results)
        {
            set.Add(new { result.Text, result.Document?.Identifier, result.Document?.Kind });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Given("an invalid organisations suggest request")]
    private void GivenAnInvalidOrganisationsSuggestRequest()
    {
        var content = new { Size = 0 };

        _api.CreateRequest(SuggestInvalidRequestKey, new HttpRequestMessage
        {
            RequestUri = new Uri("/api/organisations/suggest", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [Then("the organisations suggest result should have the follow validation errors:")]
    private async Task ThenTheOrganisationsSuggestResultShouldHaveTheFollowValidationErrors(Table table)
    {
        var response = _api[SuggestInvalidRequestKey].Response;
        
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);

        var content = await response.Content.ReadAsByteArrayAsync();
        var results = content.FromJson<ValidationError[]>();
        var set = new List<dynamic>();
        
        foreach (var result in results)
        {
            set.Add(new { result.PropertyName, result.ErrorMessage });
        }

        table.CompareToDynamicSet(set, false);
    }
}