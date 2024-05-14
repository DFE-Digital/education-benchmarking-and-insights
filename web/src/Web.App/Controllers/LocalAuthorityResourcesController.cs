using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("local-authority/{code}/find-ways-to-spend-less")]
public class LocalAuthorityResourcesController(
    ILogger<LocalAuthorityResourcesController> logger, IEstablishmentApi establishmentApi) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string code)
    {

        using (logger.BeginScope(new { code }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = new BacklinkInfo(Url.Action("Index", "LocalAuthority", new { code }));

                var authority = await establishmentApi.GetLocalAuthority(code).GetResultOrThrow<LocalAuthority>();
                var viewModel = new LocalAuthorityResourcesViewModel(authority);

                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}
