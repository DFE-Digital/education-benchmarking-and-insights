namespace Web.App.Infrastructure.Apis
{
    public class PdfResponseBody(byte[] content) : ApiResponseBody(content);
}