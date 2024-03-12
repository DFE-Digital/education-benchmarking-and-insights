using Azure.Search.Documents.Indexes;

namespace Platform.Search.Builders;

public abstract class IndexBuilder
{
    public abstract string Name { get; }
    public abstract Task Build(SearchIndexClient client);
}