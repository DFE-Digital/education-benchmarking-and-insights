using System.Text;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class TextResponseBody(byte[] content) : ApiResponseBody(content)
{
    public string Payload => Encoding.UTF8.GetString(Content);
}