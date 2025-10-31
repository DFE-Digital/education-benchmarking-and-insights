using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.Charts;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolFinancialFormViewComponent(ILocalAuthoritiesApi localAuthoritiesApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string code,
        string formPrefix,
        int maxRows,
        string defaultSort,
        Dictionary<string, StringValues> otherFormValues,
        string tabId)
    {
        var query = ParseQueryString(Request.Query, formPrefix, defaultSort);
        var results = await localAuthoritiesApi
            .GetSchoolsFinance(code, BuildQuery(query, maxRows))
            .GetResultOrDefault<LocalAuthoritySchoolFinancial[]>() ?? [];

        var (allRows,
            filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort) = query;
        var viewModel = new LocalAuthoritySchoolFinancialFormViewModel(
            code,
            formPrefix,
            maxRows,
            defaultSort,
            otherFormValues,
            tabId)
        {
            AllRows = allRows,
            FiltersVisible = filtersVisible,
            ResultAs = resultAs,
            Results = results,
            SelectedOverallPhases = selectedOverallPhases.ToArray(),
            SelectedNurseryProvisions = selectedNurseryProvisions.ToArray(),
            SelectedSpecialProvisions = selectedSpecialProvisions.ToArray(),
            SelectedSixthFormProvisions = selectedSixthFormProvisions.ToArray(),
            Sort = sort
        };

        return View(viewModel);
    }

    private static ParsedQueryString ParseQueryString(IQueryCollection query, string formPrefix, string defaultSort)
    {
        var filtersVisible = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.FiltersVisible}"] == LocalAuthoritySchoolFinancialFormViewModel.FormFieldValues.Show;

        var resultAs = Dimensions.ResultAsOptions.PercentIncome;
        var resultsAsValues = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.ResultAs}"]
            .CastQueryToEnum<Dimensions.ResultAsOptions>()
            .ToArray();
        if (resultsAsValues.Length > 0)
        {
            resultAs = resultsAsValues.First();
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

        var sortValues = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.Sort}"];
        var sort = sortValues == StringValues.Empty ? defaultSort : sortValues.ToString();
        var allRows = query[$"{formPrefix}{LocalAuthoritySchoolFinancialFormViewModel.FormFieldNames.Rows}"] == LocalAuthoritySchoolFinancialFormViewModel.FormFieldValues.All;

        return new ParsedQueryString(
            allRows,
            filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort);
    }

    private static ApiQuery BuildQuery(ParsedQueryString queryString, int maxRows)
    {
        var sort = queryString.Sort.Split("~");
        var query = new ApiQuery()
            .AddIfNotNull("dimension", queryString.ResultAs.GetQueryParam())
            .AddIfNotNull("sortField", sort.FirstOrDefault())
            .AddIfNotNull("sortOrder", sort.LastOrDefault())
            .AddIfNotNull("limit", queryString.AllRows ? null : maxRows.ToString());

        foreach (var filter in queryString.SelectedOverallPhases)
        {
            query.AddIfNotNull("overallPhase", filter.GetQueryParam());
        }

        foreach (var filter in queryString.SelectedNurseryProvisions)
        {
            query.AddIfNotNull("nurseryProvision", filter.GetQueryParam());
        }

        foreach (var filter in queryString.SelectedSpecialProvisions)
        {
            query.AddIfNotNull("specialClassesProvision", filter.GetQueryParam());
        }

        foreach (var filter in queryString.SelectedSixthFormProvisions)
        {
            query.AddIfNotNull("sixthFormProvision", filter.GetQueryParam());
        }

        return query;
    }

    private record ParsedQueryString(
        bool AllRows,
        bool FiltersVisible,
        Dimensions.ResultAsOptions ResultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions,
        string Sort);
}