using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("trust/{companyNumber}/details")]
public class TrustDetailsController(ILogger<TrustDetailsController> logger, IEstablishmentApi establishmentApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        try
        {
            ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "Trust", new { companyNumber }));

            var trust = await establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
            var trustQuery = new ApiQuery().AddIfNotNull("companyNumber", companyNumber);
            var schools = await establishmentApi.QuerySchools(trustQuery).GetResultOrDefault<School[]>() ?? [];

            var viewModel = new TrustDetailsViewModel(trust, schools);

            return View(viewModel);
        }
        catch (Exception e)
        {
            logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
            return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
        }
    }
}
