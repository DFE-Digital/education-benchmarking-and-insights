using System.Diagnostics.CodeAnalysis;
using System.Text;
using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Apis;

namespace EducationBenchmarking.Web.Infrastructure.Extensions;

[ExcludeFromCodeCoverage]
public static class ApiResultExtensions
{
    public static async Task EnsureSuccess(this Task<ApiResult> result)
    {
        var rs = await result;
        rs.EnsureSuccess();
    }

    public static async Task<T> GetResultOrThrow<T>(this Task<ApiResult> result)
    {
        var rs = await result;
        return rs.GetResultOrThrow<T>();
    }

    public static async Task<TRet> GetResult<TRet>(this Task<ApiResult> result, Func<ApiResponseBody, TRet> onSelect, Func<ApiResult, TRet> onError)
    {
        var rs = await result;

        return rs switch
        {
            SuccessApiResult success => onSelect(success.Body),
            _ => onError(rs)
        };
    }

    public static async Task<TRet?> GetResult<TRet>(this Task<ApiResult> result, Func<ApiResult, TRet> onError)
    {
        var rs = await result;
        if (rs is not SuccessApiResult s) return onError(rs);

        return s.Body switch
        {
            JsonResponseBody j => j.Content.FromJson<TRet>(),
            TextResponseBody t => t.Payload.FromJson<TRet>(),
            _ => default
        };
    }

    public static async Task<string> GetString(this Task<ApiResult> result, Func<ApiResult, string> onError)
    {
        var rs = await result;
        if (rs is not SuccessApiResult s) return onError(rs);

        return s.Body switch
        {
            JsonResponseBody j => Encoding.UTF8.GetString(j.Content),
            TextResponseBody t => t.Payload,
            _ => ""
        };
    }

    public static async Task<PagedResults<T>> GetPagedResultOrThrow<T>(this Task<ApiResult> result)
    {
        var rs = await result;
        return rs.GetPagedResultOrThrow<T>();
    }

    public static async Task<T?> GetResultOrDefault<T>(this Task<ApiResult> result, T? defaultValue = default)
    {
        var rs = await result;
        return rs.GetResultOrDefault(defaultValue);
    }

    public static T? GetResultOrDefault<T>(this ApiResult result, T? defaultValue = default)
    {
        if (result is not SuccessApiResult s) return defaultValue;
        return s.Body switch
        {
            JsonResponseBody j => j.ReadAs<T>(),
            TextResponseBody t => (T)Convert.ChangeType(t.Payload, typeof(T)),
            _ => defaultValue
        };
    }

    public static T GetResultOrThrow<T>(this ApiResult result)
    {
        result.EnsureSuccess();
        if (result is SuccessApiResult s)
        {
            return s.Body switch
            {
                JsonResponseBody j => j.ReadAs<T>(),
                TextResponseBody t => (T)Convert.ChangeType(t.Payload, typeof(T)),
                PdfResponseBody p => (T)Convert.ChangeType(p, typeof(T)),
                _ => throw new ArgumentOutOfRangeException(s.Body.GetType().Name)
            };
        }

        throw new ArgumentNullException();
    }

    public static byte[] GetBodyOrThrow(this ApiResult result)
    {
        if (result is SuccessApiResult s)
        {
            return s.Body.Content;
        }

        result.EnsureSuccess();
        return Array.Empty<byte>();
    }

    public static async Task<byte[]> GetBodyOrThrow(this Task<ApiResult> result)
    {
        var rs = await result;
        return GetBodyOrThrow(rs);
    }

    public static PagedResults<T> GetPagedResultOrThrow<T>(this ApiResult result)
    {
        if (result is SuccessApiResult s)
        {
            return s.Body switch
            {
                PagedJsonResponseBody j => j.ReadAs<T>(),
                _ => throw new ArgumentOutOfRangeException()
            };
        }

        result.EnsureSuccess();
        return new PagedResults<T>();
    }
}