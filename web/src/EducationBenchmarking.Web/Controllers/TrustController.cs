using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using SmartBreadcrumbs.Nodes;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("trust/{companyNumber}")]
public class TrustController : Controller
{
    private readonly ILogger<TrustController> _logger;
    private readonly IEstablishmentApi _establishmentApi;
    private readonly IFinanceService _financeService;

    public TrustController(ILogger<TrustController> logger, IEstablishmentApi establishmentApi, IFinanceService financeService)
    {
        _logger = logger;
        _establishmentApi = establishmentApi;
        _financeService = financeService;
    }

    [HttpGet]
    public async Task<IActionResult> Index(string companyNumber)
    {
        using (_logger.BeginScope(new { companyNumber }))
        {
            try
            {
                var node = new MvcBreadcrumbNode("Index", "Trust", "Your trust") { RouteValues = new { companyNumber } };

                ViewData["BreadcrumbNode"] = node;
                
                var trust = await _establishmentApi.GetTrust(companyNumber).GetResultOrThrow<Trust>();
                var finances = await _financeService.GetFinances(trust);
                
                var viewModel = new TrustViewModel(trust, finances);
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying trust details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}