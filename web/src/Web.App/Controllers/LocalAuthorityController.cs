using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}")]
public class LocalAuthorityController(ILogger<LocalAuthorityController> logger, IEstablishmentApi establishmentApi)
    : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {
                ViewData[ViewDataKeys.BreadcrumbNode] = BreadcrumbNodes.LocalAuthorityHome(code);

                var authority = await establishmentApi.GetLocalAuthority(code).GetResultOrThrow<LocalAuthority>();
                var laQuery = new ApiQuery().AddIfNotNull("laCode", code);
                var schools = await establishmentApi.QuerySchools(laQuery).GetResultOrDefault<School[]>() ?? [];

                var viewModel = new LocalAuthorityViewModel(authority, schools);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}
