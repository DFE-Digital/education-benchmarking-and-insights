using Azure.Search.Documents.Indexes;

namespace EducationBenchmarking.Platform.Search.Builders;

public abstract class IndexerBuilder
{
    public abstract string Name { get;}
    public abstract Task Build(SearchIndexerClient client);
    public abstract Task Reset(SearchIndexerClient client);
}