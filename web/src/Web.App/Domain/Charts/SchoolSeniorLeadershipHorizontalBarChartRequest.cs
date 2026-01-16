using System.Text.Json;
using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolSeniorLeadershipHorizontalBarChartRequest : PostHorizontalBarChartRequest<SeniorLeadershipGroup>
{
    // TODO: Move these into the base type once the Web Chart Rendering contract is finalised.
    // They may become a single combined type for easier mapping.
    // For now, LegendLabels must align with ValueField by index.
    public new string[]? ValueField { get; set; }
    public string[]? LegendLabels { get; set; }

    public SchoolSeniorLeadershipHorizontalBarChartRequest(
        string uuid,
        string urn,
        SeniorLeadershipGroup[] data,
        Func<string, string?> linkFormatter,
        CensusDimensions.ResultAsOptions resultsAs)
    {
        BarHeight = 22;
        Data = data;
        HighlightKey = urn;
        Id = uuid;
        KeyField = nameof(SeniorLeadershipGroup.URN).ToLower();
        LabelField = "schoolName";
        LabelFormat = "%2$s";
        LinkFormat = linkFormatter("%1$s");
        Sort = "desc";
        Width = 610;
        ValueField =
        [
            JsonNamingPolicy.CamelCase.ConvertName(nameof(SeniorLeadershipGroup.HeadTeacher)),
            JsonNamingPolicy.CamelCase.ConvertName(nameof(SeniorLeadershipGroup.DeputyHeadTeacher)),
            JsonNamingPolicy.CamelCase.ConvertName(nameof(SeniorLeadershipGroup.AssistantHeadTeacher)),
            JsonNamingPolicy.CamelCase.ConvertName(nameof(SeniorLeadershipGroup.LeadershipNonTeacher))
        ];
        LegendLabels = ["Head teachers", "Deputy head teachers", "Assistant head teachers", "Leadership non‑teachers"];
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel();
    }
}