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
    public class WhenGetPagingValuesIsCalled
    {
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
