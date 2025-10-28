using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Primitives;
using Web.App.Domain;
using Web.App.Domain.LocalAuthorities;
using Web.App.Extensions;
using Web.App.Infrastructure.Apis;
using Web.App.ViewModels.Components;

namespace Web.App.ViewComponents;

public class LocalAuthoritySchoolWorkforceViewComponent : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string code, string formPrefix, int maxRows, string defaultSort)
    {
        var query = ParseQueryString(Request.Query, formPrefix, defaultSort);

        // TODO: replace stubbed out schools with api call once work on backend complete
        var results = new[]
        {
            new LocalAuthoritySchoolWorkforce
            {
                Urn = "000001",
                SchoolName = "Stub 1",
                PeriodCoveredByReturn = 12,
                TotalPupils = 320,
                PupilTeacherRatio = 18.5m,
                EhcPlan = 12.3m,
                SenSupport = 8.7m
            },
            new LocalAuthoritySchoolWorkforce
            {
                Urn = "000002",
                SchoolName = "Stub 2",
                PeriodCoveredByReturn = 11,
                TotalPupils = 450,
                PupilTeacherRatio = 16.2m,
                EhcPlan = 10.1m,
                SenSupport = 9.4m
            },
            new LocalAuthoritySchoolWorkforce
            {
                Urn = "000003",
                SchoolName = "Stub 3",
                PeriodCoveredByReturn = 12,
                TotalPupils = 780,
                PupilTeacherRatio = 14.8m,
                EhcPlan = 7.9m,
                SenSupport = 6.5m
            }
        };

        var (allRows,
            filtersVisible,
            resultAs,
            selectedOverallPhases,
            selectedNurseryProvisions,
            selectedSpecialProvisions,
            selectedSixthFormProvisions,
            sort) = query;
        var viewModel = new LocalAuthoritySchoolWorkforceViewModel(code, formPrefix, maxRows, defaultSort)
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
        var filtersVisible = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.FiltersVisible}"] == LocalAuthoritySchoolWorkforceFormViewModel.FormFieldValues.Show;

        var resultAs = WorkforceDimensions.ResultAsOptions.PercentPupil;
        var resultsAsValues = query[$"{formPrefix}{LocalAuthoritySchoolWorkforceFormViewModel.FormFieldNames.ResultAs}"]
            .CastQueryToEnum<WorkforceDimensions.ResultAsOptions>()
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
        WorkforceDimensions.ResultAsOptions ResultAs,
        OverallPhaseTypes.OverallPhaseTypeFilter[] SelectedOverallPhases,
        NurseryProvisions.NurseryProvisionFilter[] SelectedNurseryProvisions,
        SpecialProvisions.SpecialProvisionFilter[] SelectedSpecialProvisions,
        SixthFormProvisions.SixthFormProvisionFilter[] SelectedSixthFormProvisions,
        string Sort);
}