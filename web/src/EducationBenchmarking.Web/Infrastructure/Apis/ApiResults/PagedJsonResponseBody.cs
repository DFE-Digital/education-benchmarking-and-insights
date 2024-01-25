using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PagedJsonResponseBody(byte[] content) : ApiResponseBody(content)
{
    public PagedJsonResponseBody(object content) : this(content.ToJsonByteArray())
    {
    }

    public PagedResults<T> ReadAs<T>()
    {
        return Content.FromJson<PagedResults<T>>();
    }
}