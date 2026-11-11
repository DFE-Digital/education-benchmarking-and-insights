using System.Net;
using AutoFixture;
using Web.App;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenRequestingComparisonDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly SchoolExpenditure[] _schoolExpenditures;

    private const string BaseExpectedColumns =
        "URN,SchoolName,SchoolType,LAName,PeriodCoveredByReturn,TotalPupils,TotalExpenditure,TotalTeachingSupportStaffCosts,TeachingStaffCosts,SupplyTeachingStaffCosts,EducationalConsultancyCosts,EducationSupportStaffCosts,AgencySupplyTeachingStaffCosts,TotalNonEducationalSupportStaffCosts,AdministrativeClericalStaffCosts,AuditorsCosts,OtherStaffCosts,ProfessionalServicesNonCurriculumCosts,TotalEducationalSuppliesCosts,ExaminationFeesCosts,LearningResourcesNonIctCosts,LearningResourcesIctCosts,TotalPremisesStaffServiceCosts,CleaningCaretakingCosts,MaintenancePremisesCosts,OtherOccupationCosts,PremisesStaffCosts,TotalUtilitiesCosts,EnergyCosts,WaterSewerageCosts,AdministrativeSuppliesNonEducationalCosts,TotalGrossCateringCosts,TotalNetCateringCosts,CateringStaffCosts,CateringSuppliesCosts,TotalOtherCosts,GroundsMaintenanceCosts,IndirectEmployeeExpenses,InterestChargesLoanBank,OtherInsurancePremiumsCosts,PrivateFinanceInitiativeCharges,RentRatesCosts,SpecialFacilitiesCosts,StaffDevelopmentTrainingCosts,StaffRelatedInsuranceCosts,SupplyTeacherInsurableCosts,CommunityFocusedSchoolStaff,CommunityFocusedSchoolCosts";

    public WhenRequestingComparisonDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _schoolExpenditures = [.. Fixture.Build<SchoolExpenditure>().CreateMany()];
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task CanReturnOkForDefaultComparatorSet(bool ks4ProgressBandingEnabled, bool attainment8Enabled)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorUrns = _schoolExpenditures.Select(x => x.URN!).ToArray();

        var characteristics = _schoolExpenditures
            .Select((e, i) => Fixture.Build<SchoolCharacteristic>()
                .With(x => x.URN, e.URN)
                .With(x => x.KS4ProgressBanding, "Well above average")
                .With(x => x.Attainment8, attainment8Enabled ? 50.0m + i : null)
                .Create())
            .ToArray();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, comparatorUrns)
            .With(x => x.Building, comparatorUrns)
            .Create();

        var disabledFeatures = new List<string>();
        if (!ks4ProgressBandingEnabled)
        {
            disabledFeatures.Add(FeatureFlags.KS4ProgressBanding);
        }
        if (!attainment8Enabled)
        {
            disabledFeatures.Add(FeatureFlags.Attainment8);
        }

        var response = await _client
            .SetupDisableFeatureFlags([.. disabledFeatures])
            .SetupEstablishment(school)
            .SetupSchool(school, characteristics: characteristics)
            .SetupComparatorSet(school, comparatorSet)
            .SetupExpenditure(school, expenditures: _schoolExpenditures)
            .SetupSchoolInsight(characteristics)
            .Get(Paths.SchoolComparisonDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "comparison-123456-pupil.csv",
            "comparison-123456-building.csv"
        };

        var expectedColumns = BuildExpectedColumns(ks4ProgressBandingEnabled, attainment8Enabled);

        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_schoolExpenditures.Length, csvLines.Length - 1);
        }
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task CanReturnOkForUserDefinedComparatorSet(bool ks4ProgressBandingEnabled, bool attainment8Enabled)
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorUrns = _schoolExpenditures.Select(x => x.URN!).ToArray();

        var characteristics = _schoolExpenditures
            .Select((e, i) => Fixture.Build<SchoolCharacteristic>()
                .With(x => x.URN, e.URN)
                .With(x => x.KS4ProgressBanding, "Well above average")
                .With(x => x.Attainment8, attainment8Enabled ? 50.0m + i : null)
                .Create())
            .ToArray();

        var comparatorSet = Fixture.Build<UserDefinedSchoolComparatorSet>()
            .With(x => x.Set, comparatorUrns)
            .Create();

        var userData = new[]
        {
            new UserData
            {
                Type = "comparator-set",
                Id = "Id"
            }
        };

        var disabledFeatures = new List<string>();
        if (!ks4ProgressBandingEnabled)
        {
            disabledFeatures.Add(FeatureFlags.KS4ProgressBanding);
        }
        if (!attainment8Enabled)
        {
            disabledFeatures.Add(FeatureFlags.Attainment8);
        }

        var response = await _client
            .SetupDisableFeatureFlags([.. disabledFeatures])
            .SetupEstablishment(school)
            .SetupSchool(school, characteristics: characteristics)
            .SetupComparatorSet(school, comparatorSet)
            .SetupExpenditure(school, expenditures: _schoolExpenditures)
            .SetupSchoolInsight(characteristics)
            .SetupUserData(userData)
            .Get(Paths.SchoolComparisonDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "comparison-123456-pupil.csv",
            "comparison-123456-building.csv"
        };

        var expectedColumns = BuildExpectedColumns(ks4ProgressBandingEnabled, attainment8Enabled);

        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(_schoolExpenditures.Length, csvLines.Length - 1);
        }
    }

    [Theory]
    [InlineData(false, false)]
    [InlineData(true, false)]
    [InlineData(false, true)]
    public async Task CanReturnOkForCustomDataComparatorSet(bool ks4ProgressBandingEnabled, bool attainment8Enabled)
    {
        const string customDataId = nameof(customDataId);
        const string schoolUrn = "123456";
        var school = Fixture.Build<School>()
            .With(x => x.URN, schoolUrn)
            .Create();

        var comparatorUrns = _schoolExpenditures.Select(x => x.URN!).ToArray();

        var userDefinedComparatorSet = Fixture.Build<UserDefinedSchoolComparatorSet>()
            .With(x => x.Set, comparatorUrns)
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, comparatorUrns)
            .With(x => x.Building, comparatorUrns)
            .Create();

        var customDataExpenditure = Fixture.Build<SchoolExpenditure>()
            .With(x => x.URN, schoolUrn)
            .Create();

        var characteristics = _schoolExpenditures
            .Select((e, i) => Fixture.Build<SchoolCharacteristic>()
                .With(x => x.URN, e.URN)
                .With(x => x.KS4ProgressBanding, "Well above average")
                .With(x => x.Attainment8, attainment8Enabled ? 50.0m + i : null)
                .Create())
            .ToArray();

        var disabledFeatures = new List<string>();
        if (!ks4ProgressBandingEnabled)
        {
            disabledFeatures.Add(FeatureFlags.KS4ProgressBanding);
        }
        if (!attainment8Enabled)
        {
            disabledFeatures.Add(FeatureFlags.Attainment8);
        }

        var response = await _client
            .SetupDisableFeatureFlags([.. disabledFeatures])
            .SetupEstablishment(school)
            .SetupSchool(school, characteristics: characteristics)
            .SetupCustomComparatorSet(school, comparatorSet, userDefinedComparatorSet)
            .SetupExpenditure(
                school,
                expenditures: _schoolExpenditures,
                customDataExpenditure: customDataExpenditure,
                customDataIdentifier: customDataId)
            .SetupSchoolInsight(characteristics)
            .Get(Paths.SchoolComparisonDownload(school.URN!, customDataId));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "comparison-123456-pupil.csv",
            "comparison-123456-building.csv"
        };

        var expectedColumns = BuildExpectedColumns(ks4ProgressBandingEnabled, attainment8Enabled);

        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            // expenditures + custom data
            var expectedLength = _schoolExpenditures.Length + 1;

            Assert.Equal(expectedColumns, csvLines.First());
            Assert.Equal(expectedLength, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await _client
            .SetupExpenditureWithException()
            .Get(Paths.SchoolComparisonDownload(urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    private static string BuildExpectedColumns(bool ks4ProgressBandingEnabled, bool attainment8Enabled)
    {
        var columns = BaseExpectedColumns;

        if (attainment8Enabled)
        {
            columns += ",TopRankedAttainment8";
        }

        if (ks4ProgressBandingEnabled)
        {
            columns += ",ProgressBanding";
        }

        return columns;
    }
}
