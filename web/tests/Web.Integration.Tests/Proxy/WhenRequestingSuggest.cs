using Newtonsoft.Json.Linq;
using System.Net;
using Web.App.Infrastructure.Apis;
using Xunit;
using Web.App.Domain;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingSuggest(SchoolBenchmarkingWebAppClient client) : IClassFixture<SchoolBenchmarkingWebAppClient>
{
    [Theory]
    [InlineData("school")]
    [InlineData("trust")]
    [InlineData("local-authority")]
    public async Task CanReturnInternalServerError(string suggestType)
    {
        var response = await client.SetupEstablishmentWithException()
            .Get(Paths.ApiSuggest("12323", suggestType));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    [Theory]
    [MemberData(nameof(TrustTestData))]
    public async Task CanReturnCorrectResponseForTrust(string search, SuggestOutput<Trust> trustTestData, SuggestOutput<Trust> expected)
    {
        var response = await client.SetupEstablishment(trustTestData)
            .Get(Paths.ApiSuggest(search, "trust"));

        Assert.IsType<HttpResponseMessage>(response);
        Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        var resultContent = await response.Content.ReadAsStringAsync();

        var resultArray = JArray.Parse(resultContent);

        Assert.Equal(resultArray.Count, expected.Results.Count());

        foreach (var expectedValue in expected.Results)
        {
            var found = false;
            var expectedText = expectedValue.Text;
            var expectedCompanyNumber = expectedValue.Document?.CompanyNumber;
            var expectedName = expectedValue.Document?.TrustName;

            foreach (var resultObj in resultArray)
            {
                var actualText = resultObj["text"]?.ToString();
                var actualCompanyNumber = resultObj["document"]?["companyNumber"]?.ToString();
                var actualName = resultObj["document"]?["trustName"]?.ToString();

                if (actualText == expectedText &&
                    actualCompanyNumber == expectedCompanyNumber &&
                    actualName == expectedName)
                {
                    found = true;
                    break;
                }
            }

            Assert.True(found);
        }
    }

    public static IEnumerable<object[]> TrustTestData =>
        new List<object[]>
        {
            new object[]
            {
                "Test",
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                        new SuggestValue<Trust>
                        {
                            Text = "*Test* Trust",
                            Document = new Trust
                            {
                                CompanyNumber = "12345678",
                                TrustName = "Test Trust"
                            }
                        }
                    ]
                },
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                        new SuggestValue<Trust>
                         {
                             Text = "*Test* Trust (12345678)",
                             Document = new Trust
                             {
                                 CompanyNumber = "12345678",
                                 TrustName = "Test Trust"
                             }
                         }
                    ]
                }
            },
            new object[]
            {
                "123",
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                        new SuggestValue<Trust>
                        {
                            Text = "*123*45678",
                            Document = new Trust
                            {
                                CompanyNumber = "12345678",
                                TrustName = "Test Trust"
                            }
                        }
                    ]
                },
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                    new SuggestValue<Trust>
                     {
                         Text = "Test Trust (*123*45678)",
                         Document = new Trust
                         {
                             CompanyNumber = "12345678",
                             TrustName = "Test Trust"
                         }
                     }
                    ]
                }
            },
            new object[]
            {
                "Test",
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                        new SuggestValue<Trust>
                        {
                            Text = "*Test* Trust",
                            Document = new Trust
                            {
                                CompanyNumber = "12345678",
                                TrustName = "Test Trust"
                            }
                        },
                        new SuggestValue<Trust>
                        {
                            Text = "Another *Test* Trust",
                            Document = new Trust
                            {
                                CompanyNumber = "87654321",
                                TrustName = "Another Test Trust"
                            }
                        },

                    ]
                },
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                         new SuggestValue<Trust>
                         {
                             Text = "*Test* Trust (12345678)",
                             Document = new Trust
                             {
                                 CompanyNumber = "12345678",
                                 TrustName = "Test Trust"
                             }
                         },
                         new SuggestValue<Trust>
                         {
                             Text = "Another *Test* Trust (87654321)",
                             Document = new Trust
                             {
                                 CompanyNumber = "87654321",
                                 TrustName = "Another Test Trust"
                             }
                         }
                     ]
                }
            },
            new object[]
            {
                "123",
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                        new SuggestValue<Trust>
                        {
                            Text = "*123*45678",
                            Document = new Trust
                            {
                                CompanyNumber = "12345678",
                                TrustName = "Test Trust"
                            }
                        },
                        new SuggestValue<Trust>
                        {
                            Text = "*123*45679",
                            Document = new Trust
                            {
                                CompanyNumber = "12345679",
                                TrustName = "Another Test Trust"
                            }
                        },
                    ]
                },
                new SuggestOutput<Trust>
                {
                    Results =
                    [
                         new SuggestValue<Trust>
                         {
                             Text = "Test Trust (*123*45678)",
                             Document = new Trust
                             {
                                 CompanyNumber = "12345678",
                                 TrustName = "Test Trust"
                             }
                         },
                         new SuggestValue<Trust>
                         {
                             Text = "Another Test Trust (*123*45679)",
                             Document = new Trust
                             {
                                 CompanyNumber = "12345679",
                                 TrustName = "Another Test Trust"
                             }
                         }
                    ]
                }
            },
            new object[]
            {
                "Test",
                new SuggestOutput<Trust>
                {
                    Results = []
                },
                new SuggestOutput<Trust>
                {
                    Results = []
                },
            },
        };
}