using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Threading.Tasks;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Platform.Json;

namespace Platform.MaintenanceTasks.Features.UserDataCleanUp;

[ExcludeFromCodeCoverage]
public class CleanUpFunctions(ILogger<CleanUpFunctions> logger, IPlatformDb db)
{
    [Function("CleanUpFunction")]
    public async Task RunAsync([TimerTrigger("0 0 0 * * *")] TimerInfo timer)
    {
        using (logger.BeginScope(new Dictionary<string, object>
               {
                   { "Application", Constants.CleanUp }
               }))
        {
            try
            {
                var records = await db.GetUserDataForDeletion();

                foreach (var record in records)
                {
                    if (record.Id != null)
                    {
                        logger.LogInformation($"Removing data : {record.Id} ({record.Type})");
                        switch (record.Type)
                        {
                            case "comparator-set" when record.OrganisationType == "school":
                                await db.RemoveSchoolComparatorSet(record.Id);
                                break;
                            case "comparator-set" when record.OrganisationType == "trust":
                                await db.RemoveTrustComparatorSet(record.Id);
                                break;
                            case "custom-data":
                                await db.RemoveCustomData(record.Id);
                                break;
                        }
                    }
                    else
                    {
                        logger.LogWarning($"Empty record id data : {record.ToJson()}");
                    }
                }
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to clean up user data");
            }
        }
    }
}