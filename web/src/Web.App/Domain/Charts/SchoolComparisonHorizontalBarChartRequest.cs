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
        string comparatorSetType)
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

        var partYear = filteredData
            .Where(x => x.PeriodCoveredByReturn is not 12)
            .Select(x => x.Urn!)
            .ToArray();
        if (partYear.Length > 0)
        {
            GroupedKeys = new ChartRequestGroupedKeys
            {
                [GroupType.PartYear] = partYear
            };
        }
    }
}
