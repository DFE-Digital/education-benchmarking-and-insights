using Platform.Api.LocalAuthority.Features.Search.Validators;
using Platform.Search;
using Xunit;

namespace Platform.LocalAuthority.Tests.Features.Search.Validators;

public class WhenLocalAuthoritiesSearchValidatorValidates
{
    private readonly LocalAuthoritiesSearchValidator _validator = new();

    [Fact]
    public void ShouldValidateValidOrderByField()
    {
        var request = new SearchRequest
        {
            SearchText = "test",
            OrderBy = new OrderByCriteria
            {
                Field = "LocalAuthorityNameSortable",
                Value = "asc"
            }
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ShouldValidateNullOrderBy()
    {
        var request = new SearchRequest
        {
            SearchText = "test",
            OrderBy = null
        };

        var result = _validator.Validate(request);

        Assert.True(result.IsValid);
    }

    [Fact]
    public void ShouldInvalidateIncorrectOrderByField()
    {
        var request = new SearchRequest
        {
            SearchText = "test",
            OrderBy = new OrderByCriteria
            {
                Field = "InvalidField"
            }
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "OrderBy Field must be LocalAuthorityNameSortable");
    }

    [Fact]
    public void ShouldInvalidateNullOrderByField()
    {
        var request = new SearchRequest
        {
            SearchText = "test",
            OrderBy = new OrderByCriteria
            {
                Field = null
            }
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.ErrorMessage == "OrderBy Field must be LocalAuthorityNameSortable");
    }

    [Fact]
    public void ShouldInvalidateShortSearchText()
    {
        var request = new SearchRequest
        {
            SearchText = "te",
            OrderBy = null
        };

        var result = _validator.Validate(request);

        Assert.False(result.IsValid);
        Assert.Contains(result.Errors, e => e.PropertyName == "SearchText");
    }
}