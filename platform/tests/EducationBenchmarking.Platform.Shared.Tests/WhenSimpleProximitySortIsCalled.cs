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
        [Theory]
        [InlineData(80, 110, 95, 100)]
        [InlineData(190, 120, 140, 150)]
        [InlineData(350, 325, 280, 300)]
        public void OrderIsCorrect(decimal value1, decimal value2, decimal value3, int baseline)
        {
            // Arrange
            // set up SimpleProximitySort
            var testSimpleProximitySort = new SimpleProximitySort
            {
                SortBy = "OtherIncome",
                Baseline = baseline
            };

            // set up test data using SchoolTrustFinance
            var testData = new List<SchoolTrustFinance>
            {
                new SchoolTrustFinance { OtherIncome = value1 },
                new SchoolTrustFinance { OtherIncome = value2 },
                new SchoolTrustFinance { OtherIncome = value3 }

            };

            // Act
            var result = testSimpleProximitySort.Sort(testData);

            // Assert
            // correct order
            Assert.Equal(value3, result.ElementAt(0).OtherIncome);
            Assert.Equal(value2, result.ElementAt(1).OtherIncome);
            Assert.Equal(value1, result.ElementAt(2).OtherIncome);
        }
    }
}

