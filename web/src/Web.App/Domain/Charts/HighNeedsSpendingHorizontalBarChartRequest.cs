using Microsoft.Azure.Cosmos.Linq;
using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record HighNeedsSpendingHorizontalBarChartRequest : PostHorizontalBarChartRequest<HighNeedsSpendingComparisonDatum>
{
    public HighNeedsSpendingHorizontalBarChartRequest(
        string uuid,
        string code,
        HighNeedsSpendingComparisonDatum[] filteredData,
        HighNeedsDimensions.ResultAsOptions resultAs,
        HighNeedsDimensions.SubmissionTypeOptions type,
        Func<string, string?>? linkFormatter = null)
    {
        BarHeight = 22;
        Data = filteredData;
        HighlightKey = code;
        Id = uuid;
        KeyField = nameof(HighNeedsSpendingComparisonDatum.Code).ToLower();
        LabelField = "name";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter == null
            ? string.Empty
            : linkFormatter.Invoke("%1$s");
        MissingDataLabel = "No data submitted";
        Sort = "desc";
        Width = 610;
        ValueField = nameof(HighNeedsSpendingComparisonDatum.Expenditure).ToLower();
        ValueType = Web.App.Domain.Charts.ValueType.Currency;
        XAxisLabel = $"{type.GetSubmissionTypeDescription()} ({resultAs.GetResultAsDescription()})";
    }
}

public class HighNeedsSpendingComparisonDatum
{
    public string? Code { get; init; }
    public string? Name { get; init; }
    public decimal? Expenditure { get; init; }
    public decimal? TotalPupils { get; init; }
}
