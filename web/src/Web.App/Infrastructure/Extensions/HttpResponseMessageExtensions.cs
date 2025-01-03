using System.Diagnostics.CodeAnalysis;
using Web.App.Infrastructure.Apis;
namespace Web.App.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResult> ToApiResult(this Task<HttpResponseMessage> message, CancellationToken cancellationToken = default)
    {
        var msg = await message;
        return await ApiResult.FromHttpResponse(msg, cancellationToken);
    }

    public static async Task<ApiResult> ToApiResult(this HttpResponseMessage message, CancellationToken cancellationToken = default) =>
        await ApiResult.FromHttpResponse(message, cancellationToken);
}