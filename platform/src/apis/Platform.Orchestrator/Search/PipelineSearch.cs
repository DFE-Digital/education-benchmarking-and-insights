using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Azure;
using Microsoft.Extensions.Logging;
using Platform.Infrastructure;
namespace Platform.Orchestrator.Search;

public interface IPipelineSearch
{
    Task<bool> RunIndexerAll();
}

public class PipelineSearch(ILogger<PipelineSearch> logger, ISearchIndexerClient searchIndexerClient) : IPipelineSearch
{
    public async Task<bool> RunIndexerAll()
    {
        var results = new Dictionary<string, bool?>();
        foreach (var indexer in ResourceNames.Search.Indexers.All)
        {
            logger.LogInformation("Triggering {indexer} indexer on {endpoint}", indexer, searchIndexerClient.Endpoint);
            results[indexer] = null;
            var result = await RunIndexer(indexer);
            results[indexer] = result;
        }

        logger.LogInformation("Finished triggering indexers on {endpoint} ({passed}/{total} were successful)", searchIndexerClient.Endpoint, results.Count(d => d.Value == true), results.Count);
        return results.All(d => d.Value == true);
    }

    private async Task<bool> RunIndexer(string indexer)
    {
        Response? response = null;
        var success = false;

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
                    success = true;
                }
            }
        }

        return success;
    }
}