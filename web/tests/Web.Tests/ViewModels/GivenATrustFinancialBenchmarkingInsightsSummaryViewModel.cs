using System.Collections;
using AutoFixture;
using Web.App.Domain;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.ViewModels;

public class GivenATrustFinancialBenchmarkingInsightsSummaryViewModel
{
    [Theory]
    [ClassData(typeof(PrioritySchoolsTestData))]
    public void GeneratesPrioritySchoolsCorrectly(PrioritySchoolsTestCase testCase)
    {
        var fixture = new Fixture();
        var trust = fixture.Build<Trust>()
            .With(t => t.Schools, [
                new TrustSchool { URN = "100001", SchoolName = "Test School A" },
                new TrustSchool { URN = "100002", SchoolName = "Test School B" },
                new TrustSchool { URN = "100003", SchoolName = "Test School C" }
            ])
            .Create();
        var balance = fixture.Create<TrustBalance>();

        var vm = new TrustFinancialBenchmarkingInsightsSummaryViewModel(trust, balance, testCase.Ratings);

        var actual = vm.PrioritySchools.ToList();

        Assert.Equal(2, actual.Count);

        AssertSchool(actual[0], testCase.First);
        AssertSchool(actual[1], testCase.Second);
    }

    private void AssertSchool(RagSchoolSummaryViewModel actual, ExpectedSchool expected)
    {
        Assert.Equal(expected.Urn, actual.Urn);
        Assert.Equal(expected.Name, actual.Name);
        Assert.Equal(expected.Red, actual.Red);
        Assert.Equal(expected.Amber, actual.Amber);
        Assert.Equal(expected.Green, actual.Green);
        Assert.Contains(actual.TopCategories, c =>
            c.Category == expected.TopCategory1.Category &&
            c.Value == expected.TopCategory1.Value &&
            c.Unit == expected.TopCategory1.Unit);
        Assert.Contains(actual.TopCategories, c =>
            c.Category == expected.TopCategory2.Category &&
            c.Value == expected.TopCategory2.Value &&
            c.Unit == expected.TopCategory2.Unit);
    }

    public record ExpectedSchool(
        string Urn,
        string Name,
        int Red,
        int Amber,
        int Green,
        RagCategorySummary TopCategory1,
        RagCategorySummary TopCategory2
    );

    public record PrioritySchoolsTestCase(
        List<RagRating> Ratings,
        ExpectedSchool First,
        ExpectedSchool Second,
        string Scenario
    )
    {
        public override string ToString() => Scenario;
    }


    private class PrioritySchoolsTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new PrioritySchoolsTestCase(
                    Ratings: new List<RagRating> {
                        new() { URN = "100001", RAG = "red", Category = Category.AdministrativeSupplies, Value = 1000 },
                        new() { URN = "100001", RAG = "red", Category = Category.EducationalIct, Value = 2000 },
                        new() { URN = "100001", RAG = "red", Category = Category.CateringStaffServices, Value = 3000 },

                        new() { URN = "100002", RAG = "red", Category = Category.EducationalSupplies, Value = 4000 },
                        new() { URN = "100002", RAG = "red", Category = Category.TeachingStaff, Value = 5000 },
                        new() { URN = "100002", RAG = "red", Category = Category.PremisesStaffServices, Value = 6000 },

                        new() { URN = "100003", RAG = "red", Category = Category.EducationalIct, Value = 1000 },
                        new() { URN = "100003", RAG = "red", Category = Category.TeachingStaff, Value = 8000 },
                        new() { URN = "100003", RAG = "red", Category = Category.Utilities, Value = 9000 }
                    },
                    First: new ExpectedSchool("100001", "Test School A", 3, 0, 0,
                        new RagCategorySummary { Category = Category.CateringStaffServices, Value = 3000, Unit = "per pupil" },
                        new RagCategorySummary { Category = Category.EducationalIct, Value = 2000, Unit = "per pupil" }),
                    Second: new ExpectedSchool("100002", "Test School B", 3, 0, 0,
                        new RagCategorySummary { Category = Category.PremisesStaffServices, Value = 6000, Unit = "per square metre" },
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 5000, Unit = "per pupil" }),
                    Scenario: "All red, tie break on name"
                )
            };
            yield return new object[]
            {
                new PrioritySchoolsTestCase(
                    Ratings: new List<RagRating> {
                        new() { URN = "100001", RAG = "green", Category = Category.AdministrativeSupplies, Value = 1000 },
                        new() { URN = "100001", RAG = "green", Category = Category.EducationalIct, Value = 2000 },
                        new() { URN = "100001", RAG = "amber", Category = Category.CateringStaffServices, Value = 3000 },

                        new() { URN = "100002", RAG = "red", Category = Category.EducationalSupplies, Value = 1000 },
                        new() { URN = "100002", RAG = "amber", Category = Category.TeachingStaff, Value = 2000 },
                        new() { URN = "100002", RAG = "green", Category = Category.PremisesStaffServices, Value = 3000 },

                        new() { URN = "100003", RAG = "amber", Category = Category.EducationalIct, Value = 1000 },
                        new() { URN = "100003", RAG = "amber", Category = Category.AdministrativeSupplies, Value = 5000 },
                        new() { URN = "100003", RAG = "red", Category = Category.TeachingStaff, Value = 9000 }
                    },
                    First: new ExpectedSchool("100003", "Test School C", 1, 2, 0,
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 9000, Unit = "per pupil" },
                        new RagCategorySummary { Category = Category.AdministrativeSupplies, Value = 5000, Unit = "per pupil" }),
                    Second: new ExpectedSchool("100002", "Test School B", 1, 1, 1,
                        new RagCategorySummary { Category = Category.PremisesStaffServices, Value = 3000, Unit = "per square metre" },
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 2000, Unit = "per pupil" }),
                    Scenario: "Mixed, tie break on amber count"
                    )
            };
            yield return new object[]
            {
                new PrioritySchoolsTestCase(
                    Ratings: new List<RagRating> {

                        new() { URN = "100001", RAG = "red", Category = Category.EducationalIct, Value = 1000 },
                        new() { URN = "100001", RAG = "amber", Category = Category.AdministrativeSupplies, Value = 5000 },
                        new() { URN = "100001", RAG = "green", Category = Category.TeachingStaff, Value = 9000 },

                        new() { URN = "100002", RAG = "red", Category = Category.EducationalSupplies, Value = 1000 },
                        new() { URN = "100002", RAG = "red", Category = Category.TeachingStaff, Value = 2000 },
                        new() { URN = "100002", RAG = "green", Category = Category.PremisesStaffServices, Value = 5000 },

                        new() { URN = "100003", RAG = "green", Category = Category.Utilities, Value = 5000 },
                        new() { URN = "100003", RAG = "green", Category = Category.EducationalIct, Value = 5000 },
                        new() { URN = "100003", RAG = "red", Category = Category.AdministrativeSupplies, Value = 5000 }
                    },
                    First: new ExpectedSchool("100002", "Test School B", 2, 0, 1,
                        new RagCategorySummary { Category = Category.PremisesStaffServices, Value = 5000, Unit = "per square metre" },
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 2000, Unit = "per pupil" }),
                    Second: new ExpectedSchool("100001", "Test School A", 1, 1, 1,
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 9000, Unit = "per pupil" },
                        new RagCategorySummary { Category = Category.AdministrativeSupplies, Value = 5000, Unit = "per pupil" }),
                    Scenario: "Mixed, green values highest spend"
                )
            };
            yield return new object[]
            {
                new PrioritySchoolsTestCase(
                    Ratings: new List<RagRating> {

                        new() { URN = "100001", RAG = "green", Category = Category.EducationalIct, Value = 1000 },
                        new() { URN = "100001", RAG = "red", Category = Category.AdministrativeSupplies, Value = 5000 },
                        new() { URN = "100001", RAG = "red", Category = Category.Other, Value = 9000 },

                        new() { URN = "100002", RAG = "red", Category = Category.EducationalSupplies, Value = 1000 },
                        new() { URN = "100002", RAG = "red", Category = Category.TeachingStaff, Value = 2000 },
                        new() { URN = "100002", RAG = "green", Category = Category.PremisesStaffServices, Value = 5000 },

                        new() { URN = "100003", RAG = "green", Category = Category.Utilities, Value = 3000 },
                        new() { URN = "100003", RAG = "amber", Category = Category.EducationalIct, Value = 5000 },
                        new() { URN = "100003", RAG = "red", Category = Category.AdministrativeSupplies, Value = 6000 }
                    },
                    First: new ExpectedSchool("100002", "Test School B", 2, 0, 1,
                        new RagCategorySummary { Category = Category.PremisesStaffServices, Value = 5000, Unit = "per square metre" },
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 2000, Unit = "per pupil" }),
                    Second: new ExpectedSchool("100003", "Test School C", 1, 1, 1,
                        new RagCategorySummary { Category = Category.AdministrativeSupplies, Value = 6000, Unit = "per pupil" },
                        new RagCategorySummary { Category = Category.EducationalIct, Value = 5000, Unit = "per pupil" }),
                    Scenario: "Mixed, other category is ignored for priority school ranking"
                )
            };
            yield return new object[]
            {
                new PrioritySchoolsTestCase(
                    Ratings: new List<RagRating> {

                        new() { URN = "100001", RAG = "red", Category = Category.EducationalIct, Value = 1000 },
                        new() { URN = "100001", RAG = "red", Category = Category.AdministrativeSupplies, Value = 5000 },
                        new() { URN = "100001", RAG = "green", Category = Category.Other, Value = 9000 },

                        new() { URN = "100002", RAG = "red", Category = Category.EducationalSupplies, Value = 1000 },
                        new() { URN = "100002", RAG = "amber", Category = Category.TeachingStaff, Value = 2000 },
                        new() { URN = "100002", RAG = "green", Category = Category.PremisesStaffServices, Value = 5000 },

                        new() { URN = "100003", RAG = "green", Category = Category.Utilities, Value = 3000 },
                        new() { URN = "100003", RAG = "green", Category = Category.EducationalIct, Value = 5000 },
                        new() { URN = "100003", RAG = "green", Category = Category.AdministrativeSupplies, Value = 6000 }
                    },
                    First: new ExpectedSchool("100001", "Test School A", 2, 0, 0,
                        new RagCategorySummary { Category = Category.AdministrativeSupplies, Value = 5000, Unit = "per pupil" },
                        new RagCategorySummary { Category = Category.EducationalIct, Value = 1000, Unit = "per pupil" }),
                    Second: new ExpectedSchool("100002", "Test School B", 1, 1, 1,
                        new RagCategorySummary { Category = Category.PremisesStaffServices, Value = 5000, Unit = "per square metre" },
                        new RagCategorySummary { Category = Category.TeachingStaff, Value = 2000, Unit = "per pupil" }),
                    Scenario: "Other category is ignored for top categories ranking and excluded from count"
                )
            };
        }
        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}