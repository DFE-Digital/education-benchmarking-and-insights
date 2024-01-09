using System.Net;
using EducationBenchmarking.Platform.Functions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;

namespace EducationBenchmarking.Platform.Api.Insight;

[ApiExplorerSettings(GroupName = "Miscellaneous")]
public class MiscFunctions
{

    [FunctionName(nameof(GetFinanceYears))]
    [ProducesResponseType((int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
    public IActionResult GetFinanceYears(
        [HttpTrigger(AuthorizationLevel.Admin, "get", Route = "finance-years")]
        HttpRequest req)
    {
        return new JsonContentResult(new { Academies = 2022, MaintainedSchools = 2021 });
    }
}