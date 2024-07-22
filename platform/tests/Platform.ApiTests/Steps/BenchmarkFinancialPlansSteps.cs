using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Benchmark.FinancialPlans;
using Platform.ApiTests.Drivers;
using Platform.Functions.Extensions;
using Xunit;
namespace Platform.ApiTests.Steps;

[Binding]
public class BenchmarkFinancialPlansSteps(BenchmarkApiDriver api)
{
    private const string FinancialPlansKey = "financial-plans";
    private const string UserId = "api.test@example.com";

    [Given("I have a valid financial plans get request for school id '(.*)' in year '(.*)' containing:")]
    public async Task GivenIHaveAValidFinancialPlansGetRequestForSchoolIdInYearContaining(string urn, string year, Table table)
    {
        var yearParsed = int.Parse(year);
        PutFinancialPlansRequest(urn, yearParsed, table);
        await WhenISubmitTheFinancialPlansRequest();
        GetFinancialPlansRequest(urn, yearParsed);
    }

    [Given("I have a valid financial plans put request for school id '(.*)' in year '(.*)' containing:")]
    public void GivenIHaveAValidFinancialPlansPutRequestForSchoolIdInYearContaining(string urn, string year, Table table)
    {
        var yearParsed = int.Parse(year);
        PutFinancialPlansRequest(urn, yearParsed, table);
    }

    [Given("I have a valid financial plans delete request for school id '(.*)' in year '(.*)' containing:")]
    public async Task GivenIHaveAValidFinancialPlansDeleteRequestForSchoolIdInYearContaining(string urn, string year, Table table)
    {
        var yearParsed = int.Parse(year);
        PutFinancialPlansRequest(urn, yearParsed, table);
        await WhenISubmitTheFinancialPlansRequest();
        DeleteFinancialPlansRequest(urn, yearParsed);
    }

    [Given("I have an invalid financial plans delete request for school id '(.*)' in year '(.*)'")]
    public void GivenIHaveAnInvalidFinancialPlansDeleteRequestForSchoolIdInYear(string urn, string year)
    {
        var yearParsed = int.Parse(year);
        DeleteFinancialPlansRequest(urn, yearParsed);
    }

    [Given("I do not have a deployed financial plan for school id '(.*)' in year '(.*)'")]
    public void GivenIDoNotHaveADeployedFinancialPlanForSchoolIdInYear(string urn, string year)
    {
        api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{urn}/{year}/deployment", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [Given("I have a valid financial plans get request including school id '(.*)' in year '(.*)' but excluding:")]
    public async Task GivenIHaveAValidFinancialPlansGetRequestIncludingSchoolIdInYearButExcluding(string urn, string year, Table table)
    {
        var dictionary = table.Rows
            .Select(r => new KeyValuePair<string, string>(r.Values.ElementAt(0), r.Values.ElementAt(1)))
            .ToDictionary(k => k.Key, v => v.Value);
        dictionary.Add(urn, year);

        foreach (var kvp in dictionary)
        {
            var json = new Dictionary<string, object>
            {
                {
                    "Urn", kvp.Key
                },
                {
                    "Year", int.Parse(kvp.Value)
                },
                {
                    "UpdatedBy", UserId
                }
            }.ToJson();
            api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
            {
                RequestUri = new Uri($"/api/financial-plan/{kvp.Key}/{kvp.Value}", UriKind.Relative),
                Method = HttpMethod.Put,
                Content = new StringContent(json, Encoding.UTF8, "application/json")
            });
            await WhenISubmitTheFinancialPlansRequest();
        }

        api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plans?urns={urn}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    [When("I submit the financial plans request")]
    [When("I submit the financial plan deployment request")]
    public async Task WhenISubmitTheFinancialPlansRequest()
    {
        await api.Send();
    }

    [Then("the financial plans response for school id '(.*)' in year '(.*)' should contain:")]
    private async Task ThenTheFinancialPlansResponseForSchoolIdInYearShouldContain(string urn, string year, Table table)
    {
        var response = api[FinancialPlansKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<FinancialPlanDetails>();

        var yearParsed = int.Parse(year);
        Assert.Equal(GetJsonFromTable(urn, yearParsed, table), result.ToJson());
    }

    [Then("the financial plans response should return created or no content")]
    private void ThenTheFinancialPlansResponseShouldReturnCreatedOrNoContent()
    {
        var response = api[FinancialPlansKey].Response;
        response.StatusCode.Should().BeOneOf(HttpStatusCode.Created, HttpStatusCode.NoContent);
    }

    [Then("the financial plans response should return ok")]
    private void ThenTheFinancialPlansResponseShouldReturnOk()
    {
        var response = api[FinancialPlansKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.OK);
    }

    [Then("the financial plans response should return internal server error")]
    private void ThenTheFinancialPlansResponseShouldReturnInternalServerError()
    {
        var response = api[FinancialPlansKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.InternalServerError);
    }

    [Then("the financial plan deployment response should return not found")]
    private void ThenTheFinancialPlanDeploymentResponseShouldReturnNotFound()
    {
        var response = api[FinancialPlansKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.NotFound);
    }

    [Then("the financial plans response should contain:")]
    private async Task ThenTheFinancialPlansResponseShouldContain(Table table)
    {
        var response = api[FinancialPlansKey].Response;
        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<IEnumerable<FinancialPlanSummary>>().ToArray();

        var row = Assert.Single(result);
        var tableRow = table.Rows.ElementAt(0).Values;
        Assert.Equal(tableRow.ElementAt(0), row.Urn);
        Assert.Equal(tableRow.ElementAt(1), row.Year.ToString());
        Assert.Equal(UserId, row.UpdatedBy);
        Assert.InRange(row.UpdatedAt!.Value, DateTimeOffset.Now.AddMinutes(-1), DateTimeOffset.Now.AddMinutes(1));
        Assert.False(row.IsComplete);
    }

    private void GetFinancialPlansRequest(string urn, int year)
    {
        api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{urn}/{year}", UriKind.Relative),
            Method = HttpMethod.Get
        });
    }

    private void PutFinancialPlansRequest(string urn, int year, Table table)
    {
        var json = GetJsonFromTable(urn, year, table);

        api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{urn}/{year}", UriKind.Relative),
            Method = HttpMethod.Put,
            Content = new StringContent(json, Encoding.UTF8, "application/json")
        });
    }

    private void DeleteFinancialPlansRequest(string urn, int year)
    {
        api.CreateRequest(FinancialPlansKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/financial-plan/{urn}/{year}", UriKind.Relative),
            Method = HttpMethod.Delete
        });
    }

    private static string GetJsonFromTable(string urn, int year, Table table)
    {
        var content = new Dictionary<string, object>
        {
            {
                "Year", year
            },
            {
                "Urn", urn
            },
            {
                "UpdatedBy", UserId
            }
        };
        foreach (var row in table.Rows)
        {
            var property = row.Values.ElementAt(0);
            var value = row.Values.ElementAt(1);
            if (decimal.TryParse(value, out var decimalValue))
            {
                content.Add(property, decimalValue);
            }
            else if (bool.TryParse(value, out var boolValue))
            {
                content.Add(property, boolValue);
            }
            else if (value == "[]")
            {
                content.Add(property, Array.Empty<string>());
            }
            else
            {
                content.Add(property, value);
            }
        }

        return content.ToJson();
    }
}