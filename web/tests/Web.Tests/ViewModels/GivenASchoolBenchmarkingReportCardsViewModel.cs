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
        _years = new FinanceYears
        {
            Aar = 2020,
            Cfr = 2024
        };
        _balance = fixture.Create<SchoolBalance>();
    }

    public static TheoryData<
        IEnumerable<RagRating>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<SchoolExpenditure>,
        IEnumerable<CostCategory>,
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

            var utilitiesRagRating = new RagRating
            {
                Category = Category.Utilities,
                RAG = "red"
            };

            return new TheoryData<
                IEnumerable<RagRating>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<SchoolExpenditure>,
                IEnumerable<CostCategory>,
                IEnumerable<CostCategory>
            >
            {
                {
                    [administrativeSuppliesRagRating, teachingStaffRagRating, nonEducationalSupportStaffRagRating, educationalIctRagRating, otherRagRating, utilitiesRagRating], [
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
                    [new AdministrativeSupplies(administrativeSuppliesRagRating), new TeachingStaff(teachingStaffRagRating), new NonEducationalSupportStaff(nonEducationalSupportStaffRagRating)], [new Utilities(utilitiesRagRating)]
                }
            };
        }
    }

    public static TheoryData<
        IEnumerable<Census>,
        SchoolBenchmarkingReportCardCensusViewModel,
        SchoolBenchmarkingReportCardCensusViewModel
    > WhenCensusesAreData
    {
        get
        {
            var schoolCensus = new Census
            {
                URN = URN,
                Teachers = 100,
                SeniorLeadership = 10
            };

            var minTeachersCensus = new Census
            {
                Teachers = 90,
                SeniorLeadership = 11
            };

            var minSeniorLeadershipCensus = new Census
            {
                Teachers = 101,
                SeniorLeadership = 1
            };

            var maxTeachersCensus = new Census
            {
                Teachers = 200,
                SeniorLeadership = 11
            };

            var maxSeniorLeadershipCensus = new Census
            {
                Teachers = 101,
                SeniorLeadership = 20
            };

            return new TheoryData<
                IEnumerable<Census>,
                SchoolBenchmarkingReportCardCensusViewModel,
                SchoolBenchmarkingReportCardCensusViewModel
            >
            {
                {
                    [minTeachersCensus, maxTeachersCensus, schoolCensus, minSeniorLeadershipCensus, maxSeniorLeadershipCensus], new SchoolBenchmarkingReportCardCensusViewModel("teacher")
                    {
                        SchoolValue = schoolCensus.Teachers,
                        MinValue = minTeachersCensus.Teachers,
                        MaxValue = maxTeachersCensus.Teachers
                    },
                    new SchoolBenchmarkingReportCardCensusViewModel("senior leadership role")
                    {
                        SchoolValue = schoolCensus.SeniorLeadership,
                        MinValue = minSeniorLeadershipCensus.SeniorLeadership,
                        MaxValue = maxSeniorLeadershipCensus.SeniorLeadership
                    }
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
        IEnumerable<CostCategory> expectedCostsAllSchools,
        IEnumerable<CostCategory> expectedCostsOtherPriorities)
    {
        var actual = new SchoolBenchmarkingReportCardsViewModel(_school, _years, _balance, ratings, pupilExpenditure, areaExpenditure, Array.Empty<Census>());
        Assert.Equal(expectedCostsAllSchools.Select(c => c.Rating), actual.CostsAllSchools.Select(c => c.Rating));
        Assert.Equal(expectedCostsOtherPriorities.Select(c => c.Rating), actual.CostsOtherPriorities.Select(c => c.Rating));
    }

    [Theory]
    [InlineData(true, "2019 - 2020")]
    [InlineData(false, "2023 - 2024")]
    public void WhenIsPartOfTrust(bool isPartOfTrust, string expected)
    {
        _school.TrustCompanyNumber = isPartOfTrust ? nameof(isPartOfTrust) : string.Empty;
        var actual = new SchoolBenchmarkingReportCardsViewModel(_school, _years, _balance, Array.Empty<RagRating>(), Array.Empty<SchoolExpenditure>(), Array.Empty<SchoolExpenditure>(), Array.Empty<Census>());
        Assert.Equal(expected, actual.Years);
    }

    [Theory]
    [MemberData(nameof(WhenCensusesAreData))]
    public void WhenCensusesAre(
        IEnumerable<Census> census,
        SchoolBenchmarkingReportCardCensusViewModel expectedPupilsPerTeacher,
        SchoolBenchmarkingReportCardCensusViewModel expectedPupilsPerSeniorLeadership)
    {
        var actual = new SchoolBenchmarkingReportCardsViewModel(_school, _years, _balance, Array.Empty<RagRating>(), Array.Empty<SchoolExpenditure>(), Array.Empty<SchoolExpenditure>(), census);
        Assert.Equivalent(expectedPupilsPerTeacher, actual.PupilsPerTeacher);
        Assert.Equivalent(expectedPupilsPerSeniorLeadership, actual.PupilsPerSeniorLeadership);
    }
}