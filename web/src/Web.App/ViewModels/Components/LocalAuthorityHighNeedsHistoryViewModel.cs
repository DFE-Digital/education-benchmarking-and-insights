using Web.App.Controllers.Api.Mappers;
using Web.App.Controllers.Api.Responses;
using Web.App.Domain.LocalAuthorities;

namespace Web.App.ViewModels.Components;

public class LocalAuthorityHighNeedsHistoryViewModel(string code, HighNeedsHistory<LocalAuthorityHighNeedsYear>? history)
{
    public string Code => code;

    public IEnumerable<LocalAuthorityHighNeedsHistoryDashboardResponse> History =>
        history == null ? [] : history.MapToDashboardResponse(code);
}