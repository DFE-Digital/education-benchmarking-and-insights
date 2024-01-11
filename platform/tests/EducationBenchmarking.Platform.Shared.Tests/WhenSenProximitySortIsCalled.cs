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
    public class WhenSenProximitySortIsCalled
    {
        [Fact]
        public void OrderIsCorrect()
        {
            // Arrange
            // set up SenProximitySort
            var testSenProximitySort = new SenProximitySort
            {
                SortBy = "SEN",
                Baseline = 100
            };

            // set up test data using SchoolTrustFinance
            var testData = new List<SchoolTrustFinance>
            {
                new SchoolTrustFinance { SEN = 80m },
                new SchoolTrustFinance { SEN = 110m },
                new SchoolTrustFinance { SEN = 95m }

            };

            // Act
            var result = testSenProximitySort.Sort(testData);

            // Assert
            // correct order
            Assert.Equal(95m, result.ElementAt(0).SEN);
            Assert.Equal(110m, result.ElementAt(1).SEN);
            Assert.Equal(80m, result.ElementAt(2).SEN);
        }
    }
}
