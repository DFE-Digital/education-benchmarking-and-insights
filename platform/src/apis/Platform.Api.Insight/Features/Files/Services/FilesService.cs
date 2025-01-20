using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Files.Models;
using Platform.Sql;

namespace Platform.Api.Insight.Features.Files.Services;

public interface IFilesService
{
    Task<IEnumerable<FileModel>> GetAarTransparencyFiles();
    Task<IEnumerable<FileModel>> GetCfrTransparencyFiles();
}

[ExcludeFromCodeCoverage]
public class FilesService(IDatabaseFactory dbFactory) : IFilesService
{
    public Task<IEnumerable<FileModel>> GetAarTransparencyFiles() => GetTransparencyFiles("VW_TransparencyFilesAar");

    public Task<IEnumerable<FileModel>> GetCfrTransparencyFiles() => GetTransparencyFiles("VW_TransparencyFilesCfr");

    private async Task<IEnumerable<FileModel>> GetTransparencyFiles(string view)
    {
        var sql = $"SELECT [Label], [FileName] from {view} ORDER BY [Label]";
        using var conn = await dbFactory.GetConnection();
        return await conn.QueryAsync<FileModel>(sql);
    }
}