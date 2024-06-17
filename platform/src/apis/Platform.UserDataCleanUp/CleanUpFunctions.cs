using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Azure.WebJobs;
using Microsoft.Extensions.Logging;
using Platform.Functions.Extensions;

namespace Platform.UserDataCleanUp;

public class CleanUpFunctions
{
    private readonly ILogger<CleanUpFunctions> _logger;
    private readonly IPlatformDb _db;

    public CleanUpFunctions(ILogger<CleanUpFunctions> logger, IPlatformDb db)
    {
        _logger = logger;
        _db = db;
    }

    [FunctionName("CleanUpFunction")]
    public async Task RunAsync([TimerTrigger("0 0 12 * * *")] TimerInfo timer)
    {
        using (_logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.ApplicationName }
               }))
        {
            try
            {
                var records = await _db.GetUserDataForDeletion();

                foreach (var record in records)
                {
                    if (record.Id != null)
                    {
                        _logger.LogInformation($"Removing data : {record.Id} ({record.Type})");
                        switch (record.Type)
                        {
                            case "comparator-set" when record.OrganisationType == "school":
                                await _db.RemoveSchoolComparatorSet(record.Id);
                                break;
                            case "comparator-set" when record.OrganisationType == "trust":
                                await _db.RemoveTrustComparatorSet(record.Id);
                                break;
                            case "custom-data":
                                await _db.RemoveCustomData(record.Id);
                                break;
                        }
                    }
                    else
                    {
                        _logger.LogWarning($"Empty record id data : {record.ToJson()}");
                    }

                }
            }
            catch (Exception e)
            {
                _logger.LogError(e, "Failed to clean up user data");
            }
        }
    }
}