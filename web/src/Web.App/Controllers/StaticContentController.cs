using Microsoft.AspNetCore.Mvc;
using Web.App.ViewModels;

namespace Web.App.Controllers;

[Controller]
[Route("/")]
public class StaticContentController : Controller
{
    // TODO: review for public beta
    //[HttpGet]
    //[Route("help-with-this-service")]
    //public IActionResult ServiceHelp()
    //{
    //    return View();
    //}

    // TODO: review for public beta
    //[HttpGet]
    //[Route("submit-an-enquiry")]
    //public IActionResult SubmitEnquiry()
    //{
    //    return View();
    //}

    // TODO: review for public beta
    //[HttpGet]
    //[Route("ask-for-help")]
    //public IActionResult AskForHelp()
    //{
    //    return View();
    //}

    // TODO: review for public beta
    //[HttpGet]
    //[Route("privacy")]
    //public IActionResult Privacy()
    //{
    //    ViewData[ViewDataKeys.UseJsBackLink] = true;
    //    return View();
    //}

    [HttpGet]
    [Route("contact")]
    public IActionResult Contact()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [Route("accessibility")]
    public IActionResult Accessibility()
    {
        ViewData[ViewDataKeys.UseJsBackLink] = true;
        return View();
    }

    [HttpGet]
    [Route("guidance/cost-categories")]
    public IActionResult CostCategories()
    {
        return View();
    }

    [HttpGet]
    [Route("guidance/high-needs-glossary")]
    public IActionResult HighNeedsGlossary()
    {
        return View(new HighNeedsGlossaryViewModel());
    }
}