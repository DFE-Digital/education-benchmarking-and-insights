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
            var testSenProximitySort = new SenProximitySort
            {
                SortBy = "SEN",
                Baseline = baseline
            };

            var testData = new List<SchoolTrustFinance>
            {
                new() { SEN = value1 },
                new() { SEN = value2 },
                new() { SEN = value3 }

            };
            
            var result = testSenProximitySort.Sort(testData).ToArray();
            
            Assert.Equal(value3, result.ElementAt(0).SEN);
            Assert.Equal(value2, result.ElementAt(1).SEN);
            Assert.Equal(value1, result.ElementAt(2).SEN);
        }
    }
}
