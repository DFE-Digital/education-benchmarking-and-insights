using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using EducationBenchmarking.Platform.Shared;
using Microsoft.Extensions.Primitives;

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
            var correlationIdHeaderValue = "123e4567-e89b-12d3-a456-426614174000";
            testHttpContext.Request.Headers.Add(Constants.CorrelationIdHeader, correlationIdHeaderValue);

            var testHttpRequest = testHttpContext.Request;

            // Act
            var result = HttpRequestExtensions.GetCorrelationId(testHttpRequest);

            // Assert
            // is valid guid
            Assert.True(Guid.TryParse(result.ToString(), out _));
            // is the same guid
            Assert.Equal(Guid.Parse(correlationIdHeaderValue), result);
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
            Assert.Throws<FormatException>(() => HttpRequestExtensions.GetCorrelationId(testHttpRequest));
        }
        
        
        [Fact]
        public void WithoutCorrelationIdHeader()
        {
            // Arrange
            var testHttpContext = new DefaultHttpContext();
            var testHttpRequest = testHttpContext.Request;

            // Act
            var result = HttpRequestExtensions.GetCorrelationId(testHttpRequest);

            // Assert
            // is valid guid
            Assert.True(Guid.TryParse(result.ToString(), out _));
        }
    }
}
