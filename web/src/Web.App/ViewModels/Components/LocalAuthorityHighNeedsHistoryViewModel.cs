using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHistoryViewModel(string code, HighNeedsHistory<HighNeedsYear>? history)
{
    public string Code => code;

    public IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> History =>
        history == null ? [] : history.MapToDashboardResponse(code);
}

public class LocalAuthorityHighNeedsHistoryTableViewModel(
    string heading,
    IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> history,
    Func<LocalAuthorityHighNeedsHistoryDashboardResponse, decimal?> valueFunc,
    Func<LocalAuthorityHighNeedsHistoryDashboardResponse, decimal?> differenceFunc)
{
    public string Heading => heading;
    public IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> History => history;
    public Func<LocalAuthorityHighNeedsHistoryDashboardResponse, decimal?> ValueFunc => valueFunc;
    public Func<LocalAuthorityHighNeedsHistoryDashboardResponse, decimal?> DifferenceFunc => differenceFunc;
}