using System.IO.Compression;
using System.Net;
using AutoFixture;
using Web.App.Domain;
using Xunit;

namespace Web.Integration.Tests.Proxy;

public class WhenRequestingCensusDownload : PageBase<SchoolBenchmarkingWebAppClient>
{
    private readonly Census[] _censuses;
    private readonly SchoolBenchmarkingWebAppClient _client;

    public WhenRequestingCensusDownload(SchoolBenchmarkingWebAppClient client) : base(client)
    {
        _client = client;
        _censuses = Fixture.Build<Census>().CreateMany().ToArray();
    }

    [Fact]
    public async Task CanReturnOk()
    {
        var school = Fixture.Build<School>()
            .With(x => x.URN, "12345")
            .Create();

        var comparatorSet = Fixture.Build<SchoolComparatorSet>()
            .With(x => x.Pupil, ["pupil"])
            .Create();

        var response = await _client
            .SetupEstablishment(school)
            .SetupComparatorSet(school, comparatorSet)
            .SetupCensus(_censuses)
            .Get(Paths.ApiCensusDownload(school.URN!, "school"));

        Assert.Equal(HttpStatusCode.OK, response.StatusCode);
        await foreach (var tuple in GetFilesFromZip(response))
        {
            Assert.Equal("census-12345.csv", tuple.fileName);

            var csvLines = tuple.content.Split(Environment.NewLine);
            Assert.Equal(
                "SchoolName,SchoolType,LAName,URN,TotalPupils,Workforce,WorkforceHeadcount,Teachers,SeniorLeadership,TeachingAssistant,NonClassroomSupportStaff,AuxiliaryStaff,PercentTeacherWithQualifiedStatus",
                csvLines.First());
            Assert.Equal(_censuses.Length, csvLines.Length - 1);
        }
    }

    [Fact]
    public async Task CanReturnInternalServerError()
    {
        const string urn = "12345";
        var response = await _client
            .SetupComparatorSetApiWithException()
            .SetupCensusWithException()
            .Get(Paths.ApiCensusDownload(urn, "school"));

        Assert.Equal(HttpStatusCode.InternalServerError, response.StatusCode);
    }

    private static async IAsyncEnumerable<(string fileName, string content)> GetFilesFromZip(HttpResponseMessage response)
    {
        var bytes = await response.Content.ReadAsByteArrayAsync();

        using var zipStream = new MemoryStream(bytes);
        using var archive = new ZipArchive(zipStream, ZipArchiveMode.Read);
        foreach (var entry in archive.Entries)
        {
            await using var entryStream = entry.Open();
            using var reader = new StreamReader(entryStream);
            var content = await reader.ReadToEndAsync();
            yield return (entry.Name, content);
        }
    }
}