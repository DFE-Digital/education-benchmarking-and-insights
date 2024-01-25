using EducationBenchmarking.Web.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class JsonResponseBody(byte[] content) : ApiResponseBody(content)
{
    public JsonResponseBody(object content) : this(content.ToJsonByteArray())
    {
    }

    public T ReadAs<T>()
    {
        return Content.FromJson<T>();
    }
}