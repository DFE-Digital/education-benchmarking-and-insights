using System.ComponentModel.DataAnnotations;
using AutoFixture;
using Web.App.Attributes;
using Xunit;

namespace Web.Tests.Attributes;

public class GivenCompareDecimalValueAttribute
{
    private readonly Fixture _fixture = new();

    [Fact]
    public void ThrowsValidationExceptionWhenOtherFieldIsInvalid()
    {
        var attribute = new CompareDecimalValueAttribute("Other", Operator.EqualTo);
        var model = new TestModel(100);
        var context = new ValidationContext(model);

        var exception = Assert.Throws<ValidationException>(() => attribute.Validate(123, context));
        Assert.Equal("Could not find a property named Other.", exception.Message);
    }

    [Fact]
    public void ThrowsValidationExceptionWhenOtherFieldIsIndexer()
    {
        var attribute = new CompareDecimalValueAttribute("Item", Operator.EqualTo);
        var model = new BadTestModel();
        var context = new ValidationContext(model);

        var exception = Assert.Throws<ArgumentException>(() => attribute.Validate(123, context));
        Assert.Equal(
            "The property Web.Tests.Attributes.GivenCompareDecimalValueAttribute+BadTestModel.Item could not be found.",
            exception.Message);
    }

    [Theory]
    [InlineData(50, 100, Operator.EqualTo, "This field should be equal to other field")]
    [InlineData(50, 100, Operator.GreaterThan, "This field cannot be less than or equal to other field")]
    [InlineData(100, 100, Operator.GreaterThan, "This field cannot be less than or equal to other field")]
    [InlineData(50, 100, Operator.GreaterThanOrEqualTo, "This field cannot be less than other field")]
    [InlineData(100, 50, Operator.LessThan, "This field cannot be greater than or equal to other field")]
    [InlineData(100, 100, Operator.LessThan, "This field cannot be greater than or equal to other field")]
    [InlineData(100, 50, Operator.LessThanOrEqualTo, "This field cannot be greater than other field")]
    public void ReturnsValidationResultWhenComparisonIsNotValid(decimal value, decimal otherValue,
        Operator operatorType, string expected)
    {
        var attribute = new CompareDecimalValueAttribute(nameof(TestModel.OtherField), operatorType);
        var model = new TestModel(otherValue);
        var memberName = _fixture.Create<string>();
        var context = new ValidationContext(model) { DisplayName = "This field", MemberName = memberName };

        var validationResult = attribute.GetValidationResult(value, context);
        Assert.Equal(expected, validationResult?.ErrorMessage);
        Assert.Equal([memberName], validationResult?.MemberNames);
    }

    [Theory]
    [InlineData(100, 100, Operator.EqualTo)]
    [InlineData(100, 50, Operator.GreaterThan)]
    [InlineData(100, 50, Operator.GreaterThanOrEqualTo)]
    [InlineData(50, 50, Operator.GreaterThanOrEqualTo)]
    [InlineData(50, 100, Operator.LessThan)]
    [InlineData(50, 100, Operator.LessThanOrEqualTo)]
    [InlineData(50, 50, Operator.LessThanOrEqualTo)]
    public void ReturnsNullWhenComparisonIsValid(decimal value, decimal otherValue, Operator operatorType)
    {
        var attribute = new CompareDecimalValueAttribute(nameof(TestModel.OtherField), operatorType);
        var model = new TestModel(otherValue);
        var context = new ValidationContext(model);

        var validationResult = attribute.GetValidationResult(value, context);
        Assert.Null(validationResult);
    }

    [Fact]
    public void ReturnsNullWhenValueIsNull()
    {
        var attribute = new CompareDecimalValueAttribute(string.Empty, Operator.EqualTo);
        var model = new TestModel(0);
        var context = new ValidationContext(model);

        var validationResult = attribute.GetValidationResult(null, context);
        Assert.Null(validationResult);
    }

    private class TestModel(decimal value)
    {
        [Display(Name = "Other field")] public decimal OtherField { get; set; } = value;
    }

    private class BadTestModel
    {
        private readonly string[] _strings = [];

        // ReSharper disable once UnusedMember.Local
        public string this[int index]
        {
            get => _strings[index];
            set => _strings[index] = value;
        }
    }
}