namespace Web.App.Infrastructure.Apis;

public abstract class ApiResponseBody(byte[] content)
{
    public byte[] Content { get; } = content;
    public bool HasContent => Content is { Length: > 0 };

    public static async Task<ApiResponseBody> FromHttpContent(HttpContent content)
    {
        if (content is not { Headers.ContentLength: > 0 }) return new EmptyResponseBody();

        var bytes = await content.ReadAsByteArrayAsync();

        return content.Headers.ContentType?.MediaType switch
        {
            "application/json" => new JsonResponseBody(bytes),
            "application/json+paged" => new PagedJsonResponseBody(bytes),
            "text/plain" => new TextResponseBody(bytes),
            "text/html" => new EmptyResponseBody(),
            "text/csv" => new CsvResponseBody(bytes),
            "application/pdf" => new PdfResponseBody(bytes),
            _ => throw new ArgumentException($"Unknown MIME type: {content.Headers.ContentType?.MediaType}")
        };
    }
}