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
    //behaviour to test
    // when valid guid in headers returns that guid
    // when invalid guid in headers throws
    // when guid not in headers return new guid

    // TODO naming
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
            // is the same guid
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
            // TODO is valid guid
            // Assert.True(Guid.TryParse(result.ToString(), out _));
            Assert.IsType<Guid>(result);
            Assert.NotEqual(Guid.Empty, result);
        }
    }
}
