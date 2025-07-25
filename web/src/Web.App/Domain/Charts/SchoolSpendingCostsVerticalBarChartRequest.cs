using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolSpendingCostsVerticalBarChartRequest : PostVerticalBarChartRequest<PriorityCostCategoryDatum>
{
    public SchoolSpendingCostsVerticalBarChartRequest(string uuid, string urn, PriorityCostCategoryDatum[] filteredData)
    {
        Data = filteredData;
        Height = 200;
        HighlightKey = urn;
        Id = uuid;
        KeyField = nameof(PriorityCostCategoryDatum.Urn).ToLower();
        Sort = "asc";
        Width = 630;
        ValueField = nameof(PriorityCostCategoryDatum.Amount).ToLower();
    }
}

public class PriorityCostCategoryDatum
{
    public string? Urn { get; init; }
    public decimal? Amount { get; init; }
}