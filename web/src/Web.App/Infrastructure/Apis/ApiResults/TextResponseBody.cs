using System.Text;

namespace Web.App.Infrastructure.Apis
{
    public class TextResponseBody(byte[] content) : ApiResponseBody(content)
    {
        public string Payload => Encoding.UTF8.GetString(Content);
    }
}