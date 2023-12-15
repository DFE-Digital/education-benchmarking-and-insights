using EducationBenchmarking.Web.Extensions;
using EducationBenchmarking.Web.Infrastructure.Extensions;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class JsonResponseBody : ApiResponseBody
{
    public JsonResponseBody(byte[] content) : base(content)
    {
    }

    public JsonResponseBody(object content) : base(content.ToJsonByteArray())
    {
    }

    public T ReadAs<T>()
    {
        return Content.FromJson<T>();
    }
}