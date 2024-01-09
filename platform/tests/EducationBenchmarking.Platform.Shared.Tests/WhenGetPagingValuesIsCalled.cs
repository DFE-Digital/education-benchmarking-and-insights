using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EducationBenchmarking.Platform.Shared.Helpers;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    //behaviour to test
    // page and pageSize are present should return tuple with both values
    // neither value present should return default values page == 1 pageSize == 10
    // page and pageSize can't be parsed as int should return default values page == 1 pageSize == 10
    // page is present but pageSize is not should return tuple with page value and default pageSize
    // pageSize is present but page is not should return tuple with pageSize value and default page
    // TODO naming
    public class WhenGetPagingValuesIsCalled
    {
        // page and pageSize are present should return tuple with both values
        [Fact]
        public void PageAndPageSizeValuesPresent()
        {
            // Arrange
            var query = new List<KeyValuePair<string, StringValues>>
            {
                new KeyValuePair<string, StringValues>("page", "2"),
                new KeyValuePair<string, StringValues>("pageSize", "20")
            };

            // Act
            var result = QueryParameters.GetPagingValues(query);

            // Assert
            Assert.Equal(2, result.Page);
            Assert.Equal(20, result.PageSize);
        }


        // neither value present should return default values page == 1 pageSize == 10
        [Fact]
        public void NoValuesPresent()
        {
            // Arrange
            var query = new List<KeyValuePair<string, StringValues>>();

            // Act
            var result = QueryParameters.GetPagingValues(query);

            // Assert
            Assert.Equal(1, result.Page);
            Assert.Equal(10, result.PageSize);
        }


        // page and pageSize can't be parsed as int should return default values page == 1 pageSize == 10
        [Fact]
        public void PageAndPageSizeValuesInvalid()
        {
            // Arrange
            var query = new List<KeyValuePair<string, StringValues>>
            {
                new KeyValuePair<string, StringValues>("page", "invalid"),
                new KeyValuePair<string, StringValues>("pageSize", "invalid")
            };

            // Act
            var result = QueryParameters.GetPagingValues(query);

            // Assert
            Assert.Equal(1, result.Page);
            Assert.Equal(10, result.PageSize);
        }


        // page is present but pageSize is not should return tuple with page value and default pageSize
        [Fact]
        public void PageValuePresent()
        {
            // Arrange
            var query = new List<KeyValuePair<string, StringValues>>
            {
                new KeyValuePair<string, StringValues>("page", "2"),
            };

            // Act
            var result = QueryParameters.GetPagingValues(query);

            // Assert
            Assert.Equal(2, result.Page);
            Assert.Equal(10, result.PageSize);
        }


        // pageSize is present but page is not should return tuple with pageSize value and default page
        [Fact]
        public void PageSizeValuePresent()
        {
            // Arrange
            var query = new List<KeyValuePair<string, StringValues>>
            {
                new KeyValuePair<string, StringValues>("pageSize", "20")
            };

            // Act
            var result = QueryParameters.GetPagingValues(query);

            // Assert
            Assert.Equal(1, result.Page);
            Assert.Equal(20, result.PageSize);
        }

    }
}
