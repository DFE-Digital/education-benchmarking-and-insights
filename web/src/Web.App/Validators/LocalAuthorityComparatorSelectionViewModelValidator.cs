using FluentValidation;
using Web.App.Domain;
using Web.App.ViewModels;

namespace Web.App.Validators;

public class LocalAuthorityComparatorSelectionViewModelValidator : AbstractValidator<LocalAuthorityComparatorSelectionViewModel>
{
    public LocalAuthorityComparatorSelectionViewModelValidator()
    {
        RuleFor(x => x.Selected)
            .Must(x => x.Length >= 1 && x.Length <= 19)
            .When(x => x.Action == FormAction.Continue)
            .WithMessage("Select between 1 and 19 comparator local authorities");

        RuleFor(x => x.LaInput)
            .Must(x => !string.IsNullOrEmpty(x))
            .When(x => x.Action == FormAction.Add)
            .WithMessage("Select a local authority");

        RuleFor(x => x.Selected)
            .Must(x => x.Length < 19)
            .When(x => x.Action == FormAction.Add)
            .WithMessage("Select up to 19 comparator local authorities");
    }
}