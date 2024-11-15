using System;
using System.Threading.Tasks;
using Azure;
using Microsoft.Extensions.Logging;
using Platform.Infrastructure;
namespace Platform.Orchestrator.Search;

public interface IPipelineSearch
{
    Task RunIndexerAll();
}

public class PipelineSearch(ILogger<PipelineSearch> logger, ISearchIndexerClient searchIndexerClient) : IPipelineSearch
{
    public async Task RunIndexerAll()
    {
        foreach (var indexer in ResourceNames.Search.Indexers.All)
        {
            logger.LogInformation("Triggering {indexer} indexer on {endpoint}", indexer, searchIndexerClient.Endpoint);
            await RunIndexer(indexer);
        }
    }

    private async Task RunIndexer(string indexer)
    {
        Response? response = null;
        try
        {
            response = await searchIndexerClient.RunIndexerAsync(indexer);
        }
        catch (Exception e)
        {
            logger.LogError(e, "{indexer} trigger failed: {message}", indexer, e.Message);
        }
        finally
        {
            if (response != null)
            {
                if (response.IsError)
                {
                    logger.LogWarning("{indexer} trigger returned {status} {reason}", indexer, response.Status, response.ReasonPhrase);
                }
                else
                {
                    logger.LogInformation("{indexer} trigger returned {status} {reason}", indexer, response.Status, response.ReasonPhrase);
                }
            }
        }
    }
}