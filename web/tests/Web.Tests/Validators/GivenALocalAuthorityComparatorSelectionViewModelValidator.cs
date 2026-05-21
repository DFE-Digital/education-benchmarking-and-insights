using Web.App.Domain;
using Web.App.Validators;
using Web.App.ViewModels;
using Xunit;

namespace Web.Tests.Validators;

public class GivenALocalAuthorityComparatorSelectionViewModelValidator
{
    private readonly LocalAuthorityComparatorSelectionViewModelValidator _validator = new();

    [Fact]
    public void ShouldFailValidationWithNoSelectedComparatorsWhenActionIsContinue()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Continue
        };

        var result = _validator.Validate(viewModel);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Select between 1 and 19 comparator local authorities", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void ShouldFailValidationWith20SelectedComparatorsWhenActionIsContinue()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Continue,
            Selected = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19", "20"]
        };

        var result = _validator.Validate(viewModel);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Select between 1 and 19 comparator local authorities", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void ShouldPassValidationWith19SelectedComparatorsWhenActionIsContinue()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Continue,
            Selected = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"]
        };

        var result = _validator.Validate(viewModel);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }


    [Fact]
    public void ShouldFailValidationWithNoLaCodeWhenActionIsAdd()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Add
        };

        var result = _validator.Validate(viewModel);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Select a local authority", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void ShouldFailValidationWith19SelectedComparatorsAndAnLaCodeWhenActionIsAdd()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Add,
            Selected = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18", "19"],
            LaInput = "20"
        };

        var result = _validator.Validate(viewModel);

        Assert.False(result.IsValid);
        Assert.NotEmpty(result.Errors);
        Assert.Equal("Select up to 19 comparator local authorities", result.Errors.First().ErrorMessage);
    }

    [Fact]
    public void ShouldPassValidationWith18SelectedComparatorsAndAnLaCodeWhenActionIsAdd()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Add,
            Selected = ["1", "2", "3", "4", "5", "6", "7", "8", "9", "10", "11", "12", "13", "14", "15", "16", "17", "18"],
            LaInput = "19"
        };

        var result = _validator.Validate(viewModel);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }

    [Fact]
    public void ShouldPassValidationWithNoSelectedComparatorsAndAnLaCodeWhenActionIsAdd()
    {
        var viewModel = new LocalAuthorityComparatorSelectionViewModel
        {
            Action = FormAction.Add,
            LaInput = "1"
        };

        var result = _validator.Validate(viewModel);

        Assert.True(result.IsValid);
        Assert.Empty(result.Errors);
    }
}
