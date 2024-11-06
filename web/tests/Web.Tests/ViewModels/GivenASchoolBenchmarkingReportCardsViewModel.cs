using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;
namespace Web.Tests.ViewModels;

public class GivenASchoolBenchmarkingReportCardsViewModel
{
    private const string URN = nameof(URN);
    private readonly SchoolBalance _balance;
    private readonly School _school;
    private readonly FinanceYears _years;

    public GivenASchoolBenchmarkingReportCardsViewModel()
    {
        var fixture = new Fixture();
        _school = fixture
            .Build<School>()
            .With(s => s.URN, URN)
            .Create();
        _years = new FinanceYears { Aar = 2020, Cfr = 2024 };
        _balance = fixture.Create<SchoolBalance>();
    }

    public static TheoryData<
        IEnumerable<RagRating>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<CostCategory>
    > WhenRagRatingsAreData
    {
        get
        {
            var administrativeSuppliesRagRating = new RagRating
            {
                Category = Category.AdministrativeSupplies,
                RAG = "red"
            };

            var teachingStaffRagRating = new RagRating
            {
                Category = Category.TeachingStaff,
                RAG = "amber"
            };

            var otherRagRating = new RagRating
            {
                Category = Category.Other,
                RAG = "amber"
            };

            var nonEducationalSupportStaffRagRating = new RagRating
            {
                Category = Category.NonEducationalSupportStaff,
                RAG = "green"
            };

            var educationalIctRagRating = new RagRating
            {
                Category = Category.EducationalIct,
                RAG = "green"
            };

            return new TheoryData<
                IEnumerable<RagRating>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<CostCategory>
            >
            {
                {
                    [administrativeSuppliesRagRating, teachingStaffRagRating, nonEducationalSupportStaffRagRating, educationalIctRagRating, otherRagRating], [
                        new SchoolExpenditure
                        {
                            URN = URN
                        }
                    ],
                    [
                        new SchoolExpenditure
                        {
                            URN = URN
                        }
                    ],
                    [new AdministrativeSupplies(administrativeSuppliesRagRating), new TeachingStaff(teachingStaffRagRating), new NonEducationalSupportStaff(nonEducationalSupportStaffRagRating)]
                }
            };
        }
    }

    [Theory]
    [MemberData(nameof(WhenRagRatingsAreData))]
    public void WhenRagRatingsAre(
        IEnumerable<RagRating> ratings,
        IEnumerable<SchoolExpenditure> pupilExpenditure,
        IEnumerable<SchoolExpenditure> areaExpenditure,
        IEnumerable<CostCategory> expectedCostsAllSchools)
    {
        var actual = new SchoolBenchmarkingReportCardsViewModel(_school, _years, _balance, ratings, pupilExpenditure, areaExpenditure);
        Assert.Equal(expectedCostsAllSchools.Select(c => c.Rating), actual.CostsAllSchools.Select(c => c.Rating));
    }

    [Theory]
    [InlineData(true, "2019 - 2020")]
    [InlineData(false, "2023 - 2024")]
    public void WhenIsPartOfTrust(bool isPartOfTrust, string expected)
    {
        _school.TrustCompanyNumber = isPartOfTrust ? nameof(isPartOfTrust) : string.Empty;
        var actual = new SchoolBenchmarkingReportCardsViewModel(_school, _years, _balance, Array.Empty<RagRating>(), Array.Empty<SchoolExpenditure>(), Array.Empty<SchoolExpenditure>());
        Assert.Equal(expected, actual.Years);
    }
}