using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Web.App.Domain;
using Web.App.Infrastructure.Apis;
using Web.App.Infrastructure.Extensions;
using Web.App.Services;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("school/{urn}/comparators")]
public class SchoolComparatorsController(ILogger<SchoolComparatorsController> logger, IComparatorSetService comparatorSetService, IEstablishmentApi establishmentApi, IFinanceService financeService) : Controller
{
    [HttpGet]
    [Route("building")]
    public async Task<IActionResult> Building(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadComparatorSet(urn);
                var finances = await financeService.GetFinances(set.DefaultArea);
                var viewModel = new SchoolComparatorsViewModel(school, finances);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school building comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("pupil")]
    public async Task<IActionResult> Pupil(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadComparatorSet(urn);
                var finances = await financeService.GetFinances(set.DefaultPupil);
                var viewModel = new SchoolComparatorsViewModel(school, finances);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school pupil comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpGet]
    [Route("view-school")]
    public async Task<IActionResult> ViewSchool(string urn, string target)
    {
        using (logger.BeginScope(new { urn, target}))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;

                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
        
                var sourceSchool = await financeService.GetFinances(urn);
                var targetSchool = await financeService.GetFinances(target);
                var viewModel = new SchoolComparatorViewModel(school, sourceSchool, targetSchool);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparison: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    [HttpGet]
    [Route("custom")]
    public async Task<IActionResult> Custom(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;
                
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }


    [HttpGet]
    [Route("custom/create")]
    public async Task<IActionResult> Create(string urn)
    {
        using (logger.BeginScope(new { urn }))
        {
            try
            {
                ViewData[ViewDataKeys.UseJsBackLink] = true;
                
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var viewModel = new SchoolComparatorsViewModel(school);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparators: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
}