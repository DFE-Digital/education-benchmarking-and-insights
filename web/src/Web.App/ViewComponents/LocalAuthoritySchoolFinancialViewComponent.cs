using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolFinancialViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string code, string formPrefix)
    {
        var (resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions) = ParseQuery(Request.Query, formPrefix);

        // todo: call API to get results

        var viewModel = new LocalAuthoritySchoolFinancialViewModel(code, formPrefix)
        {
            ResultAs = resultAs,
            SelectedOverallPhases = selectedOverallPhases.ToArray(),
            SelectedNurseryProvisions = selectedNurseryProvisions.ToArray(),
            SelectedSpecialProvisions = selectedSpecialProvisions.ToArray(),
            SelectedSixthFormProvisions = selectedSixthFormProvisions.ToArray()
        };

        return await Task.FromResult(View(viewModel));
    }

    private static (
        Dimensions.ResultAsOptions resultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] selectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] selectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] selectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] selectedSixthFormProvisions) ParseQuery(IQueryCollection query, string formPrefix)
    {
        var resultAs = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.ResultAs}"]
            .CastQueryToEnum<Dimensions.ResultAsOptions>()
            .FirstOrDefault();
        var selectedOverallPhases = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.SelectedOverallPhases}"]
            .CastQueryToEnum<OverallPhaseTypes.OverallPhaseTypeFilter>()
            .ToArray();
        var selectedNurseryProvisions = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.SelectedNurseryProvisions}"]
            .CastQueryToEnum<NurseryProvisions.NurseryProvisionFilter>()
            .ToArray();
        var selectedSpecialProvisions = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.SelectedSpecialProvisions}"]
            .CastQueryToEnum<SpecialProvisions.SpecialProvisionFilter>()
            .ToArray();
        var selectedSixthFormProvisions = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.SelectedSixthFormProvisions}"]
            .CastQueryToEnum<SixthFormProvisions.SixthFormProvisionFilter>()
            .ToArray();

        return (resultAs, selectedOverallPhases, selectedNurseryProvisions, selectedSpecialProvisions, selectedSixthFormProvisions);
    }
}