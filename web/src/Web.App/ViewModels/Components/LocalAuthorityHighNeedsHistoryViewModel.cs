using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHistoryViewModel(string code, HighNeedsHistory<HighNeedsYear>? history, string? commentary)
{
    public string Code => code;
    public string? Commentary => commentary;

    public IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> History =>
        history == null ? [] : history.MapToDashboardResponse(code);
}