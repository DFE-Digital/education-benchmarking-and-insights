using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/expenditure")]
public class SchoolExpenditureController : Controller
{
    private readonly IEstablishmentApi _establishmentApi;
    private readonly ILogger<SchoolExpenditureController> _logger;
    private readonly IInsightApi _insightApi;

    public SchoolExpenditureController(IEstablishmentApi establishmentApi, ILogger<SchoolExpenditureController> logger, IInsightApi insightApi)
    {
        _establishmentApi = establishmentApi;
        _logger = logger;
        _insightApi = insightApi;
    }

    [HttpGet]
    public async Task<IActionResult> Details(string urn)
    {
        using (_logger.BeginScope(new {urn}))
        {
            try
            {
                var school = await _establishmentApi.Get(urn).GetResultOrThrow<School>();
                var finances = await GetFinances(school).GetResultOrThrow<Finances>();
                var viewModel = new SchoolExpenditureViewModel(school, finances);
                
                return View(viewModel);
            }
            catch (Exception e)
            {
                _logger.LogError(e, "An error displaying school details: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private async Task<ApiResult> GetFinances(School school)
    {
        switch (school.FinanceType)
        {
            case EstablishmentTypes.Academies:
                return await _insightApi.GetAcademyFinances(school.Urn);
            case EstablishmentTypes.Federation:
            case EstablishmentTypes.Maintained:
                return await _insightApi.GetMaintainedSchoolFinances(school.Urn);
            default:
                throw new ArgumentOutOfRangeException(nameof(school.Kind));
        }
    }
}