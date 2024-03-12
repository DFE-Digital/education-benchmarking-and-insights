using System.Text;
using Web.App.Infrastructure.Apis;
using Xunit;

namespace Web.Tests.Infrastructure;

public class GivenAHttpContent
{
    [Fact]
    public async Task ShouldReturnJsonResponseBody()
    {
        // arrange
        var expected = Encoding.UTF8.GetBytes("test");
        var testHttpContent = new ByteArrayContent(expected);
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<JsonResponseBody>(result);
        Assert.True(result.HasContent);
        Assert.Equal(result.Content, expected);
    }


    [Fact]
    public async Task ShouldReturnPagedJsonResponseBody()
    {
        // arrange       
        var expected = Encoding.UTF8.GetBytes("test");
        var testHttpContent = new ByteArrayContent(expected);
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json+paged");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<PagedJsonResponseBody>(result);
        Assert.True(result.HasContent);
        Assert.Equal(result.Content, expected);
    }


    [Fact]
    public async Task ShouldReturnTextResponseBody()
    {
        // arrange       
        var expected = Encoding.UTF8.GetBytes("test");
        var testHttpContent = new ByteArrayContent(expected);
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<TextResponseBody>(result);
        Assert.True(result.HasContent);
        Assert.Equal(result.Content, expected);
    }


    [Fact]
    public async Task ShouldReturnEmptyResponseBody()
    {
        // arrange
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/html");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<EmptyResponseBody>(result);
        Assert.False(result.HasContent);
    }


    [Fact]
    public async Task ShouldReturnCsvResponseBody()
    {
        // arrange       
        var expected = Encoding.UTF8.GetBytes("test");
        var testHttpContent = new ByteArrayContent(expected);
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/csv");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<CsvResponseBody>(result);
        Assert.True(result.HasContent);
        Assert.Equal(result.Content, expected);
    }


    [Fact]
    public async Task ShouldReturnPdfResponseBody()
    {
        // arrange       
        var expected = Encoding.UTF8.GetBytes("test");
        var testHttpContent = new ByteArrayContent(expected);
        testHttpContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/pdf");

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<PdfResponseBody>(result);
        Assert.True(result.HasContent);
        Assert.Equal(result.Content, expected);
    }


    [Fact]
    public async Task ShouldReturnEmptyResponseBodyWhenContentLengthZero()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes(""));

        // act
        var result = await ApiResponseBody.FromHttpContent(testHttpContent);

        // assert
        Assert.IsType<EmptyResponseBody>(result);
        Assert.False(result.HasContent);
    }


    [Fact]
    public async Task ShouldThrowException()
    {
        // arrange       
        var testHttpContent = new ByteArrayContent(Encoding.UTF8.GetBytes("test"));

        // act + assert
        await Assert.ThrowsAsync<ArgumentException>(() => ApiResponseBody.FromHttpContent(testHttpContent));
    }
}