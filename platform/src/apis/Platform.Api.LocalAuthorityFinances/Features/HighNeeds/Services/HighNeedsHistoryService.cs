using System.Threading;
using System.Threading.Tasks;
using Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Models;

namespace Platform.Api.LocalAuthorityFinances.Features.HighNeeds.Services;

public interface IHighNeedsHistoryService
{
    Task<History<LocalAuthorityHighNeedsYear>?> GetHistory(string[] codes, CancellationToken cancellationToken = default);
}