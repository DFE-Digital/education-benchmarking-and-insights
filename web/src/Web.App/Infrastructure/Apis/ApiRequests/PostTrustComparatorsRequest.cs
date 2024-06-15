using Web.App.ViewModels;
// ReSharper disable InconsistentNaming
// ReSharper disable NotAccessedPositionalProperty.Global
// ReSharper disable UnusedAutoPropertyAccessor.Global
// ReSharper disable UnusedMember.Global
namespace Web.App.Infrastructure.Apis;

public class PostTrustComparatorsRequest(string companyNumber, UserDefinedTrustCharacteristicViewModel viewModel)
{
    public string Target => companyNumber;

    public CharacteristicList? PhasesCovered => viewModel.OverallPhases is { Length: > 0 }
        ? new CharacteristicList(viewModel.OverallPhases!)
        : null;

    public CharacteristicRange? TotalPupils => IsSelected(viewModel.TotalPupils)
        ? new CharacteristicRange(viewModel.TotalPupilsFrom, viewModel.TotalPupilsTo)
        : null;

    public CharacteristicRange? TotalIncome => IsSelected(viewModel.TotalIncome)
        ? new CharacteristicRange(viewModel.TotalIncomeFrom, viewModel.TotalIncomeTo)
        : null;

    public CharacteristicDateRange? OpenDate => IsSelected(viewModel.FormationYear)
        ? new CharacteristicDateRange(
            new DateTime(viewModel.FormationYearFrom.GetValueOrDefault(), 1, 1),
            new DateTime(viewModel.FormationYearTo.GetValueOrDefault(), 12, 31))
        : null;

    public CharacteristicRange? TotalInternalFloorArea => IsSelected(viewModel.InternalFloorArea)
        ? new CharacteristicRange(viewModel.InternalFloorAreaFrom, viewModel.InternalFloorAreaTo)
        : null;

    public CharacteristicRange? PercentFreeSchoolMeals => IsSelected(viewModel.FreeSchoolMeals)
        ? new CharacteristicRange(viewModel.FreeSchoolMealsFrom, viewModel.FreeSchoolMealsTo)
        : null;

    public CharacteristicRange? PercentSpecialEducationNeeds => IsSelected(viewModel.SpecialEducationalNeeds)
        ? new CharacteristicRange(viewModel.SpecialEducationalNeedsFrom, viewModel.SpecialEducationalNeedsTo)
        : null;

    public CharacteristicRange? SchoolsInTrust => IsSelected(viewModel.SchoolsInTrust)
        ? new CharacteristicRange(viewModel.SchoolsInTrustFrom, viewModel.SchoolsInTrustTo)
        : null;

    private static bool IsSelected(string? value) => bool.TrueString.Equals(value, StringComparison.OrdinalIgnoreCase);
}