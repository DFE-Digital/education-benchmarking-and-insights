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

    [FunctionName(nameof(SingleFinanceYearsAsync))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public IActionResult SingleFinanceYearsAsync(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "finance-years")]
        HttpRequest req)
    {
        return new JsonContentResult(new { Academies = 2022, MaintainedSchools = 2021 });
    }
}