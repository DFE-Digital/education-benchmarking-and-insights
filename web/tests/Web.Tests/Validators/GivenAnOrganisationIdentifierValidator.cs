using Web.App;
using Web.App.Validators;
using Xunit;

namespace Web.Tests.Validators;

public class GivenAnOrganisationIdentifierValidator
{
    private readonly OrganisationIdentifierValidator _validator = new();

    [Fact]
    public void ShouldFailValidationIfIdentifierOrTypeMissing()
    {
        var identifier = new OrganisationIdentifier();
        string[] expected =
        [
            "Organisation identifier must be provided",
            "Organisation type must be provided"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Fact]
    public void ShouldFailValidationIfTypeInvalid()
    {
        var identifier = new OrganisationIdentifier
        {
            Value = "123456",
            Type = "invalid"
        };

        string[] expected =
        [
            "Organisation type must be valid"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Theory]
    [InlineData(OrganisationTypes.School)]
    [InlineData(OrganisationTypes.Trust)]
    [InlineData(OrganisationTypes.LocalAuthority)]
    public void ShouldFailValidationIfIdentifierInvalid(string type)
    {
        var identifier = new OrganisationIdentifier
        {
            Value = "invalid",
            Type = type
        };

        string[] expected =
        [
            "Organisation identifier must be a number"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("-12345")]
    public void ShouldFailValidationIfUrnInvalid(string urn)
    {
        var identifier = new OrganisationIdentifier
        {
            Value = urn,
            Type = OrganisationTypes.School
        };

        string[] expected =
        [
            "School URN must be 6 digits"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("-1234567")]
    public void ShouldFailValidationIfCompanyNumberInvalid(string companyNumber)
    {
        var identifier = new OrganisationIdentifier
        {
            Value = companyNumber,
            Type = OrganisationTypes.Trust
        };

        string[] expected =
        [
            "Trust company number must be 8 digits"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Theory]
    [InlineData("1234")]
    [InlineData("-12")]
    public void ShouldFailValidationIfLaCodeInvalid(string code)
    {
        var identifier = new OrganisationIdentifier
        {
            Value = code,
            Type = OrganisationTypes.LocalAuthority
        };

        string[] expected =
        [
            "Local authority code must be 3 digits"
        ];

        var results = _validator.Validate(identifier);

        Assert.False(results.IsValid);
        Assert.NotEmpty(results.Errors);
        Assert.Equal(expected, results.Errors.Select(e => e.ErrorMessage));
    }

    [Theory]
    [InlineData(OrganisationTypes.School, "123456")]
    [InlineData(OrganisationTypes.Trust, "12345678")]
    [InlineData(OrganisationTypes.LocalAuthority, "123")]
    public void ShouldPassValidationIfIdentifierValid(string type, string value)
    {
        var identifier = new OrganisationIdentifier
        {
            Value = value,
            Type = type
        };

        var results = _validator.Validate(identifier);

        Assert.True(results.IsValid);
    }
}