using System.Net;
using FluentValidation;
using FluentValidation.Results;
using Moq;
using Platform.Api.Insight.Features.Expenditure;
using Platform.Api.Insight.Features.Expenditure.Models;
using Platform.Api.Insight.Features.Expenditure.Parameters;
using Platform.Api.Insight.Features.Expenditure.Services;
using Platform.Api.Insight.Shared;
using Platform.Test;
using Xunit;

namespace Platform.Insight.Tests.Expenditure;

public class GetExpenditureSchoolHistoryFunctionTests : FunctionsTestBase
{
    private readonly CancellationToken _cancellationToken = CancellationToken.None;
    private readonly GetExpenditureSchoolHistoryFunction _function;
    private readonly Mock<IExpenditureService> _service;
    private readonly Mock<IValidator<ExpenditureParameters>> _validator;

    public GetExpenditureSchoolHistoryFunctionTests()
    {
        _validator = new Mock<IValidator<ExpenditureParameters>>();
        _service = new Mock<IExpenditureService>();
        _function = new GetExpenditureSchoolHistoryFunction(_service.Object, _validator.Object);
    }

    [Fact]
    public async Task ShouldReturn200OnValidRequest()
    {
        _validator
            .Setup(v => v.ValidateAsync(It.IsAny<ExpenditureParameters>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new ValidationResult());

        _service
            .Setup(d => d.GetSchoolHistoryAsync(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((new YearsModel(), Array.Empty<ExpenditureHistoryModel>()));

        var result = await _function.RunAsync(CreateHttpRequestData(), "1", _cancellationToken);

        Assert.NotNull(result);
        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
    }
}