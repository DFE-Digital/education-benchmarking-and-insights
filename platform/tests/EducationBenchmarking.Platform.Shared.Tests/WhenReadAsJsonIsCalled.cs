using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    //behaviour to test
    //returns an object matching the type specified
    // returns an object with the correct values
    //throws null ref exc if the result is null

    // TODO naming
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
            // not null
            Assert.NotNull(result);
            // correct type
            Assert.IsType<TestObjectType>(result);
            // correct value
            Assert.Equal("testValue", result.Test0);
        }


        [Fact]
        public void IfResultIsNullThrowsNullReferenceException()
        {

        }
    }


    public class TestObjectType
    {
        public string Test0 { get; set; }
    }
}
