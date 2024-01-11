using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenSenProximitySortIsCalled
    {
        [Theory]
        [InlineData(80, 110, 95, 100)]
        [InlineData(70, 120, 90, 100)]
        [InlineData(400, 150, 190, 200)]
        public void OrderIsCorrect(decimal value1, decimal value2, decimal value3, int baseline)
        {
            // Arrange
            var testSenProximitySort = new SenProximitySort
            {
                SortBy = "SEN",
                Baseline = baseline
            };

            var testData = new List<SchoolTrustFinance>
            {
                new SchoolTrustFinance { SEN = value1 },
                new SchoolTrustFinance { SEN = value2 },
                new SchoolTrustFinance { SEN = value3 }

            };

            // Act
            var result = testSenProximitySort.Sort(testData);

            // Assert
            Assert.Equal(value3, result.ElementAt(0).SEN);
            Assert.Equal(value2, result.ElementAt(1).SEN);
            Assert.Equal(value1, result.ElementAt(2).SEN);
        }
    }
}
