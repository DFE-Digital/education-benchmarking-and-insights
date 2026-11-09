using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolComparisonHorizontalBarChartRequest : PostHorizontalBarChartRequest<SchoolComparisonDatum>
{
    public SchoolComparisonHorizontalBarChartRequest(
        string uuid,
        string urn,
        SchoolComparisonDatum[] filteredData,
        Func<string, string?> linkFormatter,
        SchoolSpendingDimensions.ResultAsOptions resultsAs,
        string comparatorSetType,
        SchoolSpendingDimensions.BandingsAsOptions[] bandingsAs,
        bool displayAttainment8)
    {
        BarHeight = 22;
        Data = filteredData;
        HighlightKey = urn;
        Id = uuid;
        KeyField = nameof(SchoolComparisonDatum.Urn).ToLower();
        LabelField = "schoolName";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter("%1$s");
        MissingDataLabel = "No data submitted";
        Sort = "desc";
        Width = 610;
        ValueField = nameof(SchoolComparisonDatum.Expenditure).ToLower();
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel(comparatorSetType);

        GroupedKeys = new ChartRequestGroupedKeys();

        var partYear = filteredData
            .Where(x => x.PeriodCoveredByReturn is not 12)
            .Select(x => x.Urn!)
            .ToArray();
        if (partYear.Length > 0)
        {
            GroupedKeys[GroupType.PartYear] = partYear;
        }

        if (bandingsAs is not { Length: > 0 } && !displayAttainment8)
        {
            return;
        }

        var legendLabels = new List<string>
        {
            ChartLegendLabels.ChosenSchool
        };

        // use only full year records for performance to prevent overlap with part‑year grouping
        var fullYearData = filteredData
            .Where(x => x.PeriodCoveredByReturn == 12)
            .ToArray();

        if (bandingsAs.Contains(SchoolSpendingDimensions.BandingsAsOptions.WellAbove))
        {
            AddBandingGroup(
                fullYearData,
                KS4ProgressBandings.Banding.WellAboveAverage,
                GroupType.Progress8WellAboveAverage);
            legendLabels.Add(KS4ProgressBandings.Banding.WellAboveAverage.ToStringValue());
        }

        if (bandingsAs.Contains(SchoolSpendingDimensions.BandingsAsOptions.Above))
        {
            AddBandingGroup(
                fullYearData,
                KS4ProgressBandings.Banding.AboveAverage,
                GroupType.Progress8AboveAverage);
            legendLabels.Add(KS4ProgressBandings.Banding.AboveAverage.ToStringValue());
        }

        if (displayAttainment8)
        {
            var items = fullYearData
                .Where(x => x.TopRankedAttainment8)
                .Select(x => x.Urn!)
                .ToArray();

            if (items.Length > 0)
            {
                GroupedKeys?[GroupType.TopRankedAttainment8] = items;
                legendLabels.Add(ChartLegendLabels.TopRankedAttainment8);
            }
        }

        LegendLabels = [.. legendLabels];
    }

    private void AddBandingGroup(
        SchoolComparisonDatum[] data,
        KS4ProgressBandings.Banding banding,
        string groupType)
    {
        var items = data
            .Where(x => x.ProgressBanding == banding.ToStringValue())
            .Select(x => x.Urn!)
            .ToArray();

        if (items.Length > 0)
        {
            GroupedKeys?[groupType] = items;
        }
    }
}
