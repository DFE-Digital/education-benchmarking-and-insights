using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Platform.Functions;

namespace Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Miscellaneous")]
public class MiscFunctions
{

    [FunctionName(nameof(SingleCurrentReturnYearsAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    public IActionResult SingleCurrentReturnYearsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "current-return-years")]
        HttpRequest req)
    {
        var aar = Environment.GetEnvironmentVariable("Cosmos__AarLatestYear");
        var cfr = Environment.GetEnvironmentVariable("Cosmos__CfrLatestYear");

        return new JsonContentResult(new
        {
            aar,
            cfr
        });
    }
}