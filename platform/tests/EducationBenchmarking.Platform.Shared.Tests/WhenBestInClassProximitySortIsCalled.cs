using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        [Fact]
        public void OrderIsCorrectAndLengthMatchesPool()
        {
            // Arrange
            var testBestInClassProximitySort = new BestInClassProximitySort
            {
                SortBy = "OtherIncome",
                Baseline = 100,
                Pool = 6
            };
            var testData = new List<SchoolTrustFinance>
            {
                new SchoolTrustFinance { SchoolName = "6", OtherIncome = 8m, OverallPhase = "Secondary", Progress8Measure = 5m },
                new SchoolTrustFinance { SchoolName = "5", OtherIncome = 9m, OverallPhase = "All-through", Progress8Measure = 6m },
                new SchoolTrustFinance { SchoolName = "4", OtherIncome = 10m, OverallPhase = "Secondary", Progress8Measure = 7m },
                new SchoolTrustFinance { SchoolName = "3", OtherIncome = 7m, OverallPhase = "Primary", Ks2Progress = 8m },
                new SchoolTrustFinance { SchoolName = "2", OtherIncome = 6m, OverallPhase = "Primary", Ks2Progress = 9m },
                new SchoolTrustFinance { SchoolName = "1", OtherIncome = 5m, OverallPhase = "Primary", Ks2Progress = 10m },
                new SchoolTrustFinance { SchoolName = "7", OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
                new SchoolTrustFinance { SchoolName = "8",  OtherIncome = 4m, OverallPhase = "Primary", Ks2Progress = 1m },
            };

            // Act
            var result = testBestInClassProximitySort.Sort(testData);

            // Assert
            // correct order
            Assert.Equal("1", result.ElementAt(0).SchoolName);
            Assert.Equal("2", result.ElementAt(1).SchoolName);
            Assert.Equal("3", result.ElementAt(2).SchoolName);
            Assert.Equal("4", result.ElementAt(3).SchoolName);
            Assert.Equal("5", result.ElementAt(4).SchoolName);
            Assert.Equal("6", result.ElementAt(5).SchoolName);
            // len == Pool
            Assert.Equal(6, result.Count());
        }
    }
}
