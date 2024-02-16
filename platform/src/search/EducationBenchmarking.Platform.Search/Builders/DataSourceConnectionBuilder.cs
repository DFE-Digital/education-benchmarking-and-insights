using Azure.Search.Documents.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders;

public abstract class DataSourceConnectionBuilder
{
    public abstract string Name { get; }
    public abstract Task Build(SearchIndexerClient client);
}