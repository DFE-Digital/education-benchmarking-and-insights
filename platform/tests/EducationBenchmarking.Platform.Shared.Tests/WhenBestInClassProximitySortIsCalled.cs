using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    // behaviour
    // with baseline the sort method should first order the list based on the absolute diffence between x and baseline lowest to highest
    // based on Pool it will then take a subset from start index and reorder those
    // first
    // if OverallPhase == "Secondary" or "All-through" sorted by Progress8Measure 
    // else by Ks2Progress
    // and return just the subset ordered descending as above
    public class WhenBestInClassProximitySortIsCalled
    {
        [Theory]
        [ClassData(typeof(BestInClassProximitySortTestData))]
        public void OrderIsCorrectAndLengthMatchesPool(
            BestInClassProximitySort bestInClassProximitySort, 
            List<SchoolTrustFinance> schoolList, 
            List<string> expectedOrder, 
            int expectedCount)
        {
            // Act
            var result = bestInClassProximitySort.Sort(schoolList);

            // Assert
            Assert.Equal(expectedOrder, result.Select(school => school.SchoolName));
            Assert.Equal(expectedCount, result.Count());
        }
    }


    public class BestInClassProximitySortTestData : IEnumerable<object[]>
    {
        public IEnumerator<object[]> GetEnumerator()
        {
            yield return new object[]
            {
                // set up bestInClassProximitySort
                new BestInClassProximitySort
                {
                    SortBy = "OtherIncome",
                    Baseline = 100,
                    Pool = 6
                },
                // set up schoolList
                new List<SchoolTrustFinance>
                {
                    new SchoolTrustFinance { SchoolName = "6", OtherIncome = 8m, OverallPhase = "Secondary", Progress8Measure = 5m },
                    new SchoolTrustFinance { SchoolName = "5", OtherIncome = 9m, OverallPhase = "All-through", Progress8Measure = 6m },
                    new SchoolTrustFinance { SchoolName = "4", OtherIncome = 10m, OverallPhase = "Secondary", Progress8Measure = 7m },
                    new SchoolTrustFinance { SchoolName = "3", OtherIncome = 7m, OverallPhase = "Primary", Ks2Progress = 8m },
                    new SchoolTrustFinance { SchoolName = "2", OtherIncome = 6m, OverallPhase = "Primary", Ks2Progress = 9m },
                    new SchoolTrustFinance { SchoolName = "1", OtherIncome = 5m, OverallPhase = "Primary", Ks2Progress = 10m },
                    new SchoolTrustFinance { SchoolName = "7", OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new SchoolTrustFinance { SchoolName = "8",  OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
                },
                // set up expectedOrder
                new List<string> { "1", "2", "3", "4", "5", "6" },
                // set up expectedCount
                6
            };


            yield return new object[]
            {
                // set up bestInClassProximitySort
                new BestInClassProximitySort
                {
                    SortBy = "OtherIncome",
                    Baseline = 11,
                    Pool = 2
                },
                // set up schoolList
                new List<SchoolTrustFinance>
                {
                    new SchoolTrustFinance { SchoolName = "1", OtherIncome = 20m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new SchoolTrustFinance { SchoolName = "2", OtherIncome = 21m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new SchoolTrustFinance { SchoolName = "3", OtherIncome = 22m, OverallPhase = "Secondary", Progress8Measure = 1m },
                    new SchoolTrustFinance { SchoolName = "4", OtherIncome = 23m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new SchoolTrustFinance { SchoolName = "5", OtherIncome = 40m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new SchoolTrustFinance { SchoolName = "6", OtherIncome = 50m, OverallPhase = "Primary", Ks2Progress = 1m },
                    new SchoolTrustFinance { SchoolName = "7", OtherIncome = 10m, OverallPhase = "Primary", Ks2Progress = 9m },
                    new SchoolTrustFinance { SchoolName = "8",  OtherIncome = 9m, OverallPhase = "Primary", Ks2Progress =10m },
                },
                // set up expectedOrder
                new List<string> { "8", "7" },
                // set up expectedCount
                2
            };
        }


        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}
