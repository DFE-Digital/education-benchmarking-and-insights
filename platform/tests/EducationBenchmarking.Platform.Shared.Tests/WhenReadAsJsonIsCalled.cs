using Microsoft.AspNetCore.Http;
using System.Text;
using EducationBenchmarking.Platform.Functions.Extensions;
using Xunit;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenReadAsJsonIsCalled
    {
        [Fact]
        public void ReturnsObjectMatchingType()
        {
            var testHttpContext = new DefaultHttpContext();
            var jsonContent = new TestObjectType("testValue").ToJson();
            testHttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            var testHttpRequest = testHttpContext.Request;

            var result = testHttpRequest.ReadAsJson<TestObjectType>();
            
            Assert.NotNull(result);
            Assert.IsType<TestObjectType>(result);
            Assert.Equal("testValue", result.TestProp);
        }


        [Fact]
        public void IfResultIsNullThrowsNullReferenceException()
        {
            var testHttpContext = new DefaultHttpContext();
            var jsonContent = "";
            testHttpContext.Request.Body = new MemoryStream(Encoding.UTF8.GetBytes(jsonContent));

            var testHttpRequest = testHttpContext.Request;

            Assert.Throws<NullReferenceException>(() => testHttpRequest.ReadAsJson<TestObjectType>());
        }
    }


    public class TestObjectType
    {
        public TestObjectType(string testProp)
        {
            TestProp = testProp;
        }

        public string TestProp { get; }
    }
}
