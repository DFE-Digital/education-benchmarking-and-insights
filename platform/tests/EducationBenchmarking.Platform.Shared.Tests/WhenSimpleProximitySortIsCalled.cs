using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    // behaviour
    // with baseline the sort method should order the list based on the absolute diffence between x and baseline lowest to highest 
    public class WhenSimpleProximitySortIsCalled
    {
        [Fact]
        public void OrderIsCorrect()
        {
            // Arrange
            // set up SimpleProximitySort
            var testSimpleProximitySort = new SimpleProximitySort
            {
                SortBy = "OtherIncome",
                Baseline = 100
            };

            // set up test data using SchoolTrustFinance
            var testData = new List<SchoolTrustFinance>
            {
                new SchoolTrustFinance { OtherIncome = 80m },
                new SchoolTrustFinance { OtherIncome = 110m },
                new SchoolTrustFinance { OtherIncome = 95m }

            };

            // Act
            var result = testSimpleProximitySort.Sort(testData);

            // Assert
            // correct order
            Assert.Equal(95m, result.ElementAt(0).OtherIncome);
            Assert.Equal(110m, result.ElementAt(1).OtherIncome);
            Assert.Equal(80m, result.ElementAt(2).OtherIncome);
        }
    }
}

