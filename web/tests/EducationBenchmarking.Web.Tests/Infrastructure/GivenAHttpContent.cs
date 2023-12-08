using EducationBenchmarking.Web.Infrastructure.Apis;
using System.Text;
using Xunit;

namespace EducationBenchmarking.Web.Tests.Infrastructure;

public class GivenAHttpContent
{

    //"application/json" => new JsonResponseBody(bytes),
    [Fact]
    public async Task ShouldReturnJsonResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<JsonResponseBody>(result);
    }


    //"application/json+paged" => new PagedJsonResponseBody(bytes),
    [Fact]
    public async Task ShouldReturnPagedJsonResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json+paged");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<PagedJsonResponseBody>(result);
    }


    //"text/plain" => new TextResponseBody(bytes),
    [Fact]
    public async Task ShouldReturnTextResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<TextResponseBody>(result);
    }


    //"text/html" => new EmptyResponseBody(),
    [Fact]
    public async Task ShouldReturnEmptyResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<EmptyResponseBody>(result);
    }


    //"text/csv" => new CsvResponseBody(bytes),
    [Fact]
    public async Task ShouldReturnCsvResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        //testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<CsvResponseBody>(result);
    }


    //"application/pdf" => new PdfResponseBody(bytes),
    [Fact]
    public async Task ShouldReturnPdfResponseBodyGivenMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<PdfResponseBody>(result);
    }


    //if (content is not { Headers.ContentLength: > 0 }) return new EmptyResponseBody();
    [Fact]
    public async Task ShouldEmptyResponseBodyWithZeroContentLength()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentLength = 0;

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        //assert
        Assert.IsType<EmptyResponseBody>(result);
    }


    //_ => throw new ArgumentException($"Unknown MIME type: {content.Headers.ContentType?.MediaType}")
    [Fact]
    public async Task ShouldThrowExceptionWithUnknownMediaType()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        // testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("");

        // act + assert
        await Assert.ThrowsAsync<ArgumentException>(() => ApiResponseBody.FromHttpContent(testHttpContent));
    }


    // arrange 
    // act
    //assert




    // arrange 
    // act
    //assert
}