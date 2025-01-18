using System.Net;
using System.Text;
using FluentAssertions;
using Platform.Api.Establishment.Features.Schools.Models;
using Platform.Api.Establishment.Features.Schools.Requests;
using Platform.Api.Establishment.Features.Trusts.Models;
using Platform.Api.Establishment.Features.Trusts.Requests;
using Platform.ApiTests.Assist;
using Platform.ApiTests.Drivers;
using Platform.Domain;
using Platform.Json;
using Xunit;

namespace Platform.ApiTests.Steps;

[Binding]
[Scope(Feature = "Establishment Comparators Endpoint Testing")]
public class EstablishmentComparatorsSteps(EstablishmentApiDriver api)
{
    private const string ComparatorSchoolsKey = "comparator-schools";
    private const string ComparatorTrustsKey = "comparator-trusts";

    [Given("a valid comparator schools request for school id '(.*)'")]
    public void GivenAValidComparatorSchoolsRequestForSchoolId(string urn)
    {
        var content = new SchoolComparatorsRequest
        {
            FinanceType = new CharacteristicList
            {
                Values = ["Maintained"]
            },
            TotalPupils = new CharacteristicRange
            {
                From = 100,
                To = 1000
            }
        };

        api.CreateRequest(ComparatorSchoolsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/school/{urn}/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the comparator schools request")]
    public async Task WhenISubmitTheComparatorSchoolsRequest()
    {
        await api.Send();
    }

    [Then("the comparator schools should total '(.*)' and contain:")]
    public async Task ThenTheComparatorSchoolsShouldTotalAndContain(string total, DataTable table)
    {
        var response = api[ComparatorSchoolsKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<SchoolComparators>();

        Assert.Equal(total, result.TotalSchools.ToString());

        var set = new List<dynamic>();
        foreach (var urn in result.Schools)
        {
            set.Add(new
            {
                Urn = urn
            });
        }

        table.CompareToDynamicSet(set, false);
    }

    [Given("a valid comparator trusts request for company number '(.*)'")]
    public void GivenAValidComparatorTrustsRequestForCompanyNumber(string companyNumber)
    {
        var content = new TrustComparatorsRequest
        {
            PhasesCovered = new CharacteristicList
            {
                Values = ["Secondary"]
            },
            TotalPupils = new CharacteristicRange
            {
                From = 500,
                To = 10000
            }
        };

        api.CreateRequest(ComparatorTrustsKey, new HttpRequestMessage
        {
            RequestUri = new Uri($"/api/trust/{companyNumber}/comparators", UriKind.Relative),
            Method = HttpMethod.Post,
            Content = new StringContent(content.ToJson(), Encoding.UTF8, "application/json")
        });
    }

    [When("I submit the comparator trusts request")]
    public async Task WhenISubmitTheComparatorTrustsRequest()
    {
        await api.Send();
    }

    [Then("the comparator trusts should total '(.*)' and contain:")]
    public async Task ThenTheComparatorTrustsShouldTotalAndContain(string total, DataTable table)
    {
        var response = api[ComparatorTrustsKey].Response;

        response.StatusCode.Should().Be(HttpStatusCode.OK);

        var content = await response.Content.ReadAsByteArrayAsync();
        var result = content.FromJson<TrustComparators>();

        Assert.Equal(total, result.TotalTrusts.ToString());

        var set = new List<dynamic>();
        foreach (var companyNumber in result.Trusts)
        {
            set.Add(new
            {
                CompanyNumber = companyNumber
            });
        }

        table.CompareToDynamicSet(set, false);
    }
}