using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenUnknownProximitySortIsCalled
    {
        [Fact]
        public void SortingWithEmptyKindThrowsNotImplementedException()
        {
            // Arrange
            var unknownProximitySort = new UnknownProximitySort();
            var testData = new List<SchoolTrustFinance>();

            // Act and Assert
            Assert.Throws<NotImplementedException>(() => unknownProximitySort.Sort(testData));
        }
    }
}
