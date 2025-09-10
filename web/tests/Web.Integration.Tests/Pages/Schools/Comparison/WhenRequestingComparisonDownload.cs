using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Pages.Schools.Comparison;

public class WhenRequestingComparisonDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly SchoolBenchmarkingWebAppClient _client;
    private readonly SchoolExpenditure[] _schoolExpenditures;

    public WhenRequestingComparisonDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _schoolExpenditures = Fixture.Build<SchoolExpenditure>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "123456")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .With(x => x.Building, ["building"])
            .Create();

        var response = await _client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupExpenditure(_schoolExpenditures)
            .Get(Paths.SchoolComparisonDownload(school.URN!));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        var expectedFileNames = new[]
        {
            "comparison-123456-pupil.csv",
            "comparison-123456-building.csv"
        };
        await foreach (var tuple in response.GetFilesFromZip())
        {
            Assert.Contains(tuple.fileName, expectedFileNames);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "URN,SchoolName,SchoolType,LAName,PeriodCoveredByReturn,TotalPupils,TotalExpenditure,TotalTeachingSupportStaffCosts,TeachingStaffCosts,SupplyTeachingStaffCosts,EducationalConsultancyCosts,EducationSupportStaffCosts,AgencySupplyTeachingStaffCosts,TotalNonEducationalSupportStaffCosts,AdministrativeClericalStaffCosts,AuditorsCosts,OtherStaffCosts,ProfessionalServicesNonCurriculumCosts,TotalEducationalSuppliesCosts,ExaminationFeesCosts,LearningResourcesNonIctCosts,LearningResourcesIctCosts,TotalPremisesStaffServiceCosts,CleaningCaretakingCosts,MaintenancePremisesCosts,OtherOccupationCosts,PremisesStaffCosts,TotalUtilitiesCosts,EnergyCosts,WaterSewerageCosts,AdministrativeSuppliesNonEducationalCosts,TotalGrossCateringCosts,TotalNetCateringCosts,CateringStaffCosts,CateringSuppliesCosts,TotalOtherCosts,GroundsMaintenanceCosts,IndirectEmployeeExpenses,InterestChargesLoanBank,OtherInsurancePremiumsCosts,PrivateFinanceInitiativeCharges,RentRatesCosts,SpecialFacilitiesCosts,StaffDevelopmentTrainingCosts,StaffRelatedInsuranceCosts,SupplyTeacherInsurableCosts,CommunityFocusedSchoolStaff,CommunityFocusedSchoolCosts",
                csvLines.First());
            Assert.Equal(_schoolExpenditures.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "123456";
        var response = await _client
            .SetupComparatorSetApiWithException()
            .Get(Paths.SchoolComparisonDownload(urn));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }
}