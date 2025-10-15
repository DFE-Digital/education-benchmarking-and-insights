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
        var (selectedOverallPhases, resultAs) = ParseQuery(Request.Query, formPrefix);

        // todo: call API to get results

        var viewModel = new LocalAuthoritySchoolFinancialViewModel(code, formPrefix)
        {
            SelectedOverallPhases = selectedOverallPhases.ToArray(),
            ResultAs = resultAs
        };

        return await Task.FromResult(View(viewModel));
    }

    private static (
        OverallPhaseTypes.OverallPhaseTypeFilter[] selectedOverallPhases,
        Dimensions.ResultAsOptions resultAs) ParseQuery(IQueryCollection query, string formPrefix)
    {
        var selectedOverallPhases = query[$"{formPrefix}{LocalAuthoritySchoolFinancialViewModel.FormNames.SelectedOverallPhases}"]
            .CastQueryToEnum<OverallPhaseTypes.OverallPhaseTypeFilter>()
            .ToArray();
        var resultAs = query[$"{formPrefix}{LocalAuthoritySchoolFinancialViewModel.FormNames.ResultAs}"]
            .CastQueryToEnum<Dimensions.ResultAsOptions>()
            .FirstOrDefault();

        return (selectedOverallPhases, resultAs);
    }
}