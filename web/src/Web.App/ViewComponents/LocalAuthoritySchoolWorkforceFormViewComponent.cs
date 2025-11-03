using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Apis.LocalAuthorities;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolWorkforceFormViewComponent(IHttpContextAccessor contextAccessor, ILocalAuthoritiesApi localAuthoritiesApi) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(
        string code,
        string formPrefix,
        int maxRows,
        string defaultSort,
        string otherFormPrefix,
        string tabId)
    {
        var query = ParseQueryString(contextAccessor.HttpContext?.Request.Query, formPrefix, defaultSort);
        var results = await localAuthoritiesApi
            .GetSchoolsWorkforce(code, BuildQuery(query, maxRows))
            .GetResultOrDefault<LocalAuthoritySchoolWorkforce[]>() ?? [];

        var (allRows,
            filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort) = query;
        var otherFormValues = contextAccessor.HttpContext?.Request.Query
            .Where(q => q.Key.StartsWith(otherFormPrefix))
            .ToDictionary();

        var viewModel = new LocalAuthoritySchoolWorkforceFormViewModel(
            code,
            formPrefix,
            maxRows,
            defaultSort,
            tabId,
            otherFormValues,
            contextAccessor.HttpContext?.Request.Path,
            contextAccessor.HttpContext?.Request.Query)
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

    private static ParsedQueryString ParseQueryString(IQueryCollection? query, string formPrefix, string defaultSort)
    {
        var resultAs = SchoolsSummaryWorkforceDimensions.ResultAsOptions.PercentPupil;
        if (query == null || query.Count == 0)
        {
            return new ParsedQueryString(false, false, resultAs, [], [], [], [], defaultSort);
        }

        var filtersVisible = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.FiltersVisible}"] == LocalAuthoritySchoolWorkforceFormViewModel.FormFieldValues.Show;

        var resultsAsValues = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.ResultAs}"]
            .CastQueryToEnum<SchoolsSummaryWorkforceDimensions.ResultAsOptions>()
            .ToArray();
        if (resultsAsValues.Length > 0)
        {
            resultAs = resultsAsValues.First();
        }

        var selectedOverallPhases = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.SelectedOverallPhases}"]
            .CastQueryToEnum<OverallPhaseTypes.OverallPhaseTypeFilter>()
            .ToArray();
        var selectedNurseryProvisions = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.SelectedNurseryProvisions}"]
            .CastQueryToEnum<NurseryProvisions.NurseryProvisionFilter>()
            .ToArray();
        var selectedSpecialProvisions = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.SelectedSpecialProvisions}"]
            .CastQueryToEnum<SpecialProvisions.SpecialProvisionFilter>()
            .ToArray();
        var selectedSixthFormProvisions = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.SelectedSixthFormProvisions}"]
            .CastQueryToEnum<SixthFormProvisions.SixthFormProvisionFilter>()
            .ToArray();

        var sortValues = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.Sort}"];
        var sort = sortValues == StringValues.Empty ? defaultSort : sortValues.ToString();
        var allRows = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.Rows}"] == LocalAuthoritySchoolWorkforceFormViewModel.FormFieldValues.All;

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
        SchoolsSummaryWorkforceDimensions.ResultAsOptions ResultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions,
        string Sort);
}