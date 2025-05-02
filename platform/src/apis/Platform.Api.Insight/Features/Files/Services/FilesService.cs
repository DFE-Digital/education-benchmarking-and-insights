using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;
using Platform.Api.Insight.Features.Files.Models;
using Platform.Sql;
using Platform.Sql.QueryBuilders;

namespace Platform.Api.Insight.Features.Files.Services;

public interface IFilesService
{
    Task<IEnumerable<FileModel>> GetActiveFilesByType(CancellationToken cancellationToken = default, params string[] types);
}

[ExcludeFromCodeCoverage]
public class FilesService(IDatabaseFactory dbFactory) : IFilesService
{
    public async Task<IEnumerable<FileModel>> GetActiveFilesByType(CancellationToken cancellationToken = default, params string[] types)
    {
        using var conn = await dbFactory.GetConnection();
        var filesBuilder = new ActiveFilesQuery(FileModel.Fields)
            .WhereTypeIn(types);

        return await conn.QueryAsync<FileModel>(filesBuilder, cancellationToken);
    }
}