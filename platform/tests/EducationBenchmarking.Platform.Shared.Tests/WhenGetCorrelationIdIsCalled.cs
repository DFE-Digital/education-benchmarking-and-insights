using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Primitives;
using Newtonsoft.Json.Linq;

namespace EducationBenchmarking.Platform.Shared.Tests
{
    public class WhenGetCorrelationIdIsCalled
    {
        [Fact]
        public void WithCorrelationIdHeaderAsValidGuid()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var correlationIdHeaderValue = Guid.NewGuid();
            testHttpContext.Request.Headers.Add(Constants.CorrelationIdHeader, correlationIdHeaderValue.ToString());

            var testHttpRequest = testHttpContext.Request;

            // Act
            var result = testHttpRequest.GetCorrelationId();

            // Assert
            Assert.Equal(correlationIdHeaderValue, result);
        }


        [Fact]
        public void WithCorrelationIdHeaderAsInvalidGuid()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var correlationIdHeaderValue = "invalid";
            testHttpContext.Request.Headers.Add(Constants.CorrelationIdHeader, correlationIdHeaderValue);

            var testHttpRequest = testHttpContext.Request;

            // Act + Assert
            Assert.Throws<FormatException>(() => testHttpRequest.GetCorrelationId());
        }
        
        
        [Fact]
        public void WithoutCorrelationIdHeader()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var testHttpRequest = testHttpContext.Request;

            // Act
            var result = testHttpRequest.GetCorrelationId();

            // Assert
            Assert.IsType<Guid>(result);
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
