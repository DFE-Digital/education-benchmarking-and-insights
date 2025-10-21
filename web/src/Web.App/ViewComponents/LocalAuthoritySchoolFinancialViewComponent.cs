using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolFinancialViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string code, string formPrefix)
    {
        var (filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort) = ParseQuery(Request.Query, formPrefix);

        // todo: replace stub with call to API to get results
        var results = await Task.FromResult(new[]
        {
            new LocalAuthoritySchoolFinancial
            {
                SchoolName = "Stub school 1",
                Urn = "000001",
                TotalPupils = 1234,
                PeriodCoveredByReturn = 12
            },
            new LocalAuthoritySchoolFinancial
            {
                SchoolName = "Stub school 2",
                Urn = "000002",
                TotalPupils = 567,
                PeriodCoveredByReturn = 12
            },
            new LocalAuthoritySchoolFinancial
            {
                SchoolName = "Stub school 3",
                Urn = "000003",
                TotalPupils = 890,
                PeriodCoveredByReturn = 10
            }
        });

        var viewModel = new LocalAuthoritySchoolFinancialViewModel(code, formPrefix)
        {
            FiltersVisible = filtersVisible,
            Results = results,
            SelectedOverallPhases = selectedOverallPhases.ToArray(),
            SelectedNurseryProvisions = selectedNurseryProvisions.ToArray(),
            SelectedSpecialProvisions = selectedSpecialProvisions.ToArray(),
            SelectedSixthFormProvisions = selectedSixthFormProvisions.ToArray()
        };

        if (resultAs != null)
        {
            viewModel.ResultAs = resultAs.Value;
        }

        if (!string.IsNullOrWhiteSpace(sort))
        {
            viewModel.Sort = sort;
        }

        return View(viewModel);
    }

    private static (
        bool filtersVisible,
        Dimensions.ResultAsOptions? resultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] selectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] selectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] selectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] selectedSixthFormProvisions,
        string? sort) ParseQuery(IQueryCollection query, string formPrefix)
    {
        var filtersVisible = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.FiltersVisible}"] != "hide";

        Dimensions.ResultAsOptions? resultAs = null;
        var resultsAs = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.ResultAs}"]
            .CastQueryToEnum<Dimensions.ResultAsOptions>()
            .ToArray();
        if (resultsAs.Length > 0)
        {
            resultAs = resultsAs.First();
        }

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

        var sort = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.Sort}"];

        return (
            filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort == StringValues.Empty ? null : sort.ToString());
    }
}