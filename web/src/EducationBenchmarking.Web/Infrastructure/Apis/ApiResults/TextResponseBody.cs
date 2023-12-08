using System.Text;

namespace EducationBenchmarking.Web.Infrastructure.Apis;

public class TextResponseBody : ApiResponseBody
{
    public TextResponseBody(byte[] content) : base(content)
    {
    }

    public string Payload => Encoding.UTF8.GetString(Content);
}