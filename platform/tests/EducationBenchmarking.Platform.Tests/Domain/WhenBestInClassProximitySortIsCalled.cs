using System.Collections;
using EducationBenchmarking.Platform.Domain;
using FluentAssertions;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Domain
{
    public class WhenBestInClassProximitySortIsCalled
    {
        [Theory]
        [ClassData(typeof(ProximitySortTestData))]
        public void OrderIsCorrectAndLengthMatchesPool(
            BestInClassProximitySort proximitySort, 
            List<SchoolTrustFinance> schools, 
            List<string> expectedOrder, 
            int expectedCount)
        {
            var result = proximitySort.Sort(schools).ToArray();
            
            proximitySort.Kind.Should().Be(ProximitySortKinds.Bic);
            
            Assert.Equal(expectedOrder, result.Select(school => school.SchoolName));
            Assert.Equal(expectedCount, result.Length);
        }
    }


    public class ProximitySortTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                new BestInClassProximitySort
                {
                    SortBy = "OtherIncome",
                    Baseline = 100,
                    Pool = 6
                },
                new List<SchoolTrustFinance>
                {
                    new() { SchoolName = "6", OtherIncome = 8m, OverallPhase = "Secondary", Progress8Measure = 5m },
                    new() { SchoolName = "5", OtherIncome = 9m, OverallPhase = "All-through", Progress8Measure = 6m },
                    new() { SchoolName = "4", OtherIncome = 10m, OverallPhase = "Secondary", Progress8Measure = 7m },
                    new() { SchoolName = "3", OtherIncome = 7m, OverallPhase = "Primary", Ks2Progress = 8m },
                    new() { SchoolName = "2", OtherIncome = 6m, OverallPhase = "Primary", Ks2Progress = 9m },
                    new() { SchoolName = "1", OtherIncome = 5m, OverallPhase = "Primary", Ks2Progress = 10m },
                    new() { SchoolName = "7", OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new() { SchoolName = "8",  OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
                },
                new List<string> { "1", "2", "3", "4", "5", "6" },
                6
            };


            yield return new object[]
            {
                new BestInClassProximitySort
                {
                    SortBy = "OtherIncome",
                    Baseline = 11,
                    Pool = 2
                },
                new List<SchoolTrustFinance>
                {
                    new() { SchoolName = "1", OtherIncome = 20m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new() { SchoolName = "2", OtherIncome = 21m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new() { SchoolName = "3", OtherIncome = 22m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new() { SchoolName = "4", OtherIncome = 23m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new() { SchoolName = "5", OtherIncome = 40m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new() { SchoolName = "6", OtherIncome = 50m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new() { SchoolName = "7", OtherIncome = 10m, OverallPhase = "Primary", Ks2Progress = 9m },
                    new() { SchoolName = "8",  OtherIncome = 9m, OverallPhase = "Primary", Ks2Progress =10m },
                },
                new List<string> { "8", "7" },
                2
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
