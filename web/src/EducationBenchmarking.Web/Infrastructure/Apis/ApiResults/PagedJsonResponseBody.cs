using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class PagedJsonResponseBody : ApiResponseBody
{
    public PagedJsonResponseBody(byte[] content) : base(content)
    {
    }

    public PagedJsonResponseBody(object content) : base(content.ToJsonByteArray())
    {
    }

    public PagedResults<T> ReadAs<T>()
    {
        return Content.FromJson<PagedResults<T>>();
    }
}