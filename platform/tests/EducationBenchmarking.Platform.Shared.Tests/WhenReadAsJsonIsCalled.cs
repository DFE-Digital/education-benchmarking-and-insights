using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenReadAsJsonIsCalled
    {
        [Fact]
        public void ReturnsObjectMatchingType()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var jsonContent = "{\"test0\": \"testValue\"}";
            testHttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            var testHttpRequest = testHttpContext.Request;

            // Act
            var result = testHttpRequest.ReadAsJson<TestObjectType>();

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TestObjectType>(result);
            Assert.Equal("testValue", result.Test0);
        }


        [Fact]
        public void IfResultIsNullThrowsNullReferenceException()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var jsonContent = "";
            testHttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            var testHttpRequest = testHttpContext.Request;

            // Act and Assert
            Assert.Throws<NullReferenceException>(() => testHttpRequest.ReadAsJson<TestObjectType>());
        }
    }


    public class TestObjectType
    {
        public string Test0 { get; set; }
    }
}
