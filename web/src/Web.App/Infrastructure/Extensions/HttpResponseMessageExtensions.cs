using Web.App.Infrastructure.Apis;

namespace Web.App.Infrastructure.Extensions;

public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResult> ToApiResult(this Task<HttpResponseMessage> message, CancellationToken cancellationToken = default)
    {
        try
        {
            var msg = await message;
            return await ApiResult.FromHttpResponse(msg, cancellationToken);
        }
        catch (TaskCanceledException) when (cancellationToken.IsCancellationRequested)
        {
            return ApiResult.Cancelled();
        }
    }
}