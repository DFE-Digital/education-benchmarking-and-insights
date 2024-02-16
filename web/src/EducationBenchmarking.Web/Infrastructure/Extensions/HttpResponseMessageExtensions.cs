using System.Diagnostics.CodeAnalysis;
using EducationBenchmarking.Web.Infrastructure.Apis;

namespace EducationBenchmarking.Web.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class HttpResponseMessageExtensions
{
    public static async Task<ApiResult> ToApiResult(this Task<HttpResponseMessage> message)
    {
        var msg = await message;
        return await ApiResult.FromHttpResponse(msg);
    }

    public static async Task<ApiResult> ToApiResult(this HttpResponseMessage message)
    {
        return await ApiResult.FromHttpResponse(message);
    }
}