using EducationBenchmarking.Platform.Api.Benchmark;
using EducationBenchmarking.Platform.Api.Benchmark.Models;
using EducationBenchmarking.Platform.Api.Benchmark.Requests;
using EducationBenchmarking.Platform.Shared;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace EducationBenchmarking.Platform.Tests.Benchmark;

public class WhenFunctionReceivesPostComparatorSetRequest : ComparatorSetFunctionsTestBase
{
    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        Db
            .Setup(d => d.CreateSet(It.IsAny<ComparatorSetRequest>()))
            .ReturnsAsync(new ComparatorSet());

        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<ComparatorSetRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        var result =
            await Functions.PostComparatorSet(CreateRequestWithBody(new ComparatorSetRequest())) as JsonContentResult;

        Assert.NotNull(result);
        Assert.Equal(200, result?.StatusCode);
    }

    [Fact]
    public async Task ShouldReturn40OOnInvalidRequest()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<ComparatorSetRequest>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult(new[] { new ValidationFailure(nameof(ComparatorSetRequest.IncludeSet), "This error message") }));

        var result = await Functions
            .PostComparatorSet(CreateRequestWithBody(new ComparatorSetRequest())) as ValidationErrorsResult;

        Assert.NotNull(result);
        Assert.Equal(400, result?.StatusCode);

        var values = result?.Value as IEnumerable<ValidationError>;
        Assert.NotNull(values);
        Assert.Contains(values, p => p.PropertyName == nameof(ComparatorSetRequest.IncludeSet));
    }

    [Fact]
    public async Task ShouldReturn500OnError()
    {
        Validator
            .Setup(v => v.ValidateAsync(It.IsAny<ComparatorSetRequest>(), It.IsAny<CancellationToken>()))
            .Throws(new Exception());
        
        var result = await Functions
            .PostComparatorSet(CreateRequestWithBody(new ComparatorSetRequest())) as StatusCodeResult;

        Assert.NotNull(result);
        Assert.Equal(500, result?.StatusCode);
    }
}