using System.Net;
using AutoFixture;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Xunit;

namespace Web.Integration.Tests.Pages.LocalAuthorities;

public class WhenRequestingHighNeedsSpendingDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly HighNeedsSpending[] _expenditures;

    public WhenRequestingHighNeedsSpendingDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _expenditures = Fixture.Build<HighNeedsSpending>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var authority = Fixture.Build<Web.App.Domain.LocalAuthorities.LocalAuthority>()
            .With(x => x.Code, "123")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(c => c.Pupil, Fixture.CreateMany<string>().ToArray())
            .Without(c => c.Building)
            .Create();

        Assert.NotNull(authority.Code);
        var response = await _client
                .SetupLocalAuthorityEndpoints(authority, highNeedsSpendings: _expenditures)
                .SetupLocalAuthoritiesComparators(authority.Code, ["123", "124"])
                .Get(Paths.LocalAuthorityHighNeedsSpendingDownload(authority.Code));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "benchmark-high-needs-spending-outturn-per-pupil-123.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "Code,Name,TotalPupils,TotalPlaceFunding,TotalTopUpFundingMaintained,TotalTopUpFundingNonMaintained,TotalSenServices,TotalAlternativeProvisionServices,TotalHospitalServices,TotalOtherHealthServices,TopFundingMaintainedEarlyYears,TopFundingMaintainedPrimary,TopFundingMaintainedSecondary,TopFundingMaintainedSpecial,TopFundingMaintainedAlternativeProvision,TopFundingMaintainedPostSchool,TopFundingMaintainedIncome,TopFundingNonMaintainedEarlyYears,TopFundingNonMaintainedPrimary,TopFundingNonMaintainedSecondary,TopFundingNonMaintainedSpecial,TopFundingNonMaintainedAlternativeProvision,TopFundingNonMaintainedPostSchool,TopFundingNonMaintainedIncome,PlaceFundingPrimary,PlaceFundingSecondary,PlaceFundingSpecial,PlaceFundingAlternativeProvision,SenTransportDsg,HometoSchoolTransportPre16,HometoSchoolTransport1618,HometoSchoolTransport1925,EdPsychologyService,SenAdmin",
                csvLines.First());
            Assert.Equal(_expenditures.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string code = "123";
        var response = await _client
            .SetupEstablishmentWithException()
            .Get(Paths.LocalAuthorityHighNeedsSpendingDownload(code));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}
