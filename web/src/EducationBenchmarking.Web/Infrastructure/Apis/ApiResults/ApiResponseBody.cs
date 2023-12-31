﻿namespace EducationBenchmarking.Web.Infrastructure.Apis;

public abstract class ApiResponseBody
{
    public byte[] Content { get; }
    public bool HasContent => Content is { Length: > 0 };
    
    public ApiResponseBody(byte[] content)
    {
        Content = content;
    }
    
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

