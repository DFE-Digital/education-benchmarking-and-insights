using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.FeatureManagement.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.TagHelpers;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[FeatureGate(FeatureFlags.LocalAuthorities)]
[Route("local-authority/{code}")]
public class LocalAuthorityController(
    ILogger<LocalAuthorityController> logger,
    IEstablishmentApi establishmentApi)
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

                var authority = await LocalAuthority(code);
                var schools = await LocalAuthoritySchools(code);

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

    [HttpGet]
    [Route("find-ways-to-spend-less")]
    public async Task<IActionResult> Resources(string code)
    {
        using (logger.BeginScope(new { code }))
        {
            try
            {
                ViewData[ViewDataKeys.Backlink] = HomeLink(code);

                var authority = await LocalAuthority(code);

                var viewModel = new LocalAuthorityViewModel(authority);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying local authority resources: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<School[]> LocalAuthoritySchools(string code) => await establishmentApi
        .QuerySchools(new ApiQuery().AddIfNotNull("laCode", code))
        .GetResultOrDefault<School[]>() ?? [];

    private async Task<LocalAuthority> LocalAuthority(string code) => await establishmentApi
        .GetLocalAuthority(code)
        .GetResultOrThrow<LocalAuthority>();

    private BacklinkInfo HomeLink(string code) => new(Url.Action("Index", new { code }));
}
