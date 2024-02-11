using EducationBenchmarking.Web.Domain;
using EducationBenchmarking.Web.Infrastructure.Apis;
using EducationBenchmarking.Web.Infrastructure.Extensions;
using EducationBenchmarking.Web.Services;
using EducationBenchmarking.Web.TagHelpers;
using EducationBenchmarking.Web.ViewModels;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace EducationBenchmarking.Web.Controllers;

[Controller]
[Route("school/{urn}/comparator-set")]
public class SchoolComparatorSetController(ILogger<SchoolComparatorSetController> logger, IComparatorSetService comparatorSetService, IEstablishmentApi establishmentApi) : Controller
{
    [HttpGet]
    public async Task<IActionResult> Index(string urn, string referrer)
    {
        using (logger.BeginScope(new {urn, referrer}))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = RefererInfo(referrer, urn);
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                var set = await comparatorSetService.ReadSchoolComparatorSet(urn);
                var viewModel = new SchoolComparatorSetViewModel(school, set, referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparator set: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> Index(string urn, string referrer, [FromForm]string action)
    {
        using (logger.BeginScope(new {urn, referrer}))
        {
            try
            {
                ViewData[ViewDataConstants.Backlink] = RefererInfo(referrer, urn);
                var school = await establishmentApi.GetSchool(urn).GetResultOrThrow<School>();
                ComparatorSetAction setAction = action;
                var set = setAction.Action switch
                {
                    ComparatorSetAction.Remove => await comparatorSetService.RemoveSchoolFromComparatorSet(urn, setAction.Urn ?? throw new ArgumentNullException(setAction.Urn)),
                    ComparatorSetAction.Reset =>  await comparatorSetService.ResetSchoolComparatorSet(urn),
                    _ => throw new ArgumentOutOfRangeException(nameof(setAction.Action))
                };
                
                var viewModel = new SchoolComparatorSetViewModel(school, set, referrer);
                return View(viewModel);
            }
            catch (Exception e)
            {
                logger.LogError(e, "An error displaying school comparator set: {DisplayUrl}", Request.GetDisplayUrl());
                return e is StatusCodeException s ? StatusCode((int)s.Status) : StatusCode(500);
            }
        }
    }

    private BacklinkInfo RefererInfo(string referrer, string urn)
    {
        return referrer switch
        {
            Referrers.SchoolComparison => new BacklinkInfo(Url.Action("Index", "SchoolComparison", new { urn })),
            Referrers.SchoolWorkforce => new BacklinkInfo(Url.Action("Index", "SchoolWorkforce", new { urn })),
            _ => throw new ArgumentOutOfRangeException(nameof(referrer))
        };
    }
}

public class ComparatorSetAction
{
    public const string Reset = "reset";
    public const string Remove = "remove";
    
    public string? Action { get; private set; }
    public string? Urn { get; private set; }
    
    public static implicit operator ComparatorSetAction(string v)
    {
        if (v.StartsWith(Remove))
        {
            var comps = v.Split("-", StringSplitOptions.RemoveEmptyEntries);
            var urn = comps[1];
            
            return new ComparatorSetAction {Action = Remove, Urn = urn};
        }

        return new ComparatorSetAction { Action = Reset };
    }
}