using System.Text.Json;
using Web.App.Infrastructure.Apis;

// ReSharper disable ClassNeverInstantiated.Global

namespace Web.App.Domain.Charts;

public record SchoolSeniorLeadershipHorizontalBarChartRequest : PostHorizontalBarChartRequest<SeniorLeadershipGroup>
{
    public new string[]? ValueField { get; set; }

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
        ValueType = resultsAs.GetValueType();
        XAxisLabel = resultsAs.GetXAxisLabel();
    }
}